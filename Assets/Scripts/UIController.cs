using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private TextMeshProUGUI tooltipText;

    private TextMeshProUGUI coinCount;

    private TextMeshProUGUI scrollCount;

    private GameObject scrollMenuPanel;

    private GameObject scrollMenuOpenButton;

    [SerializeField] private List<GameObject> _scrollMenuItems;

    private string defaultTooltipText;

    private void Awake ()
    {
        tooltipText = GameObject.Find("TooltipText").GetComponent<TextMeshProUGUI>();
        coinCount = GameObject.Find("CoinCount").GetComponent<TextMeshProUGUI>();
        scrollCount = GameObject.Find("ScrollCount").GetComponent<TextMeshProUGUI>();

        scrollMenuOpenButton = GameObject.Find("ScrollMenuButton");

        scrollMenuPanel = GameObject.Find("ScrollViewPanel");

        defaultTooltipText = tooltipText.text;

        HideTooltip();
        HideScrollMenu();
        IntializeCounters();
    }

    private void IntializeCounters ()
    {
        UpdateCoinCount(0);
        UpdateScrollCount(0);
    }

    public void DrawTooltip (string text)
    {
        tooltipText.text = text;
        tooltipText.gameObject.SetActive(true);
    }

    public void HideTooltip ()
    {
        tooltipText.text = defaultTooltipText;
        tooltipText.gameObject.SetActive(false);
    }

    public void UpdateCoinCount (int value)
    {
        coinCount.text = value.ToString();
    }

    public void UpdateScrollCount (int value)
    {
        scrollCount.text = value.ToString();
    }

    public void DrawScrollMenu (List<bool> _scrollsObtained, List<ScrollPickup> _scrolls)
    {
        scrollMenuOpenButton.SetActive(false);
        scrollMenuPanel.SetActive(true);
        for (int i = 0; i < _scrollsObtained.Count; ++i)
        {
            if (_scrollsObtained[i])
            {
                DrawScroll(i, _scrolls);
            }
        }
    }

    private void DrawScroll (int index, List<ScrollPickup> _scrolls)
    {
        Debug.Log(_scrollMenuItems[index].gameObject.name);
        _scrollMenuItems[index - 1].GetComponent<Image>().sprite = _scrolls[index - 1].scroll.scrollSprite;
        _scrollMenuItems[index - 1].SetActive(true);
    }

    public void HideScrollMenu ()
    {
        scrollMenuPanel.SetActive(false);
        scrollMenuOpenButton.SetActive(true);
    }

    public void FadeOutScreen ()
    {

    }

    public void FadeInScreen ()
    {

    }

    [ContextMenu("Autofill Scrolls")]
    void AutofillCollectibles ()
    {
        _scrollMenuItems = GameObject.FindGameObjectsWithTag("ScrollImage").ToList();
    }

}
