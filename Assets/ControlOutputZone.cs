using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOutputZone : MonoBehaviour
{
    public Transform target;
    public float distanceToCentr = 0.5f;
    public bool targetInPlace = false;

    Color currentColor;
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

    public void Awake()
    {
        currentTime = reloadTime;

        ColoringThisZone();
    }

    public void ColoringThisZone()
    {
        if(currentZoneState == ZoneState.Receiving)
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
        }

    }

    public void Update()
    {
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

        StorageItem.ItemType type = (StorageItem.ItemType)Random.Range(0, StorageItem.itemTypeCount);
        Debug.Log(type);
        //int randomNuber = Random.Range(0, StorageItem.itemTypeCount);
        //switch(randomNuber)
        //{
        //    case 0:

        //        break;


        //}
        CharacterBag.characterBag.ReceivingItem(itemPrefab, type);
    }

    public void SendItem()
    {
        if (CharacterBag.characterBag == null)
            return;

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
        }

    }

    public void ShipmentProcessing()
    {
        Debug.Log("Processing");
    }

    public void CheckColor()
    {
        if (targetInPlace)
            currentColor = Color.blue;
        else
            currentColor = Color.yellow;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = currentColor;
        Gizmos.DrawWireCube(transform.position, Vector3.one * distanceToCentr);

    }
}
