using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private AudioManager audioManager;

    private UIController UI_Controller;

    private Victory victory;
    private bool hasShownVictory = false;

    [SerializeField] private List<CollectiblePickup> _coinList;
    [SerializeField] private List<ScrollPickup> _scrollList;

    [SerializeField] private string scrollTypeName = "Scroll";
    [SerializeField] private string coinTypeName = "Coin";

    public int coinCount = 0;
    public int scrollCount = 0;

    [SerializeField] private List<bool> _scrollObtained;

    private void Awake ()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        UI_Controller = GameObject.Find("UI").GetComponent<UIController>();

        foreach (CollectiblePickup collectible in _coinList)
        {
            collectible.OnCoinPickup += HandleCoinPickup;
        }

        foreach (ScrollPickup collectible in _scrollList)
        {
            collectible.OnScrollPickup += HandleScrollPickup;
        }

        _scrollObtained.Add(false);
        foreach (ScrollPickup item in _scrollList)
        {
            _scrollObtained.Add(false);
        }

        victory = GetComponent<Victory>();
    }

    private void Update ()
    {
        if (scrollCount >= GetMaxScrolls() && !hasShownVictory)
        {
            victory.DrawVictory();
            hasShownVictory = true;
        }
    }

    private void HandleScrollPickup (CollectiblePickup obj)
    {
        audioManager.PlaySound("ScrollPickup");
        int scrollId = obj.GetComponent<ScrollPickup>().scroll.scrollId;
        _scrollObtained[scrollId] = true;
        UI_Controller.UpdateScrollCount(++scrollCount);
    }

    private void HandleCoinPickup (CollectiblePickup obj)
    {
        audioManager.PlaySound("CoinPickup");
        ChangeCoinCount(1);
    }

    public void ChangeCoinCount (int delta)
    {
        UI_Controller.UpdateCoinCount(coinCount + delta);
        coinCount += delta;
    }

    public void DrawScrollMenu ()
    {
        UI_Controller.DrawScrollMenu(_scrollObtained, _scrollList);
    }

    public int GetMaxScrolls ()
    {
        return _scrollList.Count;
    }

    [ContextMenu ("Autofill Collectibles")]
    void AutofillCollectibles ()
    {

        _coinList = GetComponentsInChildren<CollectiblePickup>()
            .Where(t => t.name.Contains("Coin"))
            .ToList();

        _scrollList = GetComponentsInChildren<ScrollPickup>()
            .Where(t => t.name.Contains("Scroll"))
            .ToList();
    }
}
