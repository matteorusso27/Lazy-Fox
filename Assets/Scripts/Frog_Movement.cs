using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Movement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private LayerMask ground;
    private BoxCollider2D boxCollider;
    private SpriteRenderer sprite;
    private Rigidbody2D rig_body;
    private Animator anim;
    private int currentWaypointIndex = 0;

    private bool isJumping;
    private static readonly System.Random getrandom = new System.Random();

    public static int GetRandomNumber(int min, int max)
    {
        lock (getrandom) // synchronize
        {
            return getrandom.Next(min, max);
        }
    }
    Coroutine Movement_Coroutine;

    IEnumerator Movement()
    {
        while (true)
        {
            
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
                
                sprite.flipX = !sprite.flipX;
                
            }

            int rnd = GetRandomNumber(1, 2);

            if (isGrounded())
            {
                isJumping = true;
                rig_body.AddForce(new Vector2(0, 400));

                //transform.position = Vector2.MoveTowards
                //                (transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

            }
            else
            {
                isJumping = false;
            }


            yield return new WaitForSeconds(3f);
        }
    }
    private void FixedUpdate()
    {
        if (isJumping)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
    }
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rig_body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Movement_Coroutine = StartCoroutine(Movement());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.collider.bounds.center.y > boxCollider.bounds.center.y + boxCollider.bounds.extents.y)
            {
                //Apply Force upwards to Player
                collision.gameObject.GetComponent<Player_Movement>().applyForce = true;

                //Trigger Death
                Die();
            }
        }
    }

    private void Die()
    {
        //Change into Death animation and destroy
        GetComponent<Animator>().SetBool("isDead", true);
        boxCollider.enabled = false;
        StopCoroutine(Movement_Coroutine);
        Destroy(gameObject, 2f);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, ground);
    }
}
