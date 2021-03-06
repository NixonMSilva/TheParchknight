using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hideout : MonoBehaviour
{
    private AudioManager audioManager;

    private GameObject player;
    private PlayerController playerController;
    private PlayerStatusController playerStatus;

    private Animator animator;
    private TooltipController tooltip;

    private Vector3 playerOriginalPosition;

    private BoxCollider2D hideoutCollider;

    [SerializeField] private string leaveHideoutText = "Leave";

    enum HideoutType
    {
        Wardrobe,
        Bush,
        Sewer
    };

    [SerializeField] private HideoutType type;

    private void Awake ()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        player = GameObject.Find("Player");
        playerStatus = player.GetComponent<PlayerStatusController>();
        playerController = player.GetComponent<PlayerController>();

        animator = GetComponent<Animator>();
        tooltip = GetComponent<TooltipController>();
        hideoutCollider = GetComponent<BoxCollider2D>();
    }

    public void OnHideoutInteraction ()
    {
        audioManager.PlaySound(type.ToString());
        if (!playerStatus.isHidden)
        {
            playerStatus.isHidden = true;
            playerController.canMove = false;

            playerOriginalPosition = player.transform.position;

            animator.SetBool("isBeingUsed", true);

            player.GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            hideoutCollider.enabled = false;

            player.transform.position = transform.position;

            StartCoroutine(HideoutAutoCorrector(0.05f));

            tooltip.ChangeText(leaveHideoutText);
            tooltip.ShowText();
        }
        else
        {

            playerStatus.isHidden = false;
            playerController.canMove = true;

            animator.SetBool("isBeingUsed", false);

            player.GetComponent<SpriteRenderer>().enabled = true;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            hideoutCollider.enabled = true;

            player.transform.position = playerOriginalPosition;

            tooltip.HideText();
            tooltip.ChangeText(tooltip.GetTooltipBaseText());
        }

    }

    IEnumerator HideoutAutoCorrector (float correctionTime)
    {
        yield return new WaitForSeconds(correctionTime);
        player.transform.position = transform.position;
    }
}
