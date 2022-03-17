using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class DeliveryMachine : MonoBehaviour
{
    public ControlOutputZone outputZone;
    [Serializable]    
    public class CurrentDeliveryData
    {
        public StorageItem.ItemType orderType;
        public int correctAmount;
        public int currentAmount;
    }

    [Header("Data settings")]
    public CurrentDeliveryData deliveryData;
    public int minAmountCount = 5;
    public int maxAmountCount = 20;

    [Header("View settings")]
    public Image orderImageColoring;
    public TextMeshProUGUI orderAmountText;

    [Header("Move Settings")]
    public Transform deployPoint;
    public Transform endPoint;
    public Transform carTransform;

    public float carSpeed = 5f;


    [ContextMenu("Moved To Deploy")]
    public void MoveToGates()
    {
        carTransform.DOMoveX(deployPoint.position.x, carSpeed).SetEase(Ease.InOutBack);
        carTransform.DOShakeRotation(2, strength: 5, 10);
        ReceivingAnOrder();
    }

    [ContextMenu("Receiving Order")]
    public void ReceivingAnOrder()
    {
        deliveryData = null;

        deliveryData = new CurrentDeliveryData();
        deliveryData.orderType = (StorageItem.ItemType)UnityEngine.Random.Range(0, StorageItem.itemTypeCount + 1);
        deliveryData.correctAmount = UnityEngine.Random.Range(minAmountCount, maxAmountCount);
        deliveryData.currentAmount = 0;

        outputZone.ChangeZoneSendType(deliveryData.orderType);
        orderImageColoring.color = outputZone.currentColor;
        
        ViewUI();
        outputZone.zoneIsActive = true;
    }

    public void ViewUI()
    {
        orderAmountText.text = deliveryData.currentAmount.ToString() + " / " + deliveryData.correctAmount.ToString();
    }

    public void OrderProcessing()
    {
        outputZone.zoneIsActive = true;
        deliveryData.currentAmount += 1;
        Debug.Log(deliveryData.currentAmount);
        ViewUI();

        if (deliveryData.currentAmount >= deliveryData.correctAmount)
            SendingOrder();
    
    }

    [ContextMenu("Moved")]
    public void SendingOrder()
    {
        outputZone.zoneIsActive = false;
        outputZone.currentColor = Color.white;
        outputZone.centrPoint.color = Color.white;
        orderImageColoring.color = outputZone.currentColor;

        orderAmountText.text = "";
        deliveryData = null;

        carTransform.DOMoveX(endPoint.position.x, carSpeed).SetEase(Ease.InOutBack);
        carTransform.DOShakeRotation(2, strength: 5, 10);
    }



}
