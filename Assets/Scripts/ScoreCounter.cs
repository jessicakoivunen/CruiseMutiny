using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreCounter : MonoBehaviour
{
    private Movement playerControllerScript;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI GameOver;
    public float currectScore;
    public float pointIncreasedPerSecond;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        playerControllerScript = playerObject.GetComponent<Movement>();

        currectScore = 0f;
        pointIncreasedPerSecond = 10f;      // Points increase by 10 points per second
    }


    void Update()
    {

        if (playerControllerScript.gameOver == false)
        {
            scoreText.text = "Score: " + ((int)currectScore).ToString();
            currectScore += pointIncreasedPerSecond * Time.deltaTime;
        }
        else
        {
            GameOver.text = "Game Over!";
        }
    }
}