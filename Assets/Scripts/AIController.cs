using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// There are tags for "Volunteer", who walk around back and forth in a small area, Crew, who walk around a larger area, and Security, who stand in place and trigger a dialogue when the player gets close.

public class AIController : MonoBehaviour
{
    private float speed;
    private float movementRadius;
    public bool gameOver = false;
    public bool canMove = true;
    private Rigidbody2D rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (CompareTag("Security"))
        {
            speed = 0;
            movementRadius = 0;
        }
        else if (CompareTag("Crew"))
        {
            speed = 3;
            movementRadius = 4;

        }
        else if (CompareTag("Volunteer"))
        {
            speed = 1;
            movementRadius = 2;
        }        
    }

    // Update is called once per frame
    // The AI will move on x axis back and forth within a certain area regardless of player position or movement.
    // The AI should not move on the y axis, so put a lock on that.
    void Update()
    {
        if (CompareTag("Crew") && (canMove))
        {
            rb.velocity = new Vector2(Mathf.PingPong(Time.time * speed, movementRadius * 2) - movementRadius, rb.velocity.y);
        }
        else if (CompareTag("Volunteer") && (canMove))
        {
            rb.velocity = new Vector2(Mathf.PingPong(Time.time * speed, movementRadius * 2) - movementRadius, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CompareTag("Security"))
            {
                Debug.Log("Security: Halt! Who goes there?");
                // if the score in CharacterController.cs is greater than or equal to 5, the player can move to the next scene.
  
                if (collision.gameObject.GetComponent<CharacterController>().score >= 5)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            if (CompareTag("Crew"))
            {
                canMove = false;
            }
            if (CompareTag("Volunteer"))
            {
                canMove = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CompareTag("Crew"))
            {
                canMove = true;
            }
            if (CompareTag("Volunteer"))
            {
                canMove = true;
            }
        }
    }
}
