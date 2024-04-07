using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the AI movement and behavior
/// * Remember to add a Rigidbody2D component to the AI in the Unity Editor
/// * Remember to add a BoxCollider2D component to the AI in the Unity Editor
/// Three different tags for the AI: "Volunteer", "Crew", and "Security"
/// * Volunteer moves back and forth in a small area
/// * Crew moves back and forth in a larger area
/// * Security stands in place and triggers a dialogue when the player gets close
///     * If the player has at least 5 doubloons, the player can bribe the security guard
///     * If the player has less than 5 doubloons, the security guard will not let the player pass
/// </summary>
public class AIController : MonoBehaviour
{
    private float speed;
    private float movementRadius;
    public bool gameOver = false;
    public bool canMove = true;
    private Rigidbody2D rb = null;

    // Sets the speed and movement radius for the AI based on the tag
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (CompareTag("Security"))
        {
            speed = 0;  // No speed for Security
            movementRadius = 0; // No movement for Security
        }
        else if (CompareTag("Crew"))
        {
            speed = 3;  // Faster speed for Crew
            movementRadius = 4; // Larger movement radius for Crew

        }
        else if (CompareTag("Volunteer"))
        {
            speed = 1;  // Slower speed for Volunteer
            movementRadius = 2; // Smaller movement radius for Volunteer
        }        
    }

/// <summary>
/// Check if the AI can move and move the AI based on the tag
///     * Crew and Volunteer move back and forth
/// PingPong is used to move the AI back and forth
///     * Mathf.PingPong returns a value between 0 and the movement radius
///     * The AI moves between -movementRadius and movementRadius
/// </summary>
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
        else if (!canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    /// <summary>
    /// Stops the AI from moving when the player is close
    ///     * This prevents the AI from running away too fast, and gives the player time to read the dialogue
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
    /// <summary>
    /// Allows the AI to move when the player is not close
    /// </summary>
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
