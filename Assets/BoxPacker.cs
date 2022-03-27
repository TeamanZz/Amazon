using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxPacker : MonoBehaviour
{
    public BoxPackerZone inputZone;
    public BoxPackerZone outputZone;

    public TextMeshProUGUI inputText;
    public TextMeshProUGUI outputCount;

    [Header("Connect settings")]
    public Transform target;
    public float distanceToCentr = 1.25f;

    public bool zoneIsActive = false;
    public bool targetInPlace = false;

    public Image fillingImage;
    public float scaleFactor;

    public float reloadTime = 0.5f;
    public float currentTime;

    [Header("Data settings")]
    public int maximumInputCount = 10;
    public int maximumOutputCount = 10;

    public Material conveyorMat;

    public ParticleSystem particle;

    public List<StorageItem.ItemType> itemInTypeStack = new List<StorageItem.ItemType>();
    public List<StorageItem.ItemType> itemOutTypeStack = new List<StorageItem.ItemType>();

    public void Awake()
    {
        zoneIsActive = true;

        inputZone.parentProcessingStation = this;
        outputZone.parentProcessingStation = this;
        ViewUI();

        fillingImage.fillAmount = 0;
        scaleFactor = 1 / reloadTime * Time.deltaTime;
        currentTime = reloadTime;
        ViewUI();
    }

    public void ViewUI()
    {
        inputText.text = itemInTypeStack.Count.ToString() + " / " + maximumInputCount.ToString();
        outputCount.text = itemOutTypeStack.Count.ToString() + " / " + maximumOutputCount.ToString();
    }

    public void ReceivingToBackpack()
    {
        if (itemOutTypeStack.Count < 1)
            return;

        //outputZone.ReceivingItem(itemOutTypeStack[0]);
        itemOutTypeStack.Remove(itemOutTypeStack[0]);
        ViewUI();
    }

    public void SendToPacker(StorageItem.ItemType itemType)
    {
        itemInTypeStack.Add(itemType);
        ViewUI();
    }

    public void FixedUpdate()
    {
        conveyorMat.mainTextureOffset = new Vector2(conveyorMat.mainTextureOffset.x, conveyorMat.mainTextureOffset.y - 0.025f);

        if (zoneIsActive == false)
            return;

        if (itemInTypeStack.Count < 1 || itemOutTypeStack.Count > maximumOutputCount - 1)
            return;

        if (currentTime <= 0)
        {
            fillingImage.fillAmount = 0;
            currentTime = reloadTime;
            particle.Play();
            switch (itemInTypeStack[0])
            {
                case StorageItem.ItemType.Photocamera:
                    //if (!CharacterBag.characterBag.FindType(StorageItem.ItemType.PreBlueBox))
                    //    break;

                    itemOutTypeStack.Add(StorageItem.ItemType.DefaultBox);
                    break;

                case StorageItem.ItemType.Tv:
                    //if (!CharacterBag.characterBag.FindType(StorageItem.ItemType.PreGreenBox))
                    //    break;

                    itemOutTypeStack.Add(StorageItem.ItemType.DefaultBox);
                    break;
            }

            itemInTypeStack.RemoveAt(0);
        }
        else
        {
            currentTime -= Time.deltaTime;
            fillingImage.fillAmount += scaleFactor;
        }

        ViewUI();
    }
}
