using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelvesOutputZone : MonoBehaviour
{
    [Header("Passive settings")]
    public Transform target;
    public float distanceToCentr = 0.5f;

    [Header("Active settings")]
    public bool zoneIsActive = false;
    public bool targetInPlace = false;

    public Color currentColor;
    [Header("View settings")]
    public Image centrPoint;

    public float reloadTime = 0.25f;
    public float currentTime;

    public ZoneState currentZoneState;
    public enum ZoneState
    {
        Receiving,
        Send
    }

    public StorageItem itemPrefab;

    public StorageItem.ItemType currentDeployType;

    [Header("Parent")]
    public ShelvesController parentShelvesController;

    public void Awake()
    {
        currentTime = reloadTime;
        ColoringThisZone();
    }

    public void ChangeZoneSendType(StorageItem.ItemType newType)
    {
        currentDeployType = newType;
        ColoringThisZone();
    }

    public void ColoringThisZone()
    {
        if (currentZoneState == ZoneState.Receiving)
        {
            currentColor = Color.white;
        }
        else
            switch (currentDeployType)
            {
                case StorageItem.ItemType.RedBox:
                    currentColor = Color.red;
                    break;

                case StorageItem.ItemType.BlueBox:
                    currentColor = Color.blue;
                    break;

                case StorageItem.ItemType.GreenBox:
                    currentColor = Color.green;
                    break;

                case StorageItem.ItemType.NoType:
                    currentColor = Color.black;
                    break;

                case StorageItem.ItemType.YellowBox:
                    currentColor = Color.yellow;
                    break;

                case StorageItem.ItemType.DirtyBarrel:
                    currentColor = Color.grey;
                    break;

                case StorageItem.ItemType.CleanBarrel:
                    currentColor = Color.blue;
                    break;

                case StorageItem.ItemType.AllBarel:
                    currentColor = Color.black;
                    break;
            }

       // Debug.Log("Coloring");
    }
    public void Update()
    {
        if (zoneIsActive == false)
            return;

        if(currentDeployType != parentShelvesController.currentDeployType)
            ChangeZoneSendType(parentShelvesController.currentDeployType);
        

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < distanceToCentr)
            targetInPlace = true;
        else
            targetInPlace = false;

        //CheckColor();
        centrPoint.color = currentColor;
        Debug.DrawLine(transform.position, target.position, currentColor);

        if (!targetInPlace)
            return;

        if (currentTime <= 0)
        {
            currentTime = reloadTime;
            switch (currentZoneState)
            {
                case ZoneState.Receiving:
                    ReceivingItem();
                    break;

                case ZoneState.Send:
                    SendItem();
                    break;
            }

        }
        else
            currentTime -= Time.deltaTime;

    }

    //  загрузка в рюкзак
    public void ReceivingItem()
    {
        if (CharacterBag.characterBag == null)
            return;

        if (parentShelvesController.outputItemsCount < 1)
            return;

        Debug.Log("Zone receve");
        parentShelvesController.outputItemsCount -= 1;
        parentShelvesController.ReceivingItem(this);

    }

    public void SendReceivingData(StorageItem currentOutItem)
    {
        CharacterBag.characterBag.ReceivingItem(itemPrefab, currentOutItem.currentItemType);
    }

    public void SendItem()
    {
        if (CharacterBag.characterBag == null)
            return;

        if (parentShelvesController.storageItems.Count > parentShelvesController.maxOutputCount - 1)
            return;

        if (CharacterBag.characterBag.storageItems.Count < 1)
            return;
       
        switch (currentDeployType)
        {
            case StorageItem.ItemType.RedBox:
                if (!CharacterBag.characterBag.FindType(StorageItem.ItemType.RedBox))
                    break;

                parentShelvesController.outputItemsCount += 1;
                CharacterBag.characterBag.SendToShelveItem(StorageItem.ItemType.RedBox, this);
                parentShelvesController.SendItem(StorageItem.ItemType.RedBox, itemPrefab);
                break;

            case StorageItem.ItemType.BlueBox:
                if (!CharacterBag.characterBag.FindType(StorageItem.ItemType.BlueBox))
                    break;

                parentShelvesController.outputItemsCount += 1;
                CharacterBag.characterBag.SendToShelveItem(StorageItem.ItemType.BlueBox, this);
                parentShelvesController.SendItem(StorageItem.ItemType.BlueBox, itemPrefab);
                break;

            case StorageItem.ItemType.GreenBox:
                if (!CharacterBag.characterBag.FindType(StorageItem.ItemType.GreenBox))
                    break;

                parentShelvesController.outputItemsCount += 1;
                CharacterBag.characterBag.SendToShelveItem(StorageItem.ItemType.GreenBox, this);
                parentShelvesController.SendItem(StorageItem.ItemType.GreenBox, itemPrefab);
                break;

            case StorageItem.ItemType.YellowBox:
                if (!CharacterBag.characterBag.FindType(StorageItem.ItemType.YellowBox))
                    break;

                parentShelvesController.outputItemsCount += 1;
                CharacterBag.characterBag.SendToShelveItem(StorageItem.ItemType.YellowBox, this);
                parentShelvesController.SendItem(StorageItem.ItemType.YellowBox, itemPrefab);
                break;

            case StorageItem.ItemType.NoType:
                parentShelvesController.outputItemsCount += 1;
                parentShelvesController.SendItem(CharacterBag.characterBag.storageItems[0].currentItemType, itemPrefab);
                CharacterBag.characterBag.SendToShelveItem(StorageItem.ItemType.NoType, this);
                break;

            case StorageItem.ItemType.DirtyBarrel:
                if (!CharacterBag.characterBag.FindType(StorageItem.ItemType.DirtyBarrel))
                    break;

                parentShelvesController.outputItemsCount += 1;
                CharacterBag.characterBag.SendToShelveItem(StorageItem.ItemType.DirtyBarrel, this);
                parentShelvesController.SendItem(StorageItem.ItemType.DirtyBarrel, itemPrefab);
                break;

            case StorageItem.ItemType.CleanBarrel:
                if (!CharacterBag.characterBag.FindType(StorageItem.ItemType.CleanBarrel))
                    break;

                parentShelvesController.outputItemsCount += 1;
                CharacterBag.characterBag.SendToShelveItem(StorageItem.ItemType.CleanBarrel, this);
                parentShelvesController.SendItem(StorageItem.ItemType.CleanBarrel, itemPrefab);
                break;

            case StorageItem.ItemType.AllBarel:
                parentShelvesController.outputItemsCount += 1;
                parentShelvesController.SendItem(CharacterBag.characterBag.storageItems[0].currentItemType, itemPrefab);
                CharacterBag.characterBag.SendToShelveItem(StorageItem.ItemType.NoType, this);
                break;
        }
    }
}
