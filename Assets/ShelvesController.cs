using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelvesController : MonoBehaviour
{
    [Header("Main settings")]
    public Transform storageForPrefabs; 
    public float itemScaleOnShelf = 1f;

    public List<ShelvesOutputZone> inputZones = new List<ShelvesOutputZone>();
    public ShelvesOutputZone outputZone;

    [Header("Connect settings")]
    public List<LocalShelves> localShelves = new List<LocalShelves>();

    public List<Vector3> positions = new List<Vector3>();
    public List<StorageItem> storageItems = new List<StorageItem>();

    public int maxOutputCount;
    public int outputItemsCount = 0;

    public StorageItem.ItemType currentDeployType;

    public void Awake()
    {
        foreach (var zone in inputZones)
        {
            zone.parentShelvesController = this;
        }
        outputZone.parentShelvesController = this;

        foreach(var shalve in localShelves)
        {
            shalve.InitializationParent(this);
        }

        storageItems.Clear();
        maxOutputCount = positions.Count;
        outputItemsCount = storageItems.Count;
    }
    public void InitializationChildShalves(List<Vector3> childStoragePositions)
    {
        positions.AddRange(childStoragePositions);
        CheckAllPositions();
    }

    public void SendItem(StorageItem.ItemType type, StorageItem newitemPrefab)
    {
        if (storageItems.Count > maxOutputCount - 1)
            return;

        StorageItem currentItem = Instantiate(newitemPrefab);
        currentItem.currentItemType = type;

        Debug.Log("Add On Shelve");
        storageItems.Add(currentItem);

        Vector3 point = positions[storageItems.Count - 1];
        
        currentItem.transform.parent = storageForPrefabs;
        currentItem.transform.localScale = Vector3.one * itemScaleOnShelf;
        currentItem.transform.localPosition = point;

        CheckAllPositions();
    }

    public void ReceivingItem(ShelvesOutputZone shelvesOutputZone)
    {
        if (storageItems.Count < 1)
            return;

        Debug.Log("Controller receve");
        StorageItem item = storageItems[0];
        shelvesOutputZone.SendReceivingData(item);

        storageItems.Remove(item);
        Destroy(item.gameObject);

        CheckAllPositions();
    }

    public void CheckAllPositions()
    {
        foreach (var item in storageItems)
            item.transform.position = Vector3.zero;

        for (int i = 0; i < storageItems.Count; i++)
        {
            storageItems[i].transform.position = positions[i];
        }
    }
}
