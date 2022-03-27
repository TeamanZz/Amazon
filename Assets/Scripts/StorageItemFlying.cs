using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static StorageItem;

public class StorageItemFlying : MonoBehaviour
{
    [Header("Settings")]
    public Renderer currentRender;
    public Renderer defaultBoxRender;
    public Renderer tvRenderer;
    public Renderer photoRenderer;
    public List<Material> materials = new List<Material>();
    private float time = 0.4f;

    public GameObject itemBox;
    public GameObject tv;
    public GameObject photocamera;
    public GameObject defaultBox;
    //
    public GameObject dirtyBarrel;
    public GameObject clearBarrel;

    public void FlyTo(Vector3 endPoint, Vector3 endRotation, StorageItem.ItemType currentItemType)
    {
        SetColor(currentItemType);
        Debug.Log("Point: " + endPoint.y);
        transform.DOLocalMove(endPoint, time).SetEase(Ease.OutBack);
        Destroy(gameObject, 0.5f);
        transform.DORotate(endRotation, time).SetEase(Ease.OutBack);
        transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), time).SetEase(Ease.OutBack);
    }

    private void SetColor(StorageItem.ItemType currentItemType)
    {
        if (currentItemType != ItemType.DirtyBarrel || currentItemType != ItemType.CleanBarrel)
        {
            dirtyBarrel.SetActive(false);
            clearBarrel.SetActive(false);
            // itemBox.SetActive(true);
            tv.SetActive(false);
        }
        else
            itemBox.SetActive(false);

        switch (currentItemType)
        {
            case ItemType.RedBox:
                //currentRender.material.color = Color.red;
                currentRender.material = materials[0];
                itemBox.SetActive(true);
                tv.SetActive(false);
                break;

            case ItemType.BlueBox:
                //currentRender.material.color = Color.blue;
                currentRender.material = materials[1];
                itemBox.SetActive(true);
                tv.SetActive(false);
                break;

            case ItemType.GreenBox:
                //currentRender.material.color = Color.green;
                currentRender.material = materials[2];
                itemBox.SetActive(true);
                tv.SetActive(false);
                break;

            case ItemType.YellowBox:
                //currentRender.material.color = Color.yellow;
                currentRender.material = materials[3];
                itemBox.SetActive(true);
                tv.SetActive(false);
                break;

            case ItemType.DirtyBarrel:
                dirtyBarrel.SetActive(true);
                clearBarrel.SetActive(false);
                tv.SetActive(false);

                itemBox.SetActive(false);
                break;

            case ItemType.CleanBarrel:
                dirtyBarrel.SetActive(false);
                clearBarrel.SetActive(true);
                tv.SetActive(false);

                itemBox.SetActive(false);
                break;

            case ItemType.Tv:
                tvRenderer.material = materials[4];
                tv.SetActive(true);
                break;

            case ItemType.Photocamera:
                photoRenderer.material = materials[5];
                photocamera.SetActive(true);
                break;

            case ItemType.DefaultBox:
                defaultBoxRender.material = materials[4];
                defaultBox.SetActive(true);
                break;
        }
    }

}