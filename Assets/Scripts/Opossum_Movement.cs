using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum_Movement : Enemy_Movement
{
    private SpriteRenderer sprite;

    public override IEnumerator Movement()
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
            transform.position = Vector2.MoveTowards
                                (transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
            yield return null;
        }
    }
    private new void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        base.Start();
    }

}
