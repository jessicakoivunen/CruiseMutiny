using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterController : MonoBehaviour
{
    //Move + Jump
    public float speed = 5f;
    private Rigidbody2D rb = null;
    public float jumpForce = 6f;
    public bool isOnGround = true;
    public bool canMove = true;

    //Game over
    public bool gameOver = false;
    public bool volunteerConversation = false;
    public bool crewConversation = false;

    public GameObject GameOverScreen;
    public GameObject CrewConversationPanel;
    public GameObject VolunteerConversationPanel;

    //Score keeping
    [SerializeField] TMP_Text scoreText;
    public int score = 0;

    //Health
    int health = 10;
    [SerializeField] TMP_Text healthText;

    //Projectiles


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Move left/right
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (canMove)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        }
        else if (!canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        //Jump
        if (((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isOnGround) && canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;

        //Score
        if (collision.collider.CompareTag("Score"))
        {
            score++;
            scoreText.text = "DOUBLOONS:  " + score.ToString();
        }

    }

    // Once conversation panel is active, pause the game for 5 seconds
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Damage
        if (collision.CompareTag("Crew"))
        {
            CrewConversationPanel.SetActive(true);
            // Once conversation panel is active, slow game scale to 0.2 for 5 seconds
            Time.timeScale = 0.2f;
            Debug.Log("Crew ACTIVE");
            canMove = false;
            TakeDamage(2);
        }
        if (collision.CompareTag("Volunteer"))
        {
            VolunteerConversationPanel.SetActive(true);
            Time.timeScale = 0.2f;
            canMove = false;
            TakeDamage(1);
        }
        if (collision.CompareTag("Security"))
        {
            score = 0;
            scoreText.text = "DOUBLOONS:  " + score.ToString();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Crew"))
        {
            CrewConversationPanel.SetActive(false);
            Time.timeScale = 1;
            canMove = true;
        }
        if (collision.CompareTag("Volunteer"))
        {
            VolunteerConversationPanel.SetActive(false);
            Time.timeScale = 1;
            canMove = true;
        }
    
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            healthText.text = "HEALTH:  " + health.ToString();
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
