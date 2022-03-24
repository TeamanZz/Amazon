using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProcessingStation : MonoBehaviour
{
    public List<ProcessingOutputZone> processingZones = new List<ProcessingOutputZone>();
    public ProcessingOutputZone outputZone;

    [Header("Data settings")]
    public int maximumAmountCount = 10;
    public int currentAmountCount = 0;

    //public List<int> amounts = new List<int>();
    public int[] amounts = { 0, 0, 0 };

    public TextMeshProUGUI redZone;
    public TextMeshProUGUI yellowZone;
    public TextMeshProUGUI greenZone;

    public TextMeshProUGUI outputCount;

    [Header("Connect settings")]
    public Transform target;
    public float distanceToCentr = 1.25f;

    public bool zoneIsActive = false;
    public bool targetInPlace = false;

    public Color currentColor = Color.gray;
    public Image centrPoint;

    public Image fillingImage;
    public float scaleFactor;

    public float reloadTime = 0.5f;
    public float currentTime;

    public int maxOutputCount = 15;
    public int outputItemsCount = 0;
    public GameObject particle;
    public void Awake()
    {
        zoneIsActive = true;

        foreach (var zone in processingZones)
        {
            zone.parentProcessingStation = this;
        }
        outputZone.parentProcessingStation = this;
        ViewUI();

        fillingImage.fillAmount = 0;
        scaleFactor = 1 /* 1f*/ / reloadTime * 0.0065f;
        currentTime = reloadTime;
    }

    public void ViewUI()
    {
        redZone.text = amounts[2].ToString() + " / " + maximumAmountCount.ToString();
        yellowZone.text = amounts[1].ToString() + " / " + maximumAmountCount.ToString();
        greenZone.text = amounts[0].ToString() + " / " + maximumAmountCount.ToString();

        outputCount.text = outputItemsCount.ToString() + " / " + maxOutputCount.ToString();
    }

    public void CheckProcessing(int colorNumber)
    {
        scaleFactor = 1 /* 1f*/ / reloadTime * 0.028f;

        switch (colorNumber)
        {
            case 0:
                amounts[0] += 1;
                break;

            case 1:
                amounts[1] += 1;
                break;

            case 2:
                amounts[2] += 1;
                break;
        }

        Debug.Log(amounts[0] + " | " + amounts[1] + " | " + amounts[2]);
        ViewUI();
    }

    public void FixedUpdate()
    {
        if (zoneIsActive == false)
            return;


        float distance = Vector3.Distance(centrPoint.transform.position, target.position);
        if (distance < distanceToCentr)
            targetInPlace = true;
        else
            targetInPlace = false;

        if ((amounts[0] <= 0 || amounts[1] <= 0 || amounts[2] <= 0) || outputItemsCount >= maxOutputCount)
            return;

        centrPoint.color = currentColor;
        Debug.DrawLine(centrPoint.transform.position, target.position, currentColor);

        if (!targetInPlace)
            return;

        if (currentTime <= 0)
        {
            fillingImage.fillAmount = 0;
            currentTime = reloadTime;
            //if (amounts[0] > 0 && amounts[1] > 0 && amounts[2] > 0) //|| outputItemsCount >= maxOutputCount)
            //{
            amounts[2] -= 1;
            amounts[1] -= 1;
            amounts[0] -= 1;

            outputItemsCount += 1;
            //}
            particle.SetActive(true);
        }
        else
        {
            currentTime -= Time.deltaTime;
            fillingImage.fillAmount += scaleFactor;
        }

        ViewUI();
    }
}
