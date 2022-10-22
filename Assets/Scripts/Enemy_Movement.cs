using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2.5f;
    private BoxCollider2D boxCollider;
    private SpriteRenderer sprite;
    private int currentWaypointIndex = 0;

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
                if (CompareTag("Opossum"))
                {
                    sprite.flipX = !sprite.flipX;
                }
            }
            transform.position = Vector2.MoveTowards
                                (transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
            yield return null;
        }
    }
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
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
}
