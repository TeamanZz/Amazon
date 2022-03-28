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
        currentColor = Color.white;

    }

    public void Update()
    {
        if (zoneIsActive == false)
            return;

        float distance = Vector3.Distance(transform.position, PlayerMovement.targetPlayer.position);
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
        Debug.DrawLine(transform.position, PlayerMovement.targetPlayer.position, currentColor);

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
        }

        type = currentDeployType;
        //StorageItem.ItemType type = (StorageItem.ItemType)Random.Range(0, StorageItem.itemTypeCount);
        Debug.Log(type);
        CharacterBag.characterBag.ReceivingItem(itemPrefab, type);
    }

    public void SendItem()
    {
        if (CharacterBag.characterBag == null)
            return;

        CharacterBag.characterBag.SendItem(currentDeployType, this);
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
