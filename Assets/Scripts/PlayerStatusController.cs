using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public bool isHidden;

    public bool isBeingCaptured;

    [SerializeField] private bool isCaptureBarActive = false;

    [SerializeField] private float captureTimeoutTime = 2f;
    [SerializeField] private float captureTimeout;
    [SerializeField] private float timeoutPercentage = 0f;

    [SerializeField] private float potionTimeoutTime = 2f;
    [SerializeField] private float potionTimeout;
    private bool isInvisible;

    private PlayerController playerController;
    private Collider2D playerCollider;

    private List<Collider2D> contacts;
    [SerializeField] private int noContacts;

    [SerializeField] private ContactFilter2D contactFilter;
    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private Color invisibleColor;

    private void Awake ()
    {
        playerController = GetComponent<PlayerController>();
        playerCollider = GetComponent<Collider2D>();

        contacts = new List<Collider2D>();

        ResetCaptureTimer();
        ResetInvisibilityTimer();
    }

    private void Update ()
    {
        // Check if the player isn't touching any enemy collider
        //playerCollider.OverlapCollider(contactFilter.NoFilter(), contacts);

        /*
        if (playerCollider.OverlapCollider(contactFilter.NoFilter(), contacts) > 0)
        {
            foreach (Collider2D obj in contacts)
            {
                Debug.Log("Teste: Executed!");
                if (obj.CompareTag("Inquisitor"))
                {
                    isBeingCaptured = true;
                    break;
                }
            }
        }
        else
        {
            isBeingCaptured = false;
        } */

        // Player capture

        if (playerCollider.IsTouchingLayers(enemyMask) && !isHidden)
        {
            isBeingCaptured = true;
        }
        else
        {
            isBeingCaptured = false;
        }

        if (isBeingCaptured)
        {
            if (!isCaptureBarActive)
            {
                playerController.EnableCaptureBar();
                isCaptureBarActive = true;
            }

            if (captureTimeout <= 0f)
            {
                // Resets the counter
                captureTimeout = captureTimeoutTime;
                isBeingCaptured = false;

                // Player gets captured
                playerController.ResetPlayerPositionOnMap();
            }

            noContacts = playerCollider.OverlapCollider(contactFilter, contacts);
            captureTimeout -= Time.deltaTime * noContacts;

            // Sets the capture bar base on the percentage values
            timeoutPercentage = (captureTimeoutTime - captureTimeout) / (captureTimeoutTime);
            playerController.UpdateCaptureBar(timeoutPercentage);
        }
        else
        {
            if (isCaptureBarActive)
            {
                isCaptureBarActive = false;
                playerController.DisableCaptureBar();
            }

            if (captureTimeout != captureTimeoutTime)
            {
                ResetCaptureTimer();
            }
        }

        // Invisibility timeout
        if (isInvisible)
        {
            if (potionTimeout <= 0)
            {
                ResetInvisibilityTimer();
                SetPlayerSpriteColor(new Color(1f, 1f, 1f, 1f));
                isInvisible = isHidden = false;
            }
            potionTimeout -= Time.deltaTime;
        }
    }

    private void ResetCaptureTimer ()
    {
        captureTimeout = captureTimeoutTime;
    }

    private void ResetInvisibilityTimer ()
    {
        potionTimeout = potionTimeoutTime;
    }

    public void UsePotion ()
    {
        isInvisible = isHidden = true;
        SetPlayerSpriteColor(invisibleColor);
    }

    private void SetPlayerSpriteColor (Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
