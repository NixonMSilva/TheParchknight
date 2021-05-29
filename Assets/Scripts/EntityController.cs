using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] protected float speed = 3f;

    [SerializeField] private float dampeningTime = 0.1f;

    private Vector2 currentSpeed = Vector2.zero;

    protected Rigidbody2D rb;

    protected Animator anim;

    // 0 - Down | 1 - Left | 2 - Up | 3 - Right
    protected int facingDir = 0;

    protected UIController UI_controller;

    protected void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        UI_controller = GameObject.Find("UI").GetComponent<UIController>();
        anim = GetComponent<Animator>();

        // Get component for the enemy
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    protected void Update ()
    {
        Vector2 velocity = rb.velocity;

        anim.SetFloat("speed_x", velocity.x);
        anim.SetFloat("speed_y", velocity.y);
        anim.SetFloat("speed", velocity.sqrMagnitude);

        facingDir = GetDirection(rb.velocity);

        ProcessAnimatorDirection(facingDir);
    }

    protected void PerformMove (Vector2 movement, bool isNormalized)
    {
        if (isNormalized)
            movement.Normalize();

        movement *= speed;
        rb.velocity = movement;
        //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    protected void StopBody ()
    {
        rb.velocity = Vector2.SmoothDamp(rb.velocity, Vector2.zero, ref currentSpeed, dampeningTime);
    }

    protected int GetDirection (Vector2 movement)
    {
        /*
        if (movement.x > 0f)
        {
            return 3;
        }
        else if (movement.y > 0f)
        {
            return 2;
        }
        else if (movement.x < 0f)
        {
            return 1;
        }
        else if (movement.y < 0f)
        {
            return 0;
        }
        return 0;
        */

        
        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            if (movement.x > 0f)
            {
                return 3;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            if (movement.y > 0f)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
    }

    protected void ProcessAnimatorDirection (int direction)
    {
        switch (direction)
        {
            case 0:
                anim.SetFloat("direction", 0f);
                break;
            case 1:
                anim.SetFloat("direction", 1f);
                break;
            case 2:
                anim.SetFloat("direction", 2f);
                break;
            case 3:
                anim.SetFloat("direction", 3f);
                break;
        }
    }
}
