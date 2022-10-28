using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class Player_Movement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private float speed = 40;
    [SerializeField] public CinemachineVirtualCamera vcamera;

    [SerializeField] private AudioSource audioDeath;

    public bool applyForce;
    private Animator animator;
    private Rigidbody2D rig_body;
    private BoxCollider2D boxCollider;

    private float horizontalMove = 0;
    private bool isJumping = false;
    private bool isCrouching = false;
    private bool isFalling = false;

    public bool isLevelCompleted;

    void Start()
    {
        animator = GetComponent<Animator>();
        rig_body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("isRunning", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && !isCrouching && boxCollider.enabled)
        {
            Jump();
        }
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }

        if(rig_body.velocity.y < -0.01f)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, isCrouching, isJumping);
       
        if (applyForce)
        {
            applyForce = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 800));
            Jump();
        }
        isJumping = false;
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isFalling", isFalling);
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
            if (circleCollider.bounds.center.y < collision.collider.bounds.center.y)
            {
                //Trigger death
                Die();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death") || collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isLevelCompleted)
        {
            animator.SetBool("isDead", true);
            boxCollider.enabled = false;
            circleCollider.enabled = false;
            rig_body.AddForce(new Vector2(0, 500));
            vcamera.GetComponent<CinemachineVirtualCamera>().Follow = null;

            audioDeath.Play();
            Invoke("RestartLevel", 2);
            GetComponent<Player_Movement>().enabled = false;
        }
        
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Jump()
    {
        isJumping = true;
        animator.SetBool("isJumping", isJumping);
    }
}