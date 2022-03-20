using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StorageItemFlying : MonoBehaviour
{
    public Renderer currentRender;
    public List<Material> materials = new List<Material>();
    private float time = 0.4f;
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
        switch (currentItemType)
        {
            case StorageItem.ItemType.RedBox:
                //currentRender.material.color = Color.red;
                currentRender.material = materials[0];
                break;

            case StorageItem.ItemType.BlueBox:
                //currentRender.material.color = Color.blue;
                currentRender.material = materials[1];
                break;

            case StorageItem.ItemType.GreenBox:
                //currentRender.material.color = Color.green;
                currentRender.material = materials[2];
                break;

            case StorageItem.ItemType.YellowBox:
                //currentRender.material.color = Color.yellow;
                currentRender.material = materials[3];
                break;
        }
    }

}