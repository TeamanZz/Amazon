using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarrelProcessingStation : MonoBehaviour
{
    public BarrelProcessingOutputZone barrelProcessingZone;
    public BarrelProcessingOutputZone outputZone;

    public TextMeshProUGUI dirtyBarell;
    public TextMeshProUGUI clearBarrel;

    [Header("Connect settings")]
    public bool zoneIsActive = false;

    public Image fillingImage;
    public float scaleFactor;

    public float reloadTime = 0.5f;
    public float currentTime;

    [Header("View settings")]
    public GameObject dirtyObject;
    public GameObject clearObject;

    [Header("Data settings")]
    public int maximumAmountCount = 1;
    public int currentAmountCount = 0;

    public int maxOutputCount = 1;
    public int outputItemsCount = 0;

    public float currentFilling = 0;
    public void Awake()
    {
        zoneIsActive = true;
        barrelProcessingZone.parentBarrelProcessingStation = this;
        outputZone.parentBarrelProcessingStation = this;

        ViewUI();

        fillingImage.fillAmount = 0;
        scaleFactor = 1f / reloadTime * 0.03f;

        currentTime = reloadTime;

        dirtyObject.SetActive(false);
        CheckOutObject();
    }

    public void ViewUI()
    {
        dirtyBarell.text = currentAmountCount.ToString() + " / " + maximumAmountCount.ToString();
        clearBarrel.text = outputItemsCount.ToString() + " / " + maxOutputCount.ToString();
    }

    public void CheckOutObject()
    {
        clearObject.SetActive(false);
        clearObject.transform.localScale = new Vector3(1f, 0f, 1f);
        dirtyObject.transform.localScale = new Vector3(1f, 0f, 1f);
    }

    public void CheckProcessing()
    {
        if (currentAmountCount >= maximumAmountCount)
            return;

        scaleFactor = 1f / reloadTime * 0.03f;

        currentAmountCount += 1;

        ViewUI();
    }

    public void Update()
    {
        if (zoneIsActive == false)
            return;

        if (currentAmountCount <= 0 && currentAmountCount >  maximumAmountCount)
            return;

        if (outputItemsCount < 0 || outputItemsCount >= maxOutputCount)
            return;

        if (currentAmountCount != 1)
            return;

        if (currentTime <= 0 && outputItemsCount < 1)
        {
            currentFilling = 0;
            fillingImage.fillAmount = currentFilling;

            currentTime = reloadTime;

            clearObject.transform.localScale = new Vector3(1f, 1f, 1f);
            dirtyObject.transform.localScale = new Vector3(1f, 0f, 1f);

            dirtyObject.SetActive(false);

            currentAmountCount -= 1;
            outputItemsCount += 1;
        }
        else
        {
            dirtyObject.SetActive(true);

            currentTime -= Time.deltaTime;

            currentFilling += scaleFactor;
            fillingImage.fillAmount = currentFilling;

            clearObject.SetActive(true);
            dirtyObject.SetActive(true);

            currentFilling = Mathf.Clamp(currentFilling, 0f, 1f);
            dirtyObject.transform.localScale = new Vector3(1f, 1 - currentFilling, 1f);
            clearObject.transform.localScale = new Vector3(1f, currentFilling, 1f);
        }

        ViewUI();
    }
}
