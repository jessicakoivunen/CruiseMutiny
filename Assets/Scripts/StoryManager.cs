using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StoryManager : MonoBehaviour
{
    public bool StoryScreen = false;
    bool bIsSecurity = false;
    public TypeWriterEffect typeWriterEffect;

    void OutroScene()
    {
        if ((SceneManager.GetActiveScene().buildIndex == 1) && (SceneManager.GetActiveScene().buildIndex > 2) && (SceneManager.GetActiveScene().buildIndex <= 12) || (SceneManager.GetActiveScene().buildIndex > 13) && (SceneManager.GetActiveScene().buildIndex <= 23))
        {
            StoryScreen = true;
        }
        else
        {
            StoryScreen = false;
        }
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    // Update is called once per frame
    void Update()
    {
        OutroScene();

        // if the scene is a story scene, the player can press the space bar to move to the next scene
        // or if the player collides with an NPC with the tag "Security", the player can press the space bar to move to the next scene
        if ((StoryScreen == true) || (bIsSecurity == true))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextScene();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Security"))
        {
            bIsSecurity = true;
        }
        else
        {
            bIsSecurity = false;
        }
    }

    public void Skip()
    {
        StopAllCoroutines();
        this.GetComponent<TextMeshProUGUI>().text = typeWriterEffect.fullText;
        typeWriterEffect.isDone = true;
    }
}
