using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


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

    /// <summary>
    /// This coroutine will show the text one character at a time.
    ///     * The currentText is updated with the next character in the fullText string
    /// WaitForSeconds is used to delay the next character
    /// isDone is set to true when the fullText has been displayed
    /// subString is used to get the next character in the fullText string
    /// </summary>
    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        isDone = true;
    }
    /// <summary>
    /// isDoneTyping is set to true when the text is done typing
    ///     * The player can press the space bar to move to the next scene
    ///     * The player can press the "Q" key to quit the game
    ///     * The player can press the "R" key to restart the game
    /// </summary>
    void Update()
    {
        // Once the text is done typing, the player can press the space bar to move to the next scene.

        if (isDone == true)
        {
            isDoneTyping = true;   
        }
        // If the scene doesn´t have player, the player can press the space bar to move to the next scene.
        // but if the scene name is Main Menu, don´t move to the next scene.
        if ((GameObject.Find("Player") == false) && (SceneManager.GetActiveScene().name != "MainMenu"))
        {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        if ((isDoneTyping) && (SceneManager.GetActiveScene().name == "L2_story11"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Load Main Menu scene
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
