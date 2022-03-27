using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderView : MonoBehaviour
{
    public List<Sprite> iconsSprites = new List<Sprite>();
    public StorageItem.ItemType currentDeployType;

    [Header("UI View")]
    public Image iconImage;
    public TextMeshProUGUI currentAmountText;

    public DeliveryMachine deliveryMachineParent;
    public void Initialization(DeliveryMachine deliveryMachine)
    {
        deliveryMachine.orderViewChild = this;
        deliveryMachineParent = deliveryMachine;

        switch (currentDeployType)
        {
            case StorageItem.ItemType.RedBox:
                iconImage.sprite = iconsSprites[0];
                break;

            case StorageItem.ItemType.BlueBox:
                iconImage.sprite = iconsSprites[3];
                break;

            case StorageItem.ItemType.GreenBox:
                iconImage.sprite = iconsSprites[2];
                break;

            case StorageItem.ItemType.YellowBox:
                iconImage.sprite = iconsSprites[3];
                break;

            case StorageItem.ItemType.DirtyBarrel:
                iconImage.sprite = iconsSprites[4];
                break;

            case StorageItem.ItemType.CleanBarrel:
                iconImage.sprite = iconsSprites[5];
                break;

            case StorageItem.ItemType.DefaultBox:
                iconImage.sprite = iconsSprites[3];
                break;
        }
    }

    public void UpdateUI(int currentAmount, int amountCount)
    {
        currentAmountText.text = deliveryMachineParent.deliveryData.currentAmount.ToString() + " / " + deliveryMachineParent.deliveryData.correctAmount.ToString();
    }

}
