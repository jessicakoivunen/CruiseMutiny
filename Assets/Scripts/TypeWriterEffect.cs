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
    public bool StoryScreen = false;

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
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        isDone = true;
    }

    void OutroScene()
    {
        if ((SceneManager.GetActiveScene().buildIndex > 3 || SceneManager.GetActiveScene().buildIndex <= 5) || (SceneManager.GetActiveScene().buildIndex > 7) || (SceneManager.GetActiveScene().buildIndex <= 16))
            {
            StoryScreen = true;
        }
        else
        {
            StoryScreen = false;
        }
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
        OutroScene();

        if (isDone == true)
        {
            isDoneTyping = true;
            
        }
        // If the player presses the space bar, the game will move to the next scene, when the scene number is odd. or
        if ((isDoneTyping) && (SceneManager.GetActiveScene().buildIndex%2 == 1) || StoryScreen == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        // If the player presses the "Q" key, the game will quit.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
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
