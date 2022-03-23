using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarrelProcessingOutputZone : MonoBehaviour
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
    public BarrelProcessingStation parentBarrelProcessingStation;

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
                case StorageItem.ItemType.DirtyBarrel:
                    currentColor = Color.white;
                    break;

                case StorageItem.ItemType.CleanBarrel:
                    currentColor = Color.blue;
                    break;
            }

    }

    public void Update()
    {
        if (zoneIsActive == false)
            return;

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < distanceToCentr)
        {
            targetInPlace = true;
            parentBarrelProcessingStation.zoneIsActive = true;
        }
        else
        {
            targetInPlace = false;
            parentBarrelProcessingStation.zoneIsActive = false;
        }
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
    //  �������� � ������
    public void ReceivingItem()
    {
        if (CharacterBag.characterBag == null)
            return;

        if (CharacterBag.characterBag.storageItems.Count > 1)
            return;

        if (parentBarrelProcessingStation.outputItemsCount <= 0)
            return;

        parentBarrelProcessingStation.outputItemsCount -= 1;
        parentBarrelProcessingStation.ViewUI();
        parentBarrelProcessingStation.CheckOutObject();

        CharacterBag.characterBag.ReceivingItem(itemPrefab, StorageItem.ItemType.CleanBarrel);
    }

    //  �������� � ������
    public void SendItem()
    {
        if (CharacterBag.characterBag == null)
            return;

        if (CharacterBag.characterBag.storageItems.Count > 1)
            return;

        //if (CharacterBag.characterBag.storageItems[0].currentItemType != StorageItem.ItemType.DirtyBarrel)
        //    return;

        Debug.Log("Send");
        CharacterBag.characterBag.SendBarrelToProcessing(this);

        parentBarrelProcessingStation.ViewUI();
    }

}
