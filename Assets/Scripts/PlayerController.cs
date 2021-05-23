using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    PlayerStatusController Status;

    private new void Awake ()
    {
        base.Awake();

        Status = GetComponent<PlayerStatusController>();
    }

    public void MovePlayer (Vector2 movement)
    {
        PerformMove(movement, true);
    }

    public void StopPlayer ()
    {
        StopBody();
    }
}
