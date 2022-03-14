using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBag : MonoBehaviour
{
    public static CharacterBag characterBag;
    public List<StorageItem> storageItems = new List<StorageItem>();

    public Transform firstPoint;
    public Vector3 currentPoint;

    public float itemScaleInBag = 0.5f;

    public int maximumLoadCapacity = 10;

    public void Awake()
    {
        characterBag = this;
    }
    public void ReceivingItem(StorageItem newItemPrefab, StorageItem.ItemType type)
    {
        if (storageItems.Count >= maximumLoadCapacity)
            return;

        StorageItem currentItem = Instantiate(newItemPrefab);
        currentItem.currentItemType = type;
        Debug.Log("Add");
        storageItems.Add(currentItem);
        Vector3 point;
        if (storageItems.Count == 0)
            point = new Vector3(firstPoint.localPosition.x, 0, firstPoint.localPosition.z);

        else
            point = new Vector3(firstPoint.localPosition.x, 0 + storageItems.Count * itemScaleInBag, firstPoint.localPosition.z);

        Debug.Log(point);
        currentPoint = point;

        currentItem.transform.parent = firstPoint;
        currentItem.transform.localScale = Vector3.one * itemScaleInBag;
        currentItem.transform.localPosition = currentPoint;

        PositionsCheck();
    }

    public void PositionsCheck()
    {
        Vector3 nullPoint;// = new Vector3(firstPoint.localPosition.x, 0, firstPoint.localPosition.z); ;
        for (int i = 0; i < storageItems.Count; i++)
        {
            nullPoint = new Vector3(firstPoint.localPosition.x, 0 + i * itemScaleInBag, firstPoint.localPosition.z);
            storageItems[i].transform.localPosition = nullPoint;
        }
    }

    public void SendItem(StorageItem.ItemType type)//, ControlOutputZone currentZone)
    {
        if (storageItems.Count <= 0)
            return;

        bool finnd = false;
        StorageItem removItem = null;

        if (type != StorageItem.ItemType.NoType)
        {
            foreach (var item in storageItems)
            {
                if (item.currentItemType == type)
                {
                    finnd = true;
                    removItem = item;
                    break;
                }
            }
        }
        else
            removItem = storageItems[0];

        Debug.Log("Remove");
        storageItems.Remove(removItem);
        Destroy(removItem.gameObject);

        PositionsCheck();
    }

    public void SendProcessingItem(StorageItem.ItemType type, ProcessingOutputZone outputZone, int number)
    {
        if (storageItems.Count <= 0)
            return;

        bool finnd = false;
        StorageItem removItem = null;

        foreach (var item in storageItems)
        {
            if (item.currentItemType == type)
            {
                finnd = true;
                removItem = item;
                break;
            }
        }

        Debug.Log("Remove");
        if (removItem != null)
        {
            storageItems.Remove(removItem);
            Destroy(removItem.gameObject);

            outputZone.SendToStation(number);
        }

        PositionsCheck();
    }
}
