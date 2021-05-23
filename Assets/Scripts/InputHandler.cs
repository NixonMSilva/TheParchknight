using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private PlayerController playerController;

    private Vector2 movementInput = Vector2.zero;

    private void Awake ()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update ()
    {
        // Movement input (Left/Right/Up/Down)
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate ()
    {
        // Perform move on fixed amount of frames per secon
        if (movementInput != Vector2.zero)
        {
            playerController.MovePlayer(movementInput);
        }
        else
        {
            playerController.StopPlayer();
        }
    }

}
