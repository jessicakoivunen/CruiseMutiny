using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    // Menu items  for Continue (after pause), Play, Settings, and Quit
    public GameObject continueButton;
    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject quitButton;

    // Start is called before the first frame update
    void Start()
    {
        // If there is no saved game, disable the continue button
        if (PlayerPrefs.GetInt("Saved") == 0)
        {
            continueButton.SetActive(false);
        }

        // If there is a saved game, enable the continue button
        else
        {
            continueButton.SetActive(true);
        }

        // Set the play, settings, and quit buttons to active
        playButton.SetActive(true);
        settingsButton.SetActive(true);
        quitButton.SetActive(true);
    }
    // If the continue button is clicked, load the saved game
    public void ContinueGame()
    {
        Debug.Log("Continue Game");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    // If the play button is clicked, load the game
    public void PlayGame()
    {
        Debug.Log("Play Game");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    // If the settings button is clicked, load the settings menu
    public void Settings()
    {
        Debug.Log("Settings");
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    // If the quit button is clicked, quit the game
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
