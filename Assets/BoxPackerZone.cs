using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxPackerZone : MonoBehaviour
{
    [Header("Passive settings")]
    //public Transform target;
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
    public BoxPacker parentProcessingStation;

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

        float distance = Vector3.Distance(transform.position, PlayerMovement.targetPlayer.position);
        if (distance < distanceToCentr)
            targetInPlace = true;
        else
            targetInPlace = false;

        //CheckColor();
        centrPoint.color = currentColor;
        Debug.DrawLine(transform.position, PlayerMovement.targetPlayer.position, currentColor);

        if (!targetInPlace)
            return;

        if (currentTime <= 0)
        {
            currentTime = reloadTime;
            switch (currentZoneState)
            {
                case ZoneState.Receiving:
                    if (parentProcessingStation.itemOutTypeStack.Count < 1)
                        break;

                    ReceivingItem();
                    break;

                case ZoneState.Send:
                    if (parentProcessingStation.itemInTypeStack.Count >= parentProcessingStation.maximumInputCount)
                        break;

                    SendItem();
                    break;
            }

        }
        else
            currentTime -= Time.deltaTime;

    }

    //  в рюкзак
    public void ReceivingItem()
    {
        if (CharacterBag.characterBag == null)
            return;

        if (parentProcessingStation.itemOutTypeStack.Count < 1)
            return;

        CharacterBag.characterBag.ReceivingItem(itemPrefab, parentProcessingStation.itemOutTypeStack[0]);
        //parentProcessingStation.itemOutTypeStack.Remove(0);
        parentProcessingStation.ReceivingToBackpack();

        parentProcessingStation.ViewUI();
    }

    //  в здание
    public void SendItem()
    {
        if (CharacterBag.characterBag == null)
            return;

        if (CharacterBag.characterBag.storageItems.Count < 1)
            return;

        //SendToStation(CharacterBag.characterBag.storageItems[0].currentItemType);
        //parentProcessingStation.SendToPacker(CharacterBag.characterBag.storageItems[0].currentItemType);
        CharacterBag.characterBag.SendPackerItem(this);

        parentProcessingStation.ViewUI();
    }
}
