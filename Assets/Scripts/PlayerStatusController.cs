using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public bool isHidden;

    public bool isBeingCaptured;

    [SerializeField] private float captureTimeoutTime = 2f;
    private float captureTimeout;

    private void Update ()
    {
        Debug.Log("Teste 2: Why");
        if (isBeingCaptured)
        {
            Debug.Log("Teste 3: Começou Bixo");
            if (captureTimeout <= 0f)
            {
                captureTimeout = captureTimeoutTime;

                Debug.Log("Teste 4: Capturado");

                // Player gets captured
                GetComponent<PlayerController>().ResetPlayerPositionOnMap();
            }
            captureTimeout -= Time.deltaTime;
        }
    }
}
