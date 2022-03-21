using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseSystem : MonoBehaviour
{
    public GameObject purchasedObject;
    public GameObject constructionZone;
    public int currentObjectPrice = 100;

    public GameObject zone;

    [Header("Zone Settings")]
    public bool zoneIsActive = false;
    public bool targetInPlace = false;

    public TextMeshProUGUI priceText;

    public Transform target;
    public float distanceToCentr = 1.25f;

    [ContextMenu("Awake")]
    public void Awake()
    {
        constructionZone.SetActive(false);

        zone.SetActive(false);
        purchasedObject.SetActive(false);
        zoneIsActive = false;
    }

    [ContextMenu("Initialization")]
    public void InitializationObject()
    {
        zone.SetActive(true);
        purchasedObject.SetActive(false);
        priceText.text = currentObjectPrice.ToString() + "$";
        zoneIsActive = true;
    }

    public void Update()
    {
        if (zoneIsActive == false)
            return;

        float distance = Vector3.Distance(zone.transform.position, target.position);
        if (distance < distanceToCentr)
        {
            targetInPlace = true;
        }
        else
        {
            targetInPlace = false;
        }

        if (MoneyController.moneyController.currentMoney > currentObjectPrice && targetInPlace == true)
        {
            BuyAnObject();
        }
    }

    public void BuyAnObject()
    {
        MoneyController.moneyController.currentMoney -= currentObjectPrice;
        MoneyController.moneyController.UpdateUI();

        constructionZone.SetActive(false);
        zone.SetActive(false);
        purchasedObject.SetActive(true);

        enabled = false;
    }

    public void SendCurrentState()
    {
        OrdersController.ordersController.CheckSystem(this);
    }
}
