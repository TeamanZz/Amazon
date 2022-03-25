using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StorageItem : MonoBehaviour
{
    [Header("Settings")]
    public Renderer currentRender;
    public Renderer preItemRender;
    public float defaultScale = 1f;

    [Header("View settings")]
    public static int itemTypeCount = 6;

    public List<Material> materials = new List<Material>();
    public ItemType currentItemType;
    public enum ItemType
    {
        RedBox,
        BlueBox,
        GreenBox,
        YellowBox,
        NoType,
        //
        PreRedBox,
        PreBlueBox,
        PreGreenBox,
        //
        DirtyBarrel,
        CleanBarrel,
        AllBarel
    }

    public GameObject itemBox;
    public GameObject preItemBox;
    //
    public GameObject dirtyBarrel;
    public GameObject clearBarrel;

    [ContextMenu("Coloring")]
    public void Start()
    {
        if (currentItemType != ItemType.DirtyBarrel || currentItemType != ItemType.CleanBarrel)
        {
            dirtyBarrel.SetActive(false);
            clearBarrel.SetActive(false);
            itemBox.SetActive(true);
            preItemBox.SetActive(false);
        }
        else
            itemBox.SetActive(false);

        switch (currentItemType)
        {
            case ItemType.RedBox:
                //currentRender.material.color = Color.red;
                currentRender.material = materials[0];
                itemBox.SetActive(true);
                preItemBox.SetActive(false);
                break;

            case ItemType.BlueBox:
                //currentRender.material.color = Color.blue;
                currentRender.material = materials[1];
                itemBox.SetActive(true);
                preItemBox.SetActive(false);
                break;

            case ItemType.GreenBox:
                //currentRender.material.color = Color.green;
                currentRender.material = materials[2];
                itemBox.SetActive(true);
                preItemBox.SetActive(false);
                break;

            case ItemType.YellowBox:
                //currentRender.material.color = Color.yellow;
                currentRender.material = materials[3];
                itemBox.SetActive(true);
                preItemBox.SetActive(false);
                break;

            case ItemType.DirtyBarrel:
                dirtyBarrel.SetActive(true);
                clearBarrel.SetActive(false);
                preItemBox.SetActive(false);

                itemBox.SetActive(false);
                break;

            case ItemType.CleanBarrel:
                dirtyBarrel.SetActive(false);
                clearBarrel.SetActive(true);
                preItemBox.SetActive(false);

                itemBox.SetActive(false);
                break;

            case ItemType.PreRedBox:
                //currentRender.material.color = Color.yellow;
                preItemRender.material = materials[4];
                itemBox.SetActive(false);
                preItemBox.SetActive(true);
                break;

            case ItemType.PreBlueBox:
                //currentRender.material.color = Color.yellow;
                preItemRender.material = materials[5];
                itemBox.SetActive(false);
                preItemBox.SetActive(true);
                break;

            case ItemType.PreGreenBox:
                //currentRender.material.color = Color.yellow;
                preItemRender.material = materials[6];
                itemBox.SetActive(false);
                preItemBox.SetActive(true);
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
