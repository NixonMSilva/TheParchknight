using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    private UIController UIcontroller;

    private bool isShowingText = false;
    private bool hasShownText = false;

    [SerializeField] private float textTimeoutTime = 3f;
    [SerializeField] private float textTimeout;

    private void Awake ()
    {
        UIcontroller = GameObject.Find("UI").GetComponent<UIController>();
        
        textTimeout = textTimeoutTime;
    }

    private void Update ()
    {
        if (isShowingText && !hasShownText)
        {
            if (textTimeout <= 0f)
            {
                UIcontroller.HideReturnText();
                hasShownText = true;
            }
            textTimeout -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (hasShownText)
        {
            SceneManager.LoadScene("Victory");
        }
    }

    public void DrawVictory ()
    {
        UIcontroller.ShowReturnText();
        isShowingText = true;
    }
}
