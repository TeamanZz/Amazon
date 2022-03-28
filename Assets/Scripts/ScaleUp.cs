using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement playerMovement;
        if (other.TryGetComponent<PlayerMovement>(out playerMovement))
        {
            transform.DOScale(0.022f, 0.3f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement playerMovement;
        if (other.TryGetComponent<PlayerMovement>(out playerMovement))
        {
            transform.DOScale(0.02f, 0.3f);
        }
    }
}