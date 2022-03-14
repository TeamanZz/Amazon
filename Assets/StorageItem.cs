using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItem : MonoBehaviour
{
    [Header("Settings")]
    public Renderer currentRender;
    public float defaultScale = 1f;

    [Header("View settings")]
    public static int itemTypeCount = 7;
    public ItemType currentItemType;
    public enum ItemType
    {
        RedBox,
        YellowBox,
        GreenBox,
        //  double
        OrangeBox,
        LimeBox,
        BrownBox,
        //  trible
        GrayBox,
        //  processing
        WasteBox,
        NoType
    }

    public void Start()
    {
        ShowColor();
    }

    [ContextMenu("Show Color")]
    public void ShowColor()
    {
        switch (currentItemType)
        {
            case ItemType.RedBox:
                currentRender.material.color = Color.red;
                break;

            case ItemType.YellowBox:
                currentRender.material.color = Color.yellow;
                break;

            case ItemType.GreenBox:
                currentRender.material.color = Color.green;
                break;

            case ItemType.OrangeBox:
                currentRender.material.color = new Color32(255, 114, 0, 255);
                break;

            case ItemType.LimeBox:
                currentRender.material.color = new Color32(138, 255, 13, 255);
                break;

            case ItemType.BrownBox:
                currentRender.material.color = new Color32(87, 46, 4, 255);
                break;

            case ItemType.GrayBox:
                currentRender.material.color = new Color32(84, 71, 56, 255);
                break;

            case ItemType.WasteBox:
                currentRender.material.color = new Color32(0, 255, 238, 255);
                break;
        }
    }

    public void ReturnScale()
    {
        transform.localScale = Vector3.one * defaultScale;
    }
}
