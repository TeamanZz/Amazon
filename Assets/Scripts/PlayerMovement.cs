using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Sirenix.OdinInspector;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private FloatingJoystick floatingJoystick;

    private Animator animator;
    public Rigidbody playerRB;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        MoveAndRotatePlayer();
    }

    private void MoveAndRotatePlayer()
    {
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        if (Mathf.Abs(floatingJoystick.Horizontal) >= 0.025f || Mathf.Abs(floatingJoystick.Vertical) >= 0.025f)
        {
            playerRB.velocity = (direction.normalized * speed);
            var angle = Mathf.Atan2(-floatingJoystick.Horizontal, floatingJoystick.Vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, -angle, 0)), Time.deltaTime * 15);

            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
            playerRB.velocity /= 1.1f;
        }
    }
}