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
    private Animator animator;
    public void Awake()
    {
        characterBag = this;
        animator = GetComponent<Animator>();
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
        {
            animator.SetBool("IsCarrying", false);
            point = new Vector3(firstPoint.localPosition.x, 0, firstPoint.localPosition.z);
        }
        else
        {
            point = new Vector3(firstPoint.localPosition.x, 0 + storageItems.Count * itemScaleInBag, firstPoint.localPosition.z);
            animator.SetBool("IsCarrying", true);
        }

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

    public void SendItem(StorageItem.ItemType type, ControlOutputZone currentZone)
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
        {
            removItem = storageItems[0];
            finnd = true;
        }

        if (finnd == true)
        {
            Debug.Log("Remove");
            storageItems.Remove(removItem);
            Destroy(removItem.gameObject);

            if (storageItems.Count == 0)
            {
                animator.SetBool("IsCarrying", false);
            }
            else
            {
                animator.SetBool("IsCarrying", true);
            }

            currentZone.ShipmentProcessing();
        }
        PositionsCheck();
    }
    public void SendToShelveItem(StorageItem.ItemType type, ShelvesOutputZone outputZone)
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
        {
            removItem = storageItems[0];
            finnd = true;
        }

        Debug.Log("Remove");
        if (removItem != null && finnd == true)
        {
            storageItems.Remove(removItem);
            Destroy(removItem.gameObject);
        }

        if (storageItems.Count == 0)
        {
            animator.SetBool("IsCarrying", false);
        }
        else
        {
            animator.SetBool("IsCarrying", true);
        }

        PositionsCheck();
    }

    public bool FindType(StorageItem.ItemType type)
    {
        bool finnd = false;

        if (type != StorageItem.ItemType.NoType)
        {
            foreach (var item in storageItems)
            {
                if (item.currentItemType == type)
                {
                    finnd = true;
                    break;
                }
            }
        }
        else
            finnd = true;

        return finnd;
    }

    public void SendProcessingItem(StorageItem.ItemType type, ProcessingOutputZone outputZone, int number)
    {
        if (storageItems.Count <= 0)
            return;

        if (outputZone.parentProcessingStation.amounts[number] >= outputZone.parentProcessingStation.maximumAmountCount)
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
            if (storageItems.Count == 0)
            {
                animator.SetBool("IsCarrying", false);
            }
            else
            {
                animator.SetBool("IsCarrying", true);
            }
            outputZone.SendToStation(number);
        }

        PositionsCheck();
    }
}
