using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{
    [SerializeField] private Transform destination;

    private GameObject player;
    
    private void Awake ()
    {
        player = GameObject.Find("Player");
    }

    public void TeleportPlayer ()
    {
        Rigidbody2D playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerRigidBody.MovePosition(destination.position);
    }
}
