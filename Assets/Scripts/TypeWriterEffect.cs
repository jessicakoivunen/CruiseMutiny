using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class TypeWriterEffect : MonoBehaviour
{

    public float delay = 0.1f;
    public string fullText;
    private string currentText = "";
    public bool isTyping = false;
    public bool isDone = false;
    public bool isDoneTyping = false;

    void Start()
    {
        StartCoroutine(ShowText());

    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        isDone = true;
    }

    void Update()
    {
        if (isDone == true)
        {
            isDoneTyping = true;
        }
    }

    public void Skip()
    {
        StopAllCoroutines();
        this.GetComponent<TextMeshProUGUI>().text = fullText;
        isDone = true;
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }




}
