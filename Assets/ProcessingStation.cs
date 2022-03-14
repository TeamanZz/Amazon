using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProcessingStation : MonoBehaviour
{
    public List<ProcessingOutputZone> processingZones = new List<ProcessingOutputZone>();

    [Header("Data settings")]
    public int maximumAmountCount = 10;
    public int currentAmountCount = 0;

    //public List<int> amounts = new List<int>();
    public int[] amounts = {0,0,0};

    public TextMeshProUGUI redZone;
    public TextMeshProUGUI yellowZone;
    public TextMeshProUGUI greenZone;

    public void Awake()
    {
        foreach (var zone in processingZones)
        {
            zone.parentProcessingStation = this;
        }
        ViewUI();
    }

    public void ViewUI()
    {
        redZone.text = amounts[0].ToString() + " / " + maximumAmountCount.ToString();
        yellowZone.text = amounts[1].ToString() + " / " + maximumAmountCount.ToString();
        greenZone.text = amounts[2].ToString() + " / " + maximumAmountCount.ToString();

    }

    public void CheckProcessing(int colorNumber)
    {
        switch(colorNumber)
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
}
