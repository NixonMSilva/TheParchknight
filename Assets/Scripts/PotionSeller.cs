using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSeller : MonoBehaviour
{
    [SerializeField] private int potionPrice;

    private PlayerController player;
    private CollectibleController collectible;
    private UIController UIcontroller;

    private TooltipController tooltip;

    private AudioManager audioManager;

    private string baseTooltipText;

    [SerializeField] private float buyCooldown = 2f;
    [SerializeField] private string buyFailureText = "";
    [SerializeField] private string buyConfirmedText = "";
    [SerializeField] private string buyAlreadyOwnText = "";

    private float temporaryCountdown;
    private bool isTemporary = false;

    // public event Action<PotionSeller, int> OnPotionPurchase;

    private void Awake ()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        collectible = GameObject.Find("Collectibles").GetComponent<CollectibleController>();
        UIcontroller = GameObject.Find("UI").GetComponent<UIController>();

        tooltip = GetComponent<TooltipController>();

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Start ()
    {
        baseTooltipText = tooltip.GetTooltipBaseText() + " (" + potionPrice.ToString() + " coins)";
        tooltip.ChangeText(baseTooltipText);
    }

    private void Update ()
    { 
        if (isTemporary)
        {
            if (temporaryCountdown <= 0f)
            {
                isTemporary = false;
                tooltip.ChangeText(baseTooltipText);
            }
            temporaryCountdown -= Time.deltaTime;
        }
    }

    public void BuyPotion ()
    {
        if (player.hasPotion)
        {
            // The player already has a potion
            ChangeTextTemporarily(buyAlreadyOwnText);
            audioManager.PlaySound("PotionError");
        }
        else
        {
            if (collectible.coinCount >= potionPrice)
            {
                // If the player can buy the potion
                player.hasPotion = true;

                audioManager.PlaySound("PotionBuy");

                UIcontroller.SetPotionIconStatus(true);

                ChangeTextTemporarily(buyConfirmedText);

                collectible.ChangeCoinCount(-potionPrice);
            }
            else
            {
                // If the player doesn't have enough coins
                ChangeTextTemporarily(buyFailureText);
                audioManager.PlaySound("PotionError");
            }
        }
    }

    private void ChangeTextTemporarily (string text)
    {
        temporaryCountdown = buyCooldown;
        tooltip.ChangeText(text);
        tooltip.ShowText();
        isTemporary = true;
    }

    public int GetPotionPrice () => potionPrice;
}
