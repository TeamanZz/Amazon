using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterBag : MonoBehaviour
{
    //public float defaultScale = 0.75f;
    public static CharacterBag characterBag;
    public List<StorageItem> storageItems = new List<StorageItem>();

    public Transform firstPoint;

    public float boxScale = 0.5f;
    public float barrelScale = 1f;

    public float itemScaleInBag = 0.5f;

    public int maximumBoxLoadCapacity = 10;
    public int maximumBarrelLoadCapacity = 1;

    private Animator animator;

    public StorageItemFlying flyingPrefab;

    public BagItemType currentBagType;
    public enum BagItemType
    {
        noType,
        box,
        barrel
    }

    public void Awake()
    {
        characterBag = this;
        animator = GetComponent<Animator>();
    }

    public void ReceivingItem(StorageItem newItemPrefab, StorageItem.ItemType type)
    {
        if (storageItems.Count == 0)
            currentBagType = BagItemType.noType;

        if(currentBagType == BagItemType.noType)
        {
            if (type == StorageItem.ItemType.CleanBarrel || type == StorageItem.ItemType.DirtyBarrel)
                currentBagType = BagItemType.barrel;
            else
                currentBagType = BagItemType.box;
        }

        Debug.Log("Go to switch");
        switch(currentBagType)
        {
            case BagItemType.box:
                if (type == StorageItem.ItemType.CleanBarrel || type == StorageItem.ItemType.DirtyBarrel)
                {
                    Debug.Log("Not a box");
                    return;
                }
                if (storageItems.Count >= maximumBoxLoadCapacity)
                    return;
            break;

            case BagItemType.barrel:
                if (type != StorageItem.ItemType.CleanBarrel && type != StorageItem.ItemType.DirtyBarrel)
                {
                    Debug.Log("Not a barrel");
                    return;
                }
                if (storageItems.Count >= maximumBarrelLoadCapacity)
                    return;
                break;
        }

        StorageItem currentItem = Instantiate(newItemPrefab);
        StorageItemFlying itemFlying = Instantiate(flyingPrefab);
        currentItem.currentItemType = type;

        //if (currentBagType == BagItemType.barrel)
        //{
        //    itemScaleInBag = barrelScale;
        //}
        //else
        //    itemScaleInBag = boxScale;

        Vector3 point = new Vector3(0, storageItems.Count * itemScaleInBag, 0);

        storageItems.Add(currentItem);
        currentItem.transform.parent = firstPoint;
        itemFlying.transform.parent = firstPoint;
        itemFlying.transform.localPosition = new Vector3(0, (storageItems.Count + 1) * itemScaleInBag, 0);
        //currentItem.transform.localScale = Vector3.one * itemScaleInBag;
        StartCoroutine(IEEnableRendererOnStart(currentItem));

        currentItem.transform.localPosition = point; //+ new Vector3(0, 5, 0);
        currentItem.transform.eulerAngles = Vector3.zero;
        itemFlying.FlyTo(new Vector3(0, (storageItems.Count - 1) * itemScaleInBag, 0), currentItem.transform.eulerAngles, type);

        PositionsCheck();

        animator.SetBool("IsCarrying", true);
        Debug.Log("Add");
    }
    private IEnumerator IEEnableRendererOnStart(StorageItem currentStorage)
    {
        currentStorage.transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(0.4f);
        currentStorage.transform.DOScale(Vector3.one * itemScaleInBag, 0.1f);
    }

    public void PositionsCheck()
    {
        Vector3 nullPoint;// = new Vector3(firstPoint.localPosition.x, 0, firstPoint.localPosition.z); ;
        for (int i = 0; i < storageItems.Count; i++)
        {
            nullPoint = new Vector3(0, i * itemScaleInBag, 0);
            storageItems[i].transform.DOLocalMove(nullPoint, 0.2f);
        }

        if (storageItems.Count == 0)
            currentBagType = BagItemType.noType;
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
            removItem.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutBack);
            Destroy(removItem.gameObject, 0.3f);

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
            removItem.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutBack);
            Destroy(removItem.gameObject, 0.3f);
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

        StorageItem removItem = null;

        foreach (var item in storageItems)
        {
            if (item.currentItemType == type)
            {
                removItem = item;
                break;
            }
        }

        Debug.Log("Remove");
        if (removItem != null)
        {
            storageItems.Remove(removItem);
            removItem.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutBack);
            Destroy(removItem.gameObject, 0.3f);
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

    public void SendBarrelToProcessing(BarrelProcessingOutputZone barrelProcessingOutputZone)
    {
        Debug.Log("In Bag");
        if (storageItems.Count > 1 || storageItems.Count == 0)
            return;

        Debug.Log(storageItems.Count);

        if (storageItems[0].currentItemType != StorageItem.ItemType.DirtyBarrel)
            return;

        Debug.Log("Pre Send");
        if (storageItems[0].currentItemType == StorageItem.ItemType.DirtyBarrel)
        {
            StorageItem item = storageItems[0];
            item.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutBack);
            storageItems.Remove(item);
            Destroy(item.gameObject, 0.3f);

            Debug.Log("Processing");
            barrelProcessingOutputZone.parentBarrelProcessingStation.CheckProcessing();
        }

        Debug.Log("No Send");
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
}
