using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private AudioManager audioManager;

    private GameObject player;
    
    private void Awake ()
    {
        player = GameObject.Find("Player");
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void TeleportPlayer ()
    {
        audioManager.PlaySound("MapChange");
        player.transform.position = destination.transform.position;
        // Rigidbody2D playerRigidBody = player.GetComponent<Rigidbody2D>();
        // playerRigidBody.MovePosition(destination.position);
    }
}
