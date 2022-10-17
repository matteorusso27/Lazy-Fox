using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private float speed = 40;
    
    private Animator animator;
    private Rigidbody2D rig_body;

    private float horizontalMove = 0;
    private bool isJumping = false;
    private bool isCrouching = false;
    private bool isFalling_No_Jump = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rig_body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("isRunning", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            animator.SetBool("isJumping", isJumping);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }
        
        if(rig_body.velocity.y < -0.01f && !isJumping && !isFalling_No_Jump)
        {
            isFalling_No_Jump = true;
            animator.SetBool("isFalling", true);
        }
        else if (rig_body.velocity.y >= 0f && isFalling_No_Jump)
        {
            animator.SetBool("isFalling",false);
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, isCrouching, isJumping);
        isJumping = false;
        animator.SetBool("isJumping", isJumping);
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }
}
