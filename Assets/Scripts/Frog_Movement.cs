using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Movement : Enemy_Movement
{
    
   //Components
    private SpriteRenderer sprite;
    private Animator anim;
    private Rigidbody2D rig_body;

    private bool grounded;
    private bool jump = false;
    private bool currentlyJumping = false;
    private float speed_frog = 0.35f;

    //Coroutine fields
    [SerializeField] private LayerMask ground;
    private Vector3 m_Velocity = Vector3.zero;
    private static readonly System.Random getrandom = new System.Random();
   
    private int GetRandomNumber(int min, int max)
    {
        lock (getrandom) // synchronize
        {
            return getrandom.Next(min, max);
        }
    }

    private void Update()
    {
        if (isGrounded())
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    public override IEnumerator Movement()
    {
        while (true)
        {

            int rnd = GetRandomNumber(1, 4);

            if (rnd == 1 && grounded)
            {
                jump = true;
            }
            
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void FixedUpdate()
    {
        if(currentlyJumping && grounded)
        {
            anim.SetBool("isJumping", false);
            currentlyJumping = false;
        }
        if (jump)
        {
            currentlyJumping = true;
            anim.SetBool("isJumping", true);
            Vector3 targetVelocity = new Vector2(speed_frog * 5f, rig_body.velocity.y + 3f);
            rig_body.velocity = Vector3.Lerp(rig_body.velocity, targetVelocity,1f);
            jump = false;
        }

        
        
    }
    private new void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rig_body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        base.Start();
        
    }
    
    private bool isGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .05f, ground);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        speed_frog = -speed_frog;
        sprite.flipX = !sprite.flipX;
    }

    public override void Die()
    {
        rig_body.bodyType = RigidbodyType2D.Static;
        base.Die();
    }

}
