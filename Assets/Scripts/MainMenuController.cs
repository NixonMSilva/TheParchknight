using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject instructionsPanel;

    public void PlayGame ()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame ()
    {
        Application.Quit();
    }

    public void ShowInstructions ()
    {
        instructionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void HideInstructions ()
    {
        instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void PlayAgain ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
