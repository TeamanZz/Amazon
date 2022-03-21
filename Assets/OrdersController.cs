using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersController : MonoBehaviour
{
    public List<PurchaseSystem> purchaseSystems = new List<PurchaseSystem>();

    public void Start()
    {
        foreach(var purch in purchaseSystems)
        {
            purch.InitializationObject();
        }
    }
}
