using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryMap : MonoBehaviour
{
    [SerializeField] private Transform retryPoint;

    // Start is called before the first frame update
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SetResetPosition(retryPoint.position);
        }
    }
}