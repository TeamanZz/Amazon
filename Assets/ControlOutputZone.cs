using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOutputZone : MonoBehaviour
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

    public ZoneType currentZoneType;
    public enum ZoneType
    {
        Statick,
        Active,
        Processing
    }
    [Header("Processing Station")]
    public ProcessingStation connectProcessingStation;

    [Header("Delivery")]
    public DeliveryMachine deliveryMachine;

    public StorageItem itemPrefab;

    public StorageItem.ItemType currentDeployType;

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

                case StorageItem.ItemType.YellowBox:
                    currentColor = Color.yellow;
                    break;

                case StorageItem.ItemType.GreenBox:
                    currentColor = Color.green;
                    break;

                case StorageItem.ItemType.NoType:
                    currentColor = Color.black;
                    break;

                case StorageItem.ItemType.OrangeBox:
                    currentColor = new Color32(255, 114, 0, 255);
                    break;

                case StorageItem.ItemType.LimeBox:
                    currentColor = new Color32(138, 255, 13, 255);
                    break;

                case StorageItem.ItemType.BrownBox:
                    currentColor = new Color32(87, 46, 4, 255);
                    break;

                case StorageItem.ItemType.GrayBox:
                    currentColor = new Color32(84, 71, 56, 255);
                    break;

                case StorageItem.ItemType.WasteBox:
                    currentColor = new Color32(0, 255, 238, 255);
                    break;
            }

    }

    public void Update()
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
    public void ReceivingItem()
    {
        if (CharacterBag.characterBag == null)
            return;


        if (currentZoneType != ZoneType.Processing)
            switch (currentDeployType)
            {
                case StorageItem.ItemType.RedBox:
                    CharacterBag.characterBag.ReceivingItem(itemPrefab, StorageItem.ItemType.RedBox);
                    break;

                case StorageItem.ItemType.YellowBox:
                    CharacterBag.characterBag.ReceivingItem(itemPrefab, StorageItem.ItemType.YellowBox);
                    break;

                case StorageItem.ItemType.GreenBox:
                    CharacterBag.characterBag.ReceivingItem(itemPrefab, StorageItem.ItemType.GreenBox);
                    break;

                case StorageItem.ItemType.NoType:
                    StorageItem.ItemType type = (StorageItem.ItemType)Random.Range(0, 3);//StorageItem.itemTypeCount);
                    Debug.Log(type);
                    CharacterBag.characterBag.ReceivingItem(itemPrefab, type);
                    break;

                //  new     processing colors
                case StorageItem.ItemType.OrangeBox:
                    CharacterBag.characterBag.ReceivingItem(itemPrefab, StorageItem.ItemType.OrangeBox);
                    break;

                case StorageItem.ItemType.LimeBox:
                    CharacterBag.characterBag.ReceivingItem(itemPrefab, StorageItem.ItemType.LimeBox);
                    break;

                case StorageItem.ItemType.BrownBox:
                    CharacterBag.characterBag.ReceivingItem(itemPrefab, StorageItem.ItemType.BrownBox);
                    break;

                case StorageItem.ItemType.GrayBox:
                    CharacterBag.characterBag.ReceivingItem(itemPrefab, StorageItem.ItemType.GrayBox);
                    break;
            }
        else
            connectProcessingStation.ReceivingItem(itemPrefab, currentDeployType);
    }

    public void SendItem()
    {
        if (CharacterBag.characterBag == null)
            return;
        if (currentZoneType != ZoneType.Processing)
            switch (currentDeployType)
            {
                case StorageItem.ItemType.RedBox:
                    CharacterBag.characterBag.SendItem(StorageItem.ItemType.RedBox, this);
                    break;

                case StorageItem.ItemType.YellowBox:
                    CharacterBag.characterBag.SendItem(StorageItem.ItemType.YellowBox, this);
                    break;

                case StorageItem.ItemType.GreenBox:
                    CharacterBag.characterBag.SendItem(StorageItem.ItemType.GreenBox, this);
                    break;

                case StorageItem.ItemType.NoType:
                    CharacterBag.characterBag.SendItem(StorageItem.ItemType.NoType, this);
                    break;

                //  new     processing colors
                case StorageItem.ItemType.OrangeBox:
                    CharacterBag.characterBag.SendItem(StorageItem.ItemType.OrangeBox, this);
                    break;

                case StorageItem.ItemType.LimeBox:
                    CharacterBag.characterBag.SendItem(StorageItem.ItemType.LimeBox, this);
                    break;

                case StorageItem.ItemType.BrownBox:
                    CharacterBag.characterBag.SendItem(StorageItem.ItemType.BrownBox, this);
                    break;

                case StorageItem.ItemType.GrayBox:
                    CharacterBag.characterBag.SendItem(StorageItem.ItemType.GrayBox, this);
                    break;
            }
        else
            connectProcessingStation.SendItem(currentDeployType, this);

    }

    public void ShipmentProcessing()
    {
        Debug.Log("Processing");
        switch (currentZoneType)
        {
            case ZoneType.Statick:
                //  νθυσÿ
                break;

            case ZoneType.Active:
                if (deliveryMachine == null)
                    return;

                deliveryMachine.OrderProcessing();
                Debug.Log("Zone to order");
                break;

            case ZoneType.Processing:
                if (connectProcessingStation == null)
                    break;

                break;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = currentColor;
        Gizmos.DrawWireCube(transform.position, Vector3.one * distanceToCentr);

    }
}
