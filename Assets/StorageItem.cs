using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItem : MonoBehaviour
{
    [Header("Settings")]
    public Renderer currentRender;
    public float defaultScale = 1f;

    [Header("View settings")]
    public static int itemTypeCount = 3;
    public ItemType currentItemType;
    public enum ItemType
    {
        RedBox,
        YellowBox,
        GreenBox,
        GrayBox,
        NoType
    }

    public void Start()
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

            case ItemType.GrayBox:
                currentRender.material.color = Color.gray;
                break;
        }

    }

    public void ReturnScale()
    {
        transform.localScale = Vector3.one * defaultScale;
    }
}
