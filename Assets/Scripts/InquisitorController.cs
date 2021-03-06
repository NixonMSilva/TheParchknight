using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class InquisitorController : EntityController
{
    // 0 - Patrol | 1 - Chase | 2 - Stop Chase
    private int currentState = 0;

    private bool canChasePlayer = true;
    private bool isChasingPlayer = false;
    private bool canThrowAlert = true;

    private float distanceToPlayer;

    private GameObject player;
    private PlayerStatusController playerStatus;

    [SerializeField] private float maxSpeed = 6f;

    // AI Distance
    [SerializeField] private float followDistance = 1.5f;
    [SerializeField] private float forgetDistance = 3f;

    // Pathfinding
    private AIDestinationSetter destinationSetter;
    private AIPath pathfinder;
    private PatrolController patrol;

    // Movement Control for the Animator
    private float currX, earlyX;
    private float currY, earlyY;

    private Transform currentNode;

    [SerializeField] LayerMask solidBlocks;

    private AudioManager audioManager;

    private new void Awake ()
    {
        base.Awake();
        
        anim = GetComponentInChildren<Animator>();
        pathfinder = GetComponent<AIPath>();
        patrol = GetComponent<PatrolController>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        // Get the patrol nodes
        currentNode = patrol.nodes[patrol.currentNode];

        // Get the player object
        player = GameObject.Find("Player");
        playerStatus = player.GetComponent<PlayerStatusController>();

        // Get the audio manager
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Start ()
    {
        destinationSetter.target = currentNode;

        pathfinder.maxSpeed = speed;

        earlyX = transform.position.x;
        earlyY = transform.position.y;
    }

    private new void Update ()
    {
        // Verifies distance to player and sets the appropriate state
        distanceToPlayer = Vector2.Distance(rb.position, player.transform.position);

        if (currentState == 0)
        {
            if (distanceToPlayer <= followDistance 
                && !TrySeePlayer() 
                && !playerStatus.isHidden)
            {
                currentState = 1;
            }
        }
        else if (currentState == 1)
        {
            if ((distanceToPlayer >= forgetDistance) 
                || (distanceToPlayer <= followDistance 
                && playerStatus.isHidden))
            {
                currentState = 2;
            }
        }
        else if (currentState == 2)
        {
            currentState = 0;
        }

        /*
        if (!isChasingPlayer && distanceToPlayer <= followDistance)
        {
            // Check to see if the player is in range, if it is,
            // cast a ray towards him
            if (!TrySeePlayer())
            {
                // Check if the player isn't occluded by any way in the way
                // If the player is in range, then chase him
                isChasingPlayer = true;
                currentState = 1;

                // TODO: Chase player updates slowly to not recalc every time
            }
            else
            {
                // If he is occluded, then it's business as usual
                currentState = 0;
            }
        }
        else if (isChasingPlayer && distanceToPlayer >= forgetDistance)
        {
            // If chasing the player but the player goes too far, stop chasing the player
            // OR if the player is past the minimum follow distance and is hiding
            isChasingPlayer = false;
            currentState = 2;
        }
        else
        {
            // Default state: patrolling
            currentState = 0;
        } */

        // FSM to decide what to do
        switch (currentState)
        {
            case 0:
                Patrol();
                break;
            case 1:
                ChasePlayer();
                break;
            case 2:
                StopChase();
                currentState = 0;
                break;
        }

        /*
        Debug.Log("Target:" + destinationSetter.target);
        Debug.Log("Target Distance:" + distanceToPlayer); 
        */
    }

    private void FixedUpdate ()
    {
        // Animator processer

        currX = transform.position.x;
        currY = transform.position.y;

        Vector2 speed = new Vector2((currX - earlyX), (currY - earlyY));

        anim.SetFloat("speed_x", speed.x);
        anim.SetFloat("speed_y", speed.y);
        anim.SetFloat("speed", speed.sqrMagnitude);

        facingDir = GetDirection(speed);

        ProcessAnimatorDirection(facingDir);

        earlyX = currX;
        earlyY = currY;

        /*
        Debug.Log("Teste: X = " + speed.x + " | Y = " + speed.y);
        Debug.Log("Teste Dir: " + facingDir); 
        */

    }

    private void Patrol ()
    {
        //pathfinder.canSearch = true;
       
        if (pathfinder.reachedDestination)
        {
            // Set the next node into the destination
            patrol.currentNode = (patrol.currentNode + 1) % patrol.PatrolLenght();
            currentNode = patrol.nodes[patrol.currentNode];
            destinationSetter.target = currentNode;
        }
    }

    private bool TrySeePlayer ()
    {
        // Casts a ray in the direction of the player to see if there isn't a wall in the way
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, distanceToPlayer, solidBlocks);

        //Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.magenta);

        return (hit.collider != null);
    }

    private void ChasePlayer ()
    {
        // pathfinder.canSearch = true;
        if (canThrowAlert)
        {
            audioManager.PlaySound("InquisitorAlert");
            canThrowAlert = false;
        }
        destinationSetter.target = player.transform;
        pathfinder.maxSpeed = maxSpeed;
    }

    private void StopChase ()
    {
        //pathfinder.canSearch = false;
        canThrowAlert = true;
        destinationSetter.target = currentNode;
        pathfinder.maxSpeed = speed;
    }
}
