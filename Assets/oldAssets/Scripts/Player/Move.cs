using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
{

    private PlayerInput input;
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerStats stats;
    private Vector2 move;

    private void Start()
    {
        stats = GetComponent<Player>().Stats;
        input = InputManager.Input;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }



    private void Update()
    {       
        move = move_calcualte();
        Reflect();
    }

    private void FixedUpdate()
    {
        rb.velocity += move * Time.fixedDeltaTime;

        if (rb.velocity.magnitude > stats.MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * stats.MaxSpeed;
        }
    }


    public bool faceRight = true;
    private void Reflect()
    {
        if ((move.x > 0 && !faceRight) || (move.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }

    private Vector2 move_calcualte()
    {
        move = input.Player.Move.ReadValue<Vector2>();
        float moveX = move.x * stats.Acceleration;
        float moveY = move.y * stats.Acceleration;

        anim.SetFloat("moveX", Mathf.Abs(move.x));
        anim.SetFloat("moveY", move.y);

        if (moveX == 0 && rb.velocity.x != 0)
        {
            moveX = Mathf.Sign(rb.velocity.x) * -stats.Friction;
            if (Mathf.Abs(moveX * Time.fixedDeltaTime) > Mathf.Abs(rb.velocity.x))
            {
                moveX = 0;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        if (moveY == 0 && rb.velocity.y != 0)
        {
            moveY = Mathf.Sign(rb.velocity.y) * -stats.Friction;
            if (Mathf.Abs(moveY * Time.fixedDeltaTime) > Mathf.Abs(rb.velocity.y))
            {
                moveY = 0;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
        return new Vector2(moveX, moveY);
    }
}

