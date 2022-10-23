using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [SerializeField] protected GameObject[] waypoints;
    [SerializeField] protected float speed = 2.5f;
    protected BoxCollider2D boxCollider;
    protected int currentWaypointIndex = 0;

    protected Coroutine Movement_Coroutine;

    public virtual IEnumerator Movement()
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
            }
            transform.position = Vector2.MoveTowards
                                (transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
            yield return null;
        }
    }
    protected void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Movement_Coroutine = StartCoroutine(Movement());
    }
    protected void OnCollisionEnter2D(Collision2D collision)
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

    protected void Die()
    {
        //Change into Death animation and destroy
        GetComponent<Animator>().SetBool("isDead", true);
        boxCollider.enabled = false;
        StopCoroutine(Movement_Coroutine);
        Destroy(gameObject, 2f);
    }
}
