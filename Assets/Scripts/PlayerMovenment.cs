using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovenment : MonoBehaviour
{
    private Rigidbody2D rb;
    private float dirX;
    private BoxCollider2D boxCollider2D;
    private Animator animator;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource JumpAudio;

    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovenmentStare { idle, running, jumping, falling }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        if (Login.loginModel.positionX != "")
        {
            var posX = float.Parse(Login.loginModel.positionX);
            var posY = float.Parse(Login.loginModel.positionY);
            var posZ = float.Parse(Login.loginModel.positionZ);
            transform.position = new Vector3(posX+1f, posY, posZ);
        }
        Debug.Log(Login.loginModel.positionX);
    }

    // Update is called once per frame
    void Update()
    {
        //Cach 1
        //if (Input.GetKeyDown("space"))
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, 14f);
        //}
        //if (Input.GetKey("a"))
        //{
        //    rb.velocity = new Vector2(-7f, rb.velocity.y);
        //}
        //else if (Input.GetKey("d"))
        //{
        //    rb.velocity = new Vector2(7f, rb.velocity.y);
        //}
        //Cach 2
        if (GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
        {
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && isGrounder())
            {
                JumpAudio.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            updateAnimation();
        }

    }

    private void updateAnimation()
    {
        MovenmentStare stare;

        if (dirX > 0f)
        {
            stare = MovenmentStare.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            stare = MovenmentStare.running;
            sprite.flipX = true;
        }
        else
        {
            stare = MovenmentStare.idle;
        }
        if (rb.velocity.y > .1f)
        {
            stare = MovenmentStare.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            stare = MovenmentStare.falling;
        }
        animator.SetInteger("stare", (int)stare);
    }
    private bool isGrounder()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
