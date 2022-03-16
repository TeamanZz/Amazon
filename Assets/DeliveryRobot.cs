using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliveryRobot : MonoBehaviour
{
    [Header("Main settings")]
    public NavMeshAgent agent;

    [Header("Charge settings")]
    public Transform chargingStation;
    public float rangeOfTheCharging = 0.25f;

    public float currentChargeLevel = 100;

    [Header("Order settings")]

    [Header("Robot backpack")]
    public List<StorageItem> storageItems = new List<StorageItem>();
    public int backpackCapacity = 3;


    [Header("State settings")]
    public RobotState currentRobotState;
    public RobotState lastrobotState;
    public enum RobotState
    {
        WaitingForOrder,
        FulfillsTheOrder,
        Charging,
        Discharged
    }

    public bool isActive = false;

    public void Start()
    {
        currentRobotState = RobotState.WaitingForOrder;
        
    }

    public void Update()
    {
        if (isActive == false)
            return;

        CheckChargeLevel();
        switch (currentRobotState)
        {
            case RobotState.WaitingForOrder:
                break;

            case RobotState.FulfillsTheOrder:
                break;

            case RobotState.Discharged:
                ReturnToTheStation();
                break;
        }

        if (lastrobotState != currentRobotState)
            lastrobotState = currentRobotState;
    }
    public void CheckChargeLevel()
    {
        if (currentRobotState == RobotState.Charging)
            return;

        if(currentChargeLevel <= 5)
        {
            currentRobotState = RobotState.Discharged;
        }
    }

    public void ReturnToTheStation()
    {
        float distance = Vector3.Distance(transform.position, chargingStation.position);
        
        if(distance <= rangeOfTheCharging)
        {
            currentRobotState = RobotState.Charging;
            if(currentChargeLevel < 100)
            {
                
            }
        }
    }
}
