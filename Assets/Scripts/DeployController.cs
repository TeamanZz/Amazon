using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployController : MonoBehaviour
{
    public List<ControlOutputZone> outputZone = new List<ControlOutputZone>();

    public bool allowShipping = false;
    public void Update()
    {
        if (allowShipping == false)
            return;


    }


}
