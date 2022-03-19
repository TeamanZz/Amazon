using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderViewController : MonoBehaviour
{
    public static OrderViewController viewController;
    public List<OrderView> orderViews = new List<OrderView>();

    public GridLayoutGroup group;
    public OrderView orderPrefab;
    
    public void Awake()
    {
        viewController = this;    
    }

    public void AddPanel(DeliveryMachine deliveryMachine, StorageItem.ItemType orderType)
    {
        OrderView newOrder = Instantiate(orderPrefab, group.transform);
        newOrder.transform.parent = group.transform;
        newOrder.currentDeployType = orderType;

        newOrder.Initialization(deliveryMachine);
        newOrder.UpdateUI(deliveryMachine.deliveryData.currentAmount, deliveryMachine.deliveryData.correctAmount);
        orderViews.Add(newOrder);

        //return newOrder;
    }

    public void RemovePanel(OrderView orderView)
    {
        orderViews.Remove(orderView);
        Destroy(orderView.gameObject);
    }
}
