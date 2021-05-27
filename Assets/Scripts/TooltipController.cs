using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TooltipController : MonoBehaviour
{
    private UIController UI_Controller;

    public UnityEvent OnPressEvent;

    [SerializeField] private string tooltipBaseText = "<default tooltip>";

    private string tooltipCurrentText = "";

    [SerializeField] private string tooltipKey = "";

    private bool canPressButton = false;

    private void Awake ()
    {
        tooltipCurrentText = tooltipBaseText;
        UI_Controller = GameObject.Find("UI").GetComponent<UIController>();
    }

    private void Start ()
    {
        tooltipKey = "[" + InputHandler.current.interactionKey.ToString().ToUpper() + "]";
        InputHandler.current.OnInteractionPressed += ButtonPressed;    
    }

    private void ButtonPressed (object sender, EventArgs e)
    {
        if (canPressButton)
        {
            OnPressEvent.Invoke();
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ShowTooltip
            canPressButton = true;
            ShowText();
        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Hide tooltip
            canPressButton = false;
            HideText();
        }
    }

    private void OnDestroy ()
    {
        InputHandler.current.OnInteractionPressed -= ButtonPressed;
    }

    public void ChangeText (string text)
    {
        tooltipCurrentText = text;
        // ShowText();
    }

    public void ShowText ()
    {
        UI_Controller.DrawTooltip(tooltipCurrentText + " " + tooltipKey);
    }

    public void ShowTextWihtoutKey ()
    {
        UI_Controller.DrawTooltip(tooltipCurrentText);
    }

    public void HideText ()
    {
        UI_Controller.HideTooltip();
    }

    public string GetTooltipBaseText () => tooltipBaseText;

    public string GetTooltipCurrentText () => tooltipCurrentText;
}
