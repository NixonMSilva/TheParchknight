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

    private string tooltipKey = "";

    private bool canPressButton = false;

    private void Awake ()
    {
        UI_Controller = GameObject.Find("UI").GetComponent<UIController>();
    }

    private void Start ()
    {
        tooltipKey = "[" + InputHandler.current.interactionKey.ToString().ToUpper() + "]";
        InputHandler.current.OnInteractionPressed += ButtonPressed;    

    }

    private void Update ()
    {

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
            canPressButton = true;
            UI_Controller.DrawTooltip(tooltipBaseText + " " + tooltipKey);
            // ShowTooltip
        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPressButton = false;
            UI_Controller.HideTooltip();
            // Hide tooltip

        }
    }

    private void OnDestroy ()
    {
        InputHandler.current.OnInteractionPressed -= ButtonPressed;
    }

    public void ChangeText (string text)
    {
        UI_Controller.DrawTooltip(text + " " + tooltipKey);
    }

    public void HideText ()
    {
        UI_Controller.HideTooltip();
    }

    public string GetTooltipBaseText () => tooltipBaseText;
}
