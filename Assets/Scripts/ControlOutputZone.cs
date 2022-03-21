using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOutputZone : MonoBehaviour
{
    public bool isStatick = false;
    public GameObject spawnerObject;
    public Animator animator;

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
        Active
    }
    public DeliveryMachine deliveryMachine;

    public StorageItem itemPrefab;

    public StorageItem.ItemType currentDeployType;

    public void Awake()
    {
        if (isStatick == true)
        {
            animator = spawnerObject.GetComponent<Animator>();
            animator.SetBool("Open", false);
        }

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

                case StorageItem.ItemType.YellowBox:
                    currentColor = Color.yellow;
                    break;

                case StorageItem.ItemType.NoType:
                    currentColor = Color.black;
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

    }

    public void Update()
    {
        if (zoneIsActive == false)
            return;

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < distanceToCentr)
        {
            targetInPlace = true;

            if (animator != null)
                animator.SetBool("Open", true);
        }
        else
        {
            targetInPlace = false;
            if (animator != null)
                animator.SetBool("Open", false);
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

    public void ReceivingItem()
    {
        if (CharacterBag.characterBag == null)
            return;

        StorageItem.ItemType type = StorageItem.ItemType.GreenBox;
        switch (currentDeployType)
        {
            case StorageItem.ItemType.NoType:
                type = (StorageItem.ItemType)Random.Range(0, StorageItem.itemTypeCount);
                break;

            case StorageItem.ItemType.RedBox:
                type = StorageItem.ItemType.RedBox;
                break;

            case StorageItem.ItemType.BlueBox:
                type = StorageItem.ItemType.BlueBox;
                break;

            case StorageItem.ItemType.GreenBox:
                type = StorageItem.ItemType.GreenBox;
                break;

            case StorageItem.ItemType.YellowBox:
                type = StorageItem.ItemType.YellowBox;
                break;

            case StorageItem.ItemType.DirtyBarrel:
                type = StorageItem.ItemType.DirtyBarrel;
                break;

            case StorageItem.ItemType.CleanBarrel:
                type = StorageItem.ItemType.CleanBarrel;
                break;
        }

        //StorageItem.ItemType type = (StorageItem.ItemType)Random.Range(0, StorageItem.itemTypeCount);
        Debug.Log(type);
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

            case StorageItem.ItemType.BlueBox:
                CharacterBag.characterBag.SendItem(StorageItem.ItemType.BlueBox, this);
                break;

            case StorageItem.ItemType.GreenBox:
                CharacterBag.characterBag.SendItem(StorageItem.ItemType.GreenBox, this);
                break;

            case StorageItem.ItemType.YellowBox:
                CharacterBag.characterBag.SendItem(StorageItem.ItemType.YellowBox, this);
                break;

            case StorageItem.ItemType.NoType:
                CharacterBag.characterBag.SendItem(StorageItem.ItemType.NoType, this);
                break;

            case StorageItem.ItemType.DirtyBarrel:
                CharacterBag.characterBag.SendItem(StorageItem.ItemType.DirtyBarrel, this);
                break;

            case StorageItem.ItemType.CleanBarrel:
                CharacterBag.characterBag.SendItem(StorageItem.ItemType.CleanBarrel, this);
                break;
        }

    }

    public void ShipmentProcessing()
    {
        Debug.Log("Processing");
        switch (currentZoneType)
        {
            case ZoneType.Statick:
                //  �����
                break;

            case ZoneType.Active:
                if (deliveryMachine == null)
                    return;

                deliveryMachine.OrderProcessing();
                Debug.Log("Zone to order");

                break;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = currentColor;
        Gizmos.DrawWireCube(transform.position, Vector3.one * distanceToCentr);

    }
}
