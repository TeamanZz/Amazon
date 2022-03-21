using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrdersController : MonoBehaviour
{
    public static OrdersController ordersController;
    public List<PurchaseSystem> purchaseSystems = new List<PurchaseSystem>();

    public Animator rewardAnimator;
    public float timeToClosedRewardPanel = 3f;
    public TextMeshProUGUI rewardText;

    public void Awake()
    {
        ordersController = this;
    }

    public void Start()
    {
        foreach(var purch in purchaseSystems)
        {
            purch.Awake();
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            purchaseSystems[0].InitializationObject();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            purchaseSystems[1].InitializationObject();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            purchaseSystems[2].InitializationObject();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            purchaseSystems[3].InitializationObject();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            purchaseSystems[4].InitializationObject();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            purchaseSystems[5].InitializationObject();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            purchaseSystems[6].InitializationObject();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            purchaseSystems[7].InitializationObject();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            purchaseSystems[8].InitializationObject();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            purchaseSystems[9].InitializationObject();
        }
    }

    public void CheckSystem(PurchaseSystem currentPurchaseSystem)
    {
        MoneyController.moneyController.currentMoney += currentPurchaseSystem.currentObjectPrice;
        rewardText.text = "+" + currentPurchaseSystem.currentObjectPrice.ToString() + "$";
        rewardAnimator.SetBool("Open", true);
        Invoke("ClosedRewardPanel", timeToClosedRewardPanel);
    }

    public void ClosedRewardPanel()
    {
        rewardAnimator.SetBool("Open", false);
        rewardText.text = "";
    }
}
