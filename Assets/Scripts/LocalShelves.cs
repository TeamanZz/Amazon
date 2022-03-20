using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalShelves : MonoBehaviour
{
    [Header("Main settings")]
    public ShelvesController shelvesController;

    [Header("Storage Setup")]
    public Transform zeroDebugPoint;
    public float itemScaleOnShelf = 1f;

    public Vector3Int shelfDimensions = new Vector3Int(3, 3, 0);

    public float verticalDistance = 1f;
    public float horizontalDistance = 1f;
    public float backDistance = 0f;

    public List<Vector3> itemsPositions = new List<Vector3>();

    public void InitializationParent(ShelvesController newShelvesController)
    {
        shelvesController = newShelvesController;
        
        CheckPositions();
        shelvesController.InitializationChildShalves(itemsPositions);
    }

    [ContextMenu("Check Positions")]
    public void CheckPositions()
    {
        itemsPositions.Clear();
        for (int y = 0; y < shelfDimensions.y; y++){
            for (int x = 0; x < shelfDimensions.x; x++)
            {
                
                int z = 0;
                Vector3 newPosition;
                //for (y = 0; y < shelfDimensions.y - 1; y++)
                //{
                //    newPosition = new Vector3(zeroDebugPoint.position.x + (x * horizontalDistance), zeroDebugPoint.position.y + (y * verticalDistance), zeroDebugPoint.position.z + (z * backDistance));
                //    itemsPositions.Add(newPosition);
                //}

                for (z = 0; z < shelfDimensions.z - 1; z++)
                {
                    newPosition = new Vector3(zeroDebugPoint.position.x + (x * horizontalDistance), zeroDebugPoint.position.y + (y * verticalDistance), zeroDebugPoint.position.z + (z * backDistance));
                    itemsPositions.Add(newPosition);
                }

                newPosition = new Vector3(zeroDebugPoint.position.x + (x * horizontalDistance), zeroDebugPoint.position.y + (y * verticalDistance), zeroDebugPoint.position.z + (z * backDistance));
                itemsPositions.Add(newPosition);
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (itemsPositions.Count <= 0)
            return;

        foreach (var item in itemsPositions)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(item, Vector3.one * itemScaleOnShelf);


        }
    }
}
