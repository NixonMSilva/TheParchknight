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
    
    private Image potionIcon;
    private TextMeshProUGUI potionKeyText;

    private GameObject scrollMenuPanel;
    private GameObject scrollMenuOpenButton;

    private GameObject captureSliderPanel;
    private Slider captureSlider;

    private Image black;

    private GameObject returnText;

    private AudioManager audioManager;

    [SerializeField] private List<GameObject> _scrollMenuItems;

    private string defaultTooltipText;

    private Color activatedColor = new Color(1f, 1f, 1f, 1f);
    private Color deactivatedColor = new Color(1f, 1f, 1f, 0.25f);

    private void Awake ()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        tooltipText = GameObject.Find("TooltipText").GetComponent<TextMeshProUGUI>();
        coinCount = GameObject.Find("CoinCount").GetComponent<TextMeshProUGUI>();
        scrollCount = GameObject.Find("ScrollCount").GetComponent<TextMeshProUGUI>();

        potionIcon = GameObject.Find("PotionIcon").GetComponent<Image>();
        potionKeyText = GameObject.Find("PotionKey").GetComponent<TextMeshProUGUI>();

        scrollMenuOpenButton = GameObject.Find("ScrollMenuButton");
        scrollMenuPanel = GameObject.Find("ScrollViewPanel");

        captureSliderPanel = GameObject.Find("CapturePanel");
        captureSlider = captureSliderPanel.GetComponentInChildren<Slider>();

        returnText = GameObject.Find("ReturnText");
        returnText.gameObject.SetActive(false);

        black = GameObject.Find("Black").GetComponent<Image>();
        
        potionIcon.color = deactivatedColor;

        defaultTooltipText = tooltipText.text;

        
    }

    private void Start ()
    {
        HideTooltip();
        HideScrollMenu();
        HideCaptureBar();
        IntializeCounters();

        potionKeyText.text = "[" + InputHandler.current.potionUseKey.ToString() + "]";
    }

    public void UpdateCaptureBar (float value)
    {
        captureSlider.value = value;
    }

    public void ShowCaptureBar ()
    {
        captureSliderPanel.SetActive(true);
    }

    public void HideCaptureBar()
    {
        captureSliderPanel.SetActive(false);
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

    public void ShowReturnText ()
    {
        returnText.gameObject.SetActive(true);
    }
    public void HideReturnText ()
    {
        returnText.gameObject.SetActive(false);
    }

    public void UpdateCoinCount (int value)
    {
        coinCount.text = value.ToString();
    }

    public void UpdateScrollCount (int value)
    {
        scrollCount.text = value.ToString();
    }

    public void SetPotionIconStatus (bool value)
    {
        if (value)
            potionIcon.color = activatedColor;
        else
            potionIcon.color = deactivatedColor;
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
        //Debug.Log(_scrollMenuItems[index - 1].gameObject.name);
        //Debug.Log(_scrolls[index - 1].scroll.scrollSprite);
        _scrollMenuItems[index - 1].GetComponent<Image>().sprite = _scrolls[index - 1].scroll.scrollSprite;
        _scrollMenuItems[index - 1].SetActive(true);
    }

    public void HideScrollMenu ()
    {
        scrollMenuPanel.SetActive(false);
        scrollMenuOpenButton.SetActive(true);
    }

    public void FadeOutScreen (float time)
    {
        StartCoroutine(CanvasFade(time, 0f, 1f));
    }

    public void FadeInScreen (float time)
    {
        StartCoroutine(CanvasFade(time, 1f, 0f));
    }

    public void PlayMenuSound ()
    {
        audioManager.PlaySound("Wardrobe");
    }

    [ContextMenu("Autofill Scrolls")]
    void AutofillCollectibles ()
    {
        _scrollMenuItems = GameObject.FindGameObjectsWithTag("ScrollImage").ToList();
    }

    // 0 - Out | 1 - In
    IEnumerator CanvasFade (float duration, float startAlpha, float endAlpha)
    {
        float startTime = Time.time;
        float endTime = Time.time + duration;
        float elapsedTime = 0f;

        while (Time.time <= endTime)
        {
            elapsedTime = Time.time - startTime; // update the elapsed time
            var percentage = 1 / (duration / elapsedTime); // calculate how far along the timeline we are
            if (startAlpha > endAlpha) // if we are fading out/down 
            {
                black.color = new Color(black.color.r, black.color.g, black.color.b, startAlpha - percentage);
            }
            else // if we are fading in/up
            {
                black.color = new Color(black.color.r, black.color.g, black.color.b, startAlpha + percentage);
            }
            yield return new WaitForEndOfFrame();
        }
        black.color = new Color(black.color.r, black.color.g, black.color.b, endAlpha);
    }

}
