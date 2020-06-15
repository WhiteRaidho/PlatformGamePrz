using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public FixedJoystick joystick;

    float horizontalMove = 0f;
    bool jump = false;
    bool onGround = false;

    public float runSpeed = 40f;

    public bool canMove = true;

    private void Update()
    {
        if (canMove)
        {
            horizontalMove = (Input.GetAxisRaw("Horizontal") + joystick.Horizontal) * runSpeed;

            if (Input.GetButtonDown("Jump") && onGround)
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
        } else
        {
            horizontalMove = 0f;
            jump = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        onGround = true;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void Jump()
    {
        jump = true;
    }
}
