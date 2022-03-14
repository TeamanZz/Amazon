using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProcessingStation : MonoBehaviour
{
    public TypeOfProcessing currentStationType;
    public enum TypeOfProcessing
    {
        DoubleMixing,
        TripleMixing,
        Cleaning
    }
    [Header("Double Settings")]
    public GameObject doubleBody;
    public List<ControlOutputZone> doubleInputZone;

    public int correctAmount = 10;

    public ControlOutputZone outputZone;

    [Header("Triple Settings")]
    public GameObject tripleBody;
    public List<ControlOutputZone> tripleInputZone;
    //public ControlOutputZone tripleOutput;

    public int currentAmountZoneOne = 0;
    public int currentAmountZoneTwo = 0;
    public int currentAmountZoneThree = 0;
    public TextMeshProUGUI amounZoneOne;
    public TextMeshProUGUI amounZoneTwo;
    public TextMeshProUGUI amounZoneThree;

    public int maximumOutputMaterial = 10;
    public int currentOutputMaterial = 0;

    public void Awake()
    {
        ShowStationType();
    }

    [ContextMenu("Show Station")]
    public void ShowStationType()
    {
        currentAmountZoneOne = 0;
        currentAmountZoneTwo = 0;
        currentAmountZoneThree = 0;

        doubleBody.SetActive(false);

        switch (currentStationType)
        {
            case TypeOfProcessing.DoubleMixing:
                doubleBody.SetActive(true);
                CheckDoubleInput(doubleInputZone);
                break;

            case TypeOfProcessing.TripleMixing:
                CheckDoubleInput(tripleInputZone);
                break;

            case TypeOfProcessing.Cleaning:
                break;
        }
        ViewOnUI();
    }

    public void CheckDoubleInput(List<ControlOutputZone> inputZones)
    {
        switch (inputZones.Count)
        {
            case 2:
                #region
                if (inputZones[0].currentDeployType == StorageItem.ItemType.RedBox || inputZones[1].currentDeployType == StorageItem.ItemType.RedBox)
                {
                    if (inputZones[0].currentDeployType == StorageItem.ItemType.YellowBox || inputZones[1].currentDeployType == StorageItem.ItemType.YellowBox)
                        outputZone.currentDeployType = StorageItem.ItemType.OrangeBox;
                    else if (inputZones[0].currentDeployType == StorageItem.ItemType.GreenBox || inputZones[1].currentDeployType == StorageItem.ItemType.GreenBox)
                        outputZone.currentDeployType = StorageItem.ItemType.BrownBox;
                }
                if (inputZones[0].currentDeployType == StorageItem.ItemType.YellowBox || inputZones[1].currentDeployType == StorageItem.ItemType.YellowBox)
                    if (inputZones[0].currentDeployType == StorageItem.ItemType.GreenBox || inputZones[1].currentDeployType == StorageItem.ItemType.GreenBox)
                        outputZone.currentDeployType = StorageItem.ItemType.LimeBox;
                #endregion
                break;

            case 3:
                outputZone.currentDeployType = StorageItem.ItemType.GrayBox;
                break;
        }


    }


    public void ReceivingItem(StorageItem newItemPrefab, StorageItem.ItemType type)
    {
        switch (currentStationType)
        {
            case TypeOfProcessing.DoubleMixing:
                if (currentAmountZoneOne > 0 && currentAmountZoneTwo > 0 && currentOutputMaterial < maximumOutputMaterial)
                {
                    currentAmountZoneOne -= 1;
                    currentAmountZoneTwo -= 1;

                    currentOutputMaterial += 1;
                }
                else
                    return;
                break;

            case TypeOfProcessing.TripleMixing:
                if (currentAmountZoneOne > 0 && currentAmountZoneTwo > 0 && currentAmountZoneThree > 0 && currentOutputMaterial < maximumOutputMaterial)
                {
                    currentAmountZoneOne -= 1;
                    currentAmountZoneTwo -= 1;
                    currentAmountZoneThree -= 1;

                    currentOutputMaterial += 1;
                }
                else
                    return;
                break;
        }

        StorageItem currentItem = Instantiate(newItemPrefab);
        currentItem.currentItemType = outputZone.currentDeployType;
        currentItem.ShowColor();

        CharacterBag.characterBag.ReceivingItem(currentItem, type);
        ViewOnUI();

    }

    //  in
    public void SendItem(StorageItem.ItemType type, ControlOutputZone currentZone)
    {
        StorageItem removItem = null;

       
            foreach (var item in CharacterBag.characterBag.storageItems)
            {

            switch (currentStationType)
            {
                case TypeOfProcessing.DoubleMixing:
                    switch (item.currentItemType)
                    {
                        case StorageItem.ItemType.RedBox:
                            CharacterBag.characterBag.SendItem(StorageItem.ItemType.RedBox, currentZone);
                            currentAmountZoneOne += 1;
                            break;

                        case StorageItem.ItemType.YellowBox:
                            CharacterBag.characterBag.SendItem(StorageItem.ItemType.YellowBox, currentZone);
                            currentAmountZoneTwo += 1;
                            break;

                        case StorageItem.ItemType.GreenBox:
                            CharacterBag.characterBag.SendItem(StorageItem.ItemType.GreenBox, currentZone);
                            currentAmountZoneThree += 1;
                            break;


                    }
                    break;
            }
        }



        ViewOnUI();
    }

    public void ViewOnUI()
    {
        switch (currentStationType)
        {
            case TypeOfProcessing.DoubleMixing:
                amounZoneOne.text = currentAmountZoneOne.ToString() + " / " + correctAmount;
                amounZoneTwo.text = currentAmountZoneTwo.ToString() + " / " + correctAmount;
                amounZoneThree.transform.parent.transform.gameObject.SetActive(false);
                //amounZoneThree.text = currentAmountZoneThree.ToString() + " / " + correctAmount;
                break;

            case TypeOfProcessing.TripleMixing:
                amounZoneOne.text = currentAmountZoneOne.ToString() + " / " + correctAmount;
                amounZoneTwo.text = currentAmountZoneTwo.ToString() + " / " + correctAmount;
                amounZoneThree.transform.parent.transform.gameObject.SetActive(true);
                amounZoneThree.text = currentAmountZoneThree.ToString() + " / " + correctAmount;
                break;
        }
    }
}
