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
        public StorageItem.ItemType orderType = StorageItem.ItemType.RedBox;
        public int correctAmount;
        public int currentAmount;
    }

    [Header("Data settings")]
    public CurrentDeliveryData deliveryData;
    public int minAmountCount = 5;
    public int maxAmountCount = 20;

    [Header("View settings")]
    // public Image orderImageColoring;
    public TextMeshProUGUI orderAmountText;
    public OrderView orderViewChild;

    [Header("Move Settings")]
    public Transform deployPoint;
    public Transform endPoint;
    public Transform carTransform;

    public float carSpeed = 5f;

    public Transform leftDoor;
    public Transform rightDoor;

    [ContextMenu("Moved To Deploy")]
    public void MoveToGates()
    {
        carTransform.DOMoveX(deployPoint.position.x, carSpeed).SetEase(Ease.InOutBack);
        carTransform.DOShakeRotation(2, strength: 5, 10);

        Invoke("OpenDoor", 4f);
        ReceivingAnOrder();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            MoveToGates();
        }
    }

    [ContextMenu("Receiving Order")]
    public void ReceivingAnOrder()
    {
        if (orderViewChild != null)
        {
            OrderViewController.viewController.RemovePanel(orderViewChild);
            return;
        }
        deliveryData = null;

        deliveryData = new CurrentDeliveryData();
        deliveryData.orderType = (StorageItem.ItemType)UnityEngine.Random.Range(0, StorageItem.itemTypeCount + 1);
        deliveryData.correctAmount = UnityEngine.Random.Range(minAmountCount, maxAmountCount);
        deliveryData.currentAmount = 0;

        outputZone.ChangeZoneSendType(deliveryData.orderType);
        // orderImageColoring.color = outputZone.currentColor;

        outputZone.zoneIsActive = true;
    }

    public void OpenDoor()
    {
        if (orderViewChild != null)
            return;

        Debug.Log("Open Door");

        //  0 -> 150
        Vector3 leftVector = new Vector3(0, 120, 0);
        leftDoor.DOLocalRotate(leftVector, 1.2f).SetEase(Ease.OutBack);// = leftVector;
        //  0 -> -150
        Vector3 rightVector = new Vector3(0, -120, 0);
        rightDoor.DOLocalRotate(rightVector, 0.8f).SetEase(Ease.OutBack);
        //leftDoor.rotation = Quaternion.Lerp(Quaternion.EulerAngles(Vector3.zero), Quaternion.EulerAngles(leftVector), 5f); 

        ViewUI();
        OrderViewController.viewController.AddPanel(this, deliveryData.orderType);
    }

    public void ClosedDoor()
    {
        Vector3 leftVector = new Vector3(0, 0, 0);
        leftDoor.DOLocalRotate(leftVector, 1.2f).SetEase(Ease.OutBack);
        //  0 -> -150
        Vector3 rightVector = new Vector3(0, 0, 0);
        rightDoor.DOLocalRotate(rightVector, 0.8f).SetEase(Ease.OutBack);
    }

    public void ViewUI()
    {
        orderAmountText.text = deliveryData.currentAmount.ToString() + " / " + deliveryData.correctAmount.ToString();
        if (orderViewChild != null)
            orderViewChild.UpdateUI(deliveryData.currentAmount, deliveryData.correctAmount);
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
        // orderImageColoring.color = outputZone.currentColor;

        orderAmountText.text = "";
        deliveryData = null;

        ClosedDoor();
        OrderViewController.viewController.RemovePanel(orderViewChild);

        carTransform.DOMoveX(endPoint.position.x, carSpeed).SetEase(Ease.InOutBack);
        carTransform.DOShakeRotation(2, strength: 5, 10);
    }
}