using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static GameEvent current;

    private void Awake ()
    {
        current = this;
    }

    private float coinCount = 0;

    public event Action<int> onCoinPickup;

    public void OnCoinPickup (int value)
    {
        onCoinPickup?.Invoke(value);
    }
}
