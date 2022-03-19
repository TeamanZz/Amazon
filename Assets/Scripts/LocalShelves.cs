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

    public Vector2Int shelfDimensions = new Vector2Int(3, 3);

    public float verticalDistance = 1f;
    public float horizontalDistance = 1f;

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
        for (int y = 0; y < shelfDimensions.y; y++)
        {
            for (int x = 0; x < shelfDimensions.y; x++)
            {
                Vector3 newPosition = new Vector3(zeroDebugPoint.position.x + (x * horizontalDistance), zeroDebugPoint.position.y + (y * verticalDistance), zeroDebugPoint.position.z);
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
