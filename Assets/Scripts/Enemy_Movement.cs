using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;

    private BoxCollider2D boxCollider;
    private Animator animator;
    private int currentWaypointIndex = 0;
    
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
       
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards
                            (transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
           
        
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.bounds.center.y > boxCollider.bounds.extents.y + boxCollider.bounds.center.y)
        {
            //Trigger death
            animator.SetBool("isDead", true);
            boxCollider.enabled = false;

            //Trigger jump for the Player
            collision.gameObject.GetComponent<Player_Movement>().applyForce = true;
        }
    }
    

}
