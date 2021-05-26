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

    protected UIController UI_controller;

    protected void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        UI_controller = GameObject.Find("UI").GetComponent<UIController>();
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
}
