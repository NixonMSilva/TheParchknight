using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hideout : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    private PlayerStatusController playerStatus;
    

    private Animator animator;
    private TooltipController tooltip;

    private Vector3 playerOriginalPosition;

    [SerializeField] private string leaveHideoutText = "Leave";

    private void Awake ()
    {
        player = GameObject.Find("Player");
        playerStatus = player.GetComponent<PlayerStatusController>();
        playerController = player.GetComponent<PlayerController>();

        animator = GetComponent<Animator>();
        tooltip = GetComponent<TooltipController>();
    }

    public void OnHideoutInteraction ()
    {
        if (!playerStatus.isHidden)
        {
            playerStatus.isHidden = true;
            playerController.canMove = false;

            animator.SetBool("isBeingUsed", true);

            player.GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponent<Rigidbody2D>().Sleep();

            playerOriginalPosition = player.transform.position;
            player.GetComponent<Rigidbody2D>().MovePosition(transform.position);

            tooltip.ChangeText(leaveHideoutText);
        }
        else
        {
            playerStatus.isHidden = false;
            playerController.canMove = true;

            animator.SetBool("isBeingUsed", false);

            player.GetComponent<SpriteRenderer>().enabled = true;
            player.GetComponent<Rigidbody2D>().WakeUp();

            player.GetComponent<Rigidbody2D>().MovePosition(playerOriginalPosition);

            tooltip.HideText();
            tooltip.ChangeText(tooltip.GetTooltipBaseText());
        }

    }
}
