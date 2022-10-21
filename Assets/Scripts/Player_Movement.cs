﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player_Movement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private CircleCollider2D boxCollider;
    [SerializeField] private float speed = 40;

    public bool applyForce;
    private Animator animator;
    private Rigidbody2D rig_body;

    private float horizontalMove = 0;
    private bool isJumping = false;
    private bool isCrouching = false;

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
       
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, isCrouching, isJumping);
       
        if (applyForce)
        {
            applyForce = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 800));
            isJumping = true;
            animator.SetBool("isJumping", isJumping);
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (boxCollider.bounds.center.y < collision.collider.bounds.center.y)
            {
                //Trigger death
                Die();
            }
        }
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        rig_body.bodyType = RigidbodyType2D.Static;
        Invoke("RestartLevel", 2);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
