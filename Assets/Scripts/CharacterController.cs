using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls the player character's movement, interactions, and health
/// Score is kept track of and displayed on the screen
/// Conversations with NPCs are triggered when the player gets close to them
/// Score (doubloons) are used to bribe the security guard to pass
/// </summary>
public class CharacterController : MonoBehaviour
{
    //Move + Jump
    public float speed = 5f;
    private Rigidbody2D rb = null;
    public float jumpForce = 6f;
    public bool isOnGround = true;

    //Game over
    public bool gameOver = false;
    public GameObject GameOverScreen;
    public GameObject PauseButton;

    //Score keeping
    [SerializeField] TMP_Text scoreText;
    public int score = 0;

    //Convo Panels
    [SerializeField] TMP_Text CrewConversationPanelText;
    [SerializeField] TMP_Text VolunteerConversationPanelText;
    public GameObject SpeechBubble;

    //Health
    int health = 10;
    [SerializeField] TMP_Text healthText;

    //Projectiles
    [SerializeField] GameObject[] projectiles;
    public float offset = 0.2f;

    /// <summary>
    /// Audio clips for player actions
    /// </summary>
    private AudioSource playerAudio;                // Reference to the audio source
    public AudioClip jump_audio;                    // Plays when the player jumps
    public AudioClip coin_pickup;                   // Plays when the player picks up a coin
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();  // Gets the audio source component
        rb = GetComponent<Rigidbody2D>();           // Gets the Rigidbody2D component
        Time.timeScale = 1;                         // Ensures the game is not paused
    }
    void RandomProjectile()
    {
        int rnd = Random.Range(0, projectiles.Length);                                                          // Randomly selects a projectile from the array of projectiles
        Instantiate(projectiles[rnd], rb.position + Vector2.right*offset, projectiles[rnd].transform.rotation); // Spawns the projectile in front of the player character
    }

    /// <summary>
    /// Set the player큦 controls for movement, jumping, and shooting projectiles.
    ///     * The player can move left and right using the A and D keys
    ///     * The player can jump using the W key
    ///     * The player can shoot projectiles using the Space key
    /// Check if the player is touching the ground to allow jumping
    ///     * if not, the player cannot jump
    /// </summary>
    void Update()
    {
        //Move left/right
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);   // Moves the player left and right

        //Jump
        if (Input.GetKeyDown(KeyCode.W) && isOnGround)                  // Checks if the player is on the ground before jumping
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);        // Makes the player jump
            isOnGround = false;                                         // Prevents the player from jumping in the air
            playerAudio.PlayOneShot(jump_audio, 1.0f);                  // Plays the jump audio
        }

        //Throw projectiles
        if (Input.GetKeyDown(KeyCode.Space))                            // Checks if the player presses the space key
        {
            RandomProjectile();                                         // Calls the RandomProjectile function to shoot a projectile
        }
    }

    /// <summary>
    /// Player collects coins and increases the score
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;

        if (collision.collider.CompareTag("Score"))                 // Checks if the player collides with an object tagged "Score"
        {
            playerAudio.PlayOneShot(coin_pickup, 1.0f);             // Plays the coin pickup audio
            score++;                                                // Increases the score by 1
            scoreText.text = "DOUBLOONS:  " + score.ToString();     // Displays the score on the screen
            Destroy(collision.gameObject);                          // Destroys the coin object once the player collects it
        }
    }

    /// <summary>
    /// Set the player큦 interactions with NPCs and security guard
    ///     * The player can interact with the crew, volunteers, and security guard
    ///     * The player can take damage from the crew and volunteers
    ///     * The player can pass the security guard if the score is greater than or equal to 5
    ///     * If the player has less than 5 doubloons, the security guard will not let the player pass
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //NPC interactions
        if (collision.CompareTag("Crew"))   // Checks if the player collides with an object tagged "Crew"
        {
            SpeechBubble.SetActive(true);   // Displays the speech bubble
            CrewText();                     // Calls the CrewText function to display a random conversation
            TakeDamage(2);                  // Crew deals more damage (-2 hp)
        }
        if (collision.CompareTag("Volunteer"))  // Checks if the player collides with an object tagged "Volunteer"
        {
            SpeechBubble.SetActive(true);       // Displays the speech bubble
            VolunteerText();                    // Calls the VolunteerText function to display a random conversation
            TakeDamage(1);                      // Volunteer deals less damage (-1 hp)
        }
        if (collision.CompareTag("Security"))       // Checks if the player collides with an object tagged "Security"
        {
            if (score >= 5)                         // Checks if the player has 5 or more doubloons
            {
                // if YES, subtract 5 from score and load next level
                score -= 5;                         // Subtracts 5 from the score when the player passes the security guard
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   // Loads the next level
            }
            else
            {
                Debug.Log("Security: Halt! Who goes there?");
            }
        }
    }
    /// <summary>
    /// Disable the speech bubble when the player exits the NPC큦 trigger area
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Crew"))
        {
            SpeechBubble.SetActive(false);
        }
        if (collision.CompareTag("Volunteer"))
        {
            SpeechBubble.SetActive(false);
        }
    }
    /// <summary>
    /// Randomizes the conversation text for the crew based on a random number
    /// </summary>
    int iRandomNumber()
    {
        return Random.Range(0, 3);
    }

    /// <summary>
    /// Displays a random conversation for the crew based on a random number from iRandomNumber()-function
    /// </summary>
    private void CrewText()
    {
        // Get a random number between 0 and 3  
        if (iRandomNumber() == 0)                                                       // Checks if the random number is 0
        {
            CrewConversationPanelText.text = "Crew: Ello there matey!";                 // Displays the conversation text
        }
        else if (iRandomNumber() == 1)
        {
            CrewConversationPanelText.text = "Crew: Ahoy there!";                       // Displays the conversation text
        }
        else if (iRandomNumber() == 2)                                                  // Checks if the random number is 2
        {
            CrewConversationPanelText.text = "Crew: Hey, watch where you're going!";    // Displays the conversation text
        }
        else if (iRandomNumber() == 3)                                                  // Checks if the random number is 3
        {
            CrewConversationPanelText.text = "Crew: Fock off shoeless!";                // Displays the conversation text
        }
    }

    /// <summary>
    /// Randomizes the conversation text for the volunteer based on a random number
    /// </summary>
    public void VolunteerText()
    {
        // Get a random number between 0 and 3  
        if (iRandomNumber() == 0)
        {
            VolunteerConversationPanelText.text = "Volunteer: Are you here to volunteer as well?";
        }
        else if (iRandomNumber() == 1)
        {
            VolunteerConversationPanelText.text = "Volunteer: Where are your shoes!?";
        }
        else if (iRandomNumber() == 2)
        {
            VolunteerConversationPanelText.text = "Volunteer: Aren큧 you cold?";
        }
        else if (iRandomNumber() == 3)
        {
            VolunteerConversationPanelText.text = "Volunteer: Fock off shoeless!";
        }
    }

    /// <summary>
    /// Player takes damage from the crew and volunteers
    ///     * The player큦 health decreases by 1 HP when hit by the volunteer
    ///     * The player큦 health decreases by 2 HP when hit by the crew
    ///     * If the player큦 health reaches 0, the game is over
    /// </summary>
    public void TakeDamage(int damage)                          // Takes damage from the crew and volunteers
    {
        if (health >= 1)                                        // Checks if the player큦 health is greater than or equal to 1
        {
            health -= damage;                                   // Decreases the player큦 health by the damage amount
            healthText.text = "HEALTH:  " + health.ToString();  // Displays the player큦 health on the screen
        }
        else
        {
            GameOver();                                         // Calls the GameOver function when the player큦 health reaches 0
        }
    }
    /// <summary>
    /// Stops the game and displays the game over screen when the player큦 health reaches 0
    /// </summary>
    public void GameOver()
    {
        PauseButton.SetActive(false);    // Disables the pause button
        GameOverScreen.SetActive(true); // Displays the game over screen
        Time.timeScale = 0;             // Pauses the game
    }
}
