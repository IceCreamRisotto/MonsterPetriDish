using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{

    private bool jumping = false;//跳躍中
    private bool isGround = false;//在不在地板

    public float jumpHeight = 450f;
    private Rigidbody2D player;
    private Animator animator;

    private void Awake()
    {
        player =  GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        SetJumpState();
        StateMachine();
    }

    public void Jump()
    {
        if (!isGround || player.velocity.y != 0) { return; }

        player.AddForce(Vector3.up * jumpHeight);

    }

    void SetJumpState()
    {
        if (player.velocity.y > 0.1)
        {
            isGround = false;
            jumping = true;
        }
        else if (jumping)
        {
            jumping = false;
        }
    }

    void StateMachine()
    {
        animator.SetBool("Jumping", jumping);
        animator.SetBool("Ground", isGround);
    }

    private void OnCollisionEnter2D(Collision2D co)
    {
        if (co.gameObject.tag == "Ground" && co.contacts[0].normal == Vector2.up)
        {
            isGround = true;
        }
    }
}
