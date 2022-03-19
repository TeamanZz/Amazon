using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConveyorOutputZone : MonoBehaviour
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
    
    [Header("Parent Settings")]
    public Conveyor parentConveyor;

    public void Awake()
    {
        currentTime = reloadTime;
        parentConveyor.conveyorOutputZone = this;

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
            }

        // Debug.Log("Coloring");
    }
    public void Update()
    {
        if (zoneIsActive == false)
            return;

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < distanceToCentr)
        {
            targetInPlace = true;
            parentConveyor.isActive = true;
        }
        else
        {
            targetInPlace = false;
            parentConveyor.isActive = false;
        }

        if (CharacterBag.characterBag.storageItems.Count >= CharacterBag.characterBag.maximumLoadCapacity)
            parentConveyor.sendToBacpack = false;
        else
            parentConveyor.sendToBacpack = true;

        //CheckColor();
        centrPoint.color = currentColor;
        Debug.DrawLine(transform.position, target.position, currentColor);

        if (!targetInPlace)
            return;

        //if (currentTime <= 0)
        //{
        //    currentTime = reloadTime;
        //    switch (currentZoneState)
        //    {
        //        case ZoneState.Receiving:
        //            ReceivingItem();
        //            break;

        //        case ZoneState.Send:
        //            currentZoneState = ZoneState.Receiving;
        //            break;
        //    }

        //}
        //else
        //    currentTime -= Time.deltaTime;

    }

    //  загрузка в рюкзак
    public void ReceivingItem()
    {
        if (CharacterBag.characterBag == null)
            return;

        Debug.Log("Zone receve");
        CharacterBag.characterBag.ReceivingItem(itemPrefab, currentDeployType);

    }
}
