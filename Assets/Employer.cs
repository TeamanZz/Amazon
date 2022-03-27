using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Employer : MonoBehaviour
{
    public Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        rb.DOMove(new Vector3(-20, 0, -11), 15).SetEase(Ease.Linear);
    }

    private void FixedUpdate()
    {
    }
}
