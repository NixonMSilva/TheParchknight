using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    private PlayerStatusController status;

    [HideInInspector] public bool canMove = true;

    [SerializeField] private Transform currentReset;

    public bool hasPotion = false;

    private new void Awake ()
    {
        base.Awake();

        status = GetComponent<PlayerStatusController>();
    }

    private void Start ()
    {
        InputHandler.current.OnPotionKeyPressed += UsePotion;
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
        status.isHidden = true;
        StartCoroutine(ResetWait(1f));
    }

    public void UpdateCaptureBar (float value)
    {
        UI_controller.UpdateCaptureBar(value);
    }

    public void EnableCaptureBar ()
    {
        UI_controller.ShowCaptureBar();
    }

    public void DisableCaptureBar ()
    {
        UI_controller.HideCaptureBar();
    }

    public void SetPotion (bool value) => hasPotion = value;

    public bool GetPotion () => hasPotion;

    private void UsePotion (object sender, EventArgs e)
    {
        if (hasPotion)
        {
            hasPotion = false;
            status.UsePotion();
        }
    }

    private void OnDestroy ()
    {
        InputHandler.current.OnPotionKeyPressed -= UsePotion;
    }

    IEnumerator ResetWait (float duration)
    {
        UI_controller.FadeOutScreen(1f);
        Debug.Log("Teste: A!");
        yield return new WaitForSeconds(duration);
        Debug.Log("Teste: B!");
        transform.position = currentReset.position;
        yield return new WaitForSeconds(duration);
        Debug.Log("Teste: C!");
        UI_controller.FadeInScreen(1f);
        status.isHidden = false;
    }
}
