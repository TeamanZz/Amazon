using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyController : MonoBehaviour
{
    [Header("Main Settings")]
    public static MoneyController moneyController;

    public int currentMoney = 500;
    [Header("View Settings")]
    public TextMeshProUGUI currentMoneyText;

    public void Awake()
    {
        moneyController = this;
        UpdateUI();
    }

    public void UpdateUI()
    {
        currentMoneyText.text = currentMoney.ToString() + "$";
    }
}
