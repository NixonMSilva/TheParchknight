using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler current;

    private PlayerController playerController;

    private Vector2 movementInput = Vector2.zero;

    [SerializeField] public KeyCode interactionKey = KeyCode.F;

    [SerializeField] public KeyCode potionUseKey = KeyCode.G;

    public event EventHandler OnInteractionPressed;

    public event EventHandler OnPotionKeyPressed;

    private void Awake ()
    {
        current = this;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update ()
    {
        // Movement input (Left/Right/Up/Down)
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        // If you can press the key shown in the tooltip
        if (Input.GetKeyDown(interactionKey))
        {
            //Debug.Log("Test 2");
            OnInteractionPressed?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetKeyDown(potionUseKey))
        {
            OnPotionKeyPressed?.Invoke(this, EventArgs.Empty);
        }
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
