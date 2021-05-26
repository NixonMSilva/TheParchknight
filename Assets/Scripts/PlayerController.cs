using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    private PlayerStatusController status;

    [HideInInspector] public bool canMove = true;

    [SerializeField] private Transform currentReset;

    private new void Awake ()
    {
        base.Awake();

        status = GetComponent<PlayerStatusController>();
    }

    public void MovePlayer (Vector2 movement)
    {
        if (canMove)
        {
            PerformMove(movement, true);
        }
    }

    public void StopPlayer ()
    {
        StopBody();
    }

    public void SetResetPosition (Vector3 position) => currentReset.position = position;

    public void ResetPlayerPositionOnMap ()
    {
        UI_controller.FadeOutScreen();

        status.isHidden = true;

        rb.MovePosition(currentReset.position);

        status.isHidden = false;

        UI_controller.FadeInScreen();
    }

}
