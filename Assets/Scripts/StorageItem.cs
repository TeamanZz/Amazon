using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StorageItem : MonoBehaviour
{
    [Header("Settings")]
    public Renderer currentRender;
    public float defaultScale = 1f;

    [Header("View settings")]
    public static int itemTypeCount = 3;

    public List<Material> materials = new List<Material>();
    public ItemType currentItemType;
    public enum ItemType
    {
        RedBox,
        BlueBox,
        GreenBox,
        YellowBox,
        NoType
    }

    [ContextMenu("Coloring")]
    public void Start()
    {
        switch (currentItemType)
        {
            case ItemType.RedBox:
                //currentRender.material.color = Color.red;
                currentRender.material = materials[0];
                break;

            case ItemType.BlueBox:
                //currentRender.material.color = Color.blue;
                currentRender.material = materials[1];
                break;

            case ItemType.GreenBox:
                //currentRender.material.color = Color.green;
                currentRender.material = materials[2];
                break;

            case ItemType.YellowBox:
                //currentRender.material.color = Color.yellow;
                currentRender.material = materials[3];
                break;
        }

        //StartCoroutine(IEEnableRendererOnStart());
    }

    //private IEnumerator IEEnableRendererOnStart()
    //{
    //    transform.localScale = Vector3.zero;
    //    yield return new WaitForSeconds(0.4f);
    //    transform.DOScale(Vector3.one * defaultScale, 0.1f);
    //}

    public void ReturnScale()
    {
        transform.localScale = Vector3.one * defaultScale;
    }
}
