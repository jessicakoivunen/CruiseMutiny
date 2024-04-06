using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject ControlsPanel;
    public GameObject CreditsPanel;
    public GameObject OptionsPanel;
    public GameObject PausePanel;

    // If the play button is clicked, load the game
    public void PlayGame()
    {
        Debug.Log("Play Game");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    // If the quit button is clicked, quit the game
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void MainMenu()
    {
        //Open main menu
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    //Open + close controls
    public void OpenControls()
    {
        if (ControlsPanel.activeInHierarchy)
        {
            ControlsPanel.SetActive(false);
        }
        else
        {
            ControlsPanel.SetActive(true);
        }
    }

    //Open + close credits
    public void OpenCredits()
    {
        if (CreditsPanel.activeInHierarchy)
        {
            CreditsPanel.SetActive(false);
        }
        else
        {
            CreditsPanel.SetActive(true);
        }
    }

    //Open + close settings/options
    public void OpenOptions()
    {
        if (OptionsPanel.activeInHierarchy)
        {
            OptionsPanel.SetActive(false);
        }
        else
        {
            OptionsPanel.SetActive(true);
        }
    }

    public void PauseGame()
    {
        if (PausePanel.activeInHierarchy)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
