using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpable_ground;

    private float dirX = 0f;
    [SerializeField] private float move_speed = 7f;
    [SerializeField] private float jump_force = 14f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jump_soundeffect;
    

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Hard coding the specific key
        // if (Input.GetKeyDown("space")) 
        // {
        //     rb.velocity = new Vector3(0, 14f, 0);
        // }
        dirX = Input.GetAxisRaw("Horizontal");        //GetAxis = slides the character after moving left and right
                                                      //GetAxisRaw = stops the character after moving left and right
        
        rb.velocity = new Vector2(dirX * move_speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded()) 
        {
            jump_soundeffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jump_force);
        }

        UpdateAnimationState();
    }
    

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpable_ground);
    }
}
