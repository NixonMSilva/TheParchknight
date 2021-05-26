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

    private PlayerController playerController;
    private Collider2D playerCollider;

    private List<Collider2D> contacts;

    private ContactFilter2D contactFilter;
    [SerializeField] private LayerMask enemyMask;

    private void Awake ()
    {
        playerController = GetComponent<PlayerController>();
        playerCollider = GetComponent<Collider2D>();

        contacts = new List<Collider2D>();

        ResetCaptureTimer();
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

        if (playerCollider.IsTouchingLayers(enemyMask))
        {
            isBeingCaptured = true;
        }
        else
        {
            isBeingCaptured = false;
        }

        Debug.Log("Teste3: " + isBeingCaptured);
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
            // Decreases the counter
            captureTimeout -= Time.deltaTime;

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
    }

    private void ResetCaptureTimer ()
    {
        captureTimeout = captureTimeoutTime;
    }
}
