using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickup : MonoBehaviour
{
    public event Action<CollectiblePickup> OnCoinPickup;

    public event Action<CollectiblePickup> OnScrollPickup;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.name.Contains("Coin"))
            {
                OnCoinPickup?.Invoke(this);
            }
            else if (gameObject.name.Contains("Scroll"))
            {
                OnScrollPickup?.Invoke(this);
            }
            gameObject.SetActive(false);
        }
    }
}
