using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessingOutputZone : MonoBehaviour
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
    public ProcessingStation parentProcessingStation;

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
                    currentColor = new Color(0.8207547f, 0.2284176f, 0.2801296f);
                    break;

                case StorageItem.ItemType.BlueBox:
                    currentColor = new Color(0.5364009f, 0.7325992f, 0.9245283f);
                    break;

                case StorageItem.ItemType.GreenBox:
                    currentColor = new Color(0.114142f, 0.8962264f, 0.4832793f);
                    break;

                case StorageItem.ItemType.NoType:
                    currentColor = Color.white;
                    break;

                case StorageItem.ItemType.YellowBox:
                    currentColor = Color.yellow;
                    break;
            }

    }

    public void FixedUpdate()
    {
        if (zoneIsActive == false)
            return;

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
    //  �������� � ������
    public void ReceivingItem()
    {
        if (CharacterBag.characterBag == null)
            return;

        if (parentProcessingStation.outputItemsCount <= 0)
            return;

        parentProcessingStation.outputItemsCount -= 1;
        parentProcessingStation.ViewUI();

        //StorageItem.ItemType type = (StorageItem.ItemType)Random.Range(0, StorageItem.itemTypeCount);
        StorageItem.ItemType type = StorageItem.ItemType.YellowBox;
        //Debug.Log(type);
        CharacterBag.characterBag.ReceivingItem(itemPrefab, type);
    }

    //  �������� � ������
    public void SendItem()
    {
        if (CharacterBag.characterBag == null)
            return;

        //if (parentProcessingStation.currentAmountCount >= parentProcessingStation.maximumAmountCount)
        //    return;

        switch (currentDeployType)
        {
            case StorageItem.ItemType.RedBox:
                if (parentProcessingStation.amounts[0] < parentProcessingStation.maximumAmountCount)
                    CharacterBag.characterBag.SendProcessingItem(StorageItem.ItemType.RedBox, this, 0);
                //parentProcessingStation.CheckProcessing(0);
                break;

            case StorageItem.ItemType.BlueBox:
                if (parentProcessingStation.amounts[1] < parentProcessingStation.maximumAmountCount)
                    CharacterBag.characterBag.SendProcessingItem(StorageItem.ItemType.BlueBox, this, 1);
                //parentProcessingStation.CheckProcessing(1);
                break;

            case StorageItem.ItemType.GreenBox:
                if (parentProcessingStation.amounts[2] < parentProcessingStation.maximumAmountCount)
                    CharacterBag.characterBag.SendProcessingItem(StorageItem.ItemType.GreenBox, this, 2);
                //parentProcessingStation.CheckProcessing(2);
                break;

            case StorageItem.ItemType.NoType:
                CharacterBag.characterBag.SendProcessingItem(StorageItem.ItemType.NoType, this, 4);
                break;
        }

        parentProcessingStation.ViewUI();
    }

    public void SendToStation(int number)
    {
        parentProcessingStation.CheckProcessing(number);
    }


}
