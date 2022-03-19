using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAt : MonoBehaviour
{
    public Transform target;
    public bool isActive = true;

    public void Update()
    {
        if (!isActive)
            return;

        transform.LookAt(target);
    }
}
