using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [Header("Main Settings")]
    public Vector2 offsetScale = new Vector2(1.5f, 1f);

    public float currentOffsetPosition = 0;
    public float offsetMoveSpeed = 1;
    public float offsetMoveTime = 3f;

    public float maxTapeOffset = 6f;

    public Material currentMaterial;

    [Header("Item Move Settings")]
    public Transform startPoint;
    public Transform endPoint;

    public float offsetToItemMove = 1.5f;
    public List<Transform> itemsPoints = new List<Transform>();

    public int pointsCount = 5;
    public List<float> itemsPointsPositionsX = new List<float>();
    public float itemScale = 1.5f;

    public bool isActive = false;
    public bool sendToBacpack = false;

    [Header("Child Settings")]
    public ConveyorOutputZone conveyorOutputZone;
    public StorageItem storageItem;

    public void Start()
    {
        storageItem.currentItemType = conveyorOutputZone.currentDeployType;
        storageItem.Start();

        itemsPointsPositionsX.Clear();
        for (int i = 0; i < pointsCount; i++)
        {
            float newPosition = startPoint.localPosition.z - currentOffsetPosition + (i * offsetToItemMove);
            itemsPointsPositionsX.Add(newPosition);

            itemsPoints[i].localPosition = new Vector3(0, startPoint.position.y, itemsPointsPositionsX[i]);
        }
    }

    public Vector2 tapeDirection = new Vector2(0, 1);
    public void FixedUpdate()
    {
        if (isActive)
        {
            float direction;

            if (sendToBacpack == true)
            {
                direction = offsetMoveSpeed / offsetMoveTime * Time.deltaTime;
            }
            else
                direction = 0;

            currentOffsetPosition += direction;

            for (int i = 0; i < itemsPointsPositionsX.Count; i++)
            {
                itemsPointsPositionsX[i] -= direction;

                if (itemsPointsPositionsX[i] < endPoint.localPosition.z)
                {
                    if (sendToBacpack == true)
                    {
                        SendItemToBackpack();

                        itemsPointsPositionsX[i] = startPoint.localPosition.z;
                    }
                }
            }

            if (currentOffsetPosition > maxTapeOffset)
                currentOffsetPosition = 0;

            for (int i = 0; i < itemsPoints.Count; i++)
            {
                //itemsPoints[i].position = new Vector3(startPoint.position.x, startPoint.position.y, startPoint.localPosition.z - currentOffsetPosition + (i * offsetToItemMove));
                //    itemsPoints[i].position = new Vector3(startPoint.position.x, startPoint.position.y, startPoint.position.z - currentOffsetPosition + (i * offsetToItemMove));
                itemsPoints[i].localPosition = new Vector3(0, startPoint.position.y, itemsPointsPositionsX[i]);
            }

            currentMaterial.mainTextureOffset = new Vector2(tapeDirection.x * currentOffsetPosition, tapeDirection.y * currentOffsetPosition);
        }
    }

    public void SendItemToBackpack()
    {
        conveyorOutputZone.ReceivingItem();
        
        //AddItemOnTape();
    }

    //public void AddItemOnTape()
    //{

    //}

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < itemsPoints.Count; i++)
        {
            Gizmos.DrawWireCube(itemsPoints[i].position, Vector3.one * itemScale);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startPoint.position, Vector3.one * itemScale);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(endPoint.position, Vector3.one * itemScale);


    }
}
