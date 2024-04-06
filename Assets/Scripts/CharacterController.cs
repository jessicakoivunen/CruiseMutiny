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
    public bool PCcanMove = true;

    //Game over
    public bool gameOver = false;
    public bool volunteerConversation = false;
    public bool crewConversation = false;
    public bool speechBubble = false;

    public GameObject GameOverScreen;
    public GameObject CrewConversationPanel;
    public GameObject VolunteerConversationPanel;
    public GameObject SpeechBubble;

    //Score keeping
    [SerializeField] TMP_Text scoreText;
    public int score = 0;

    //Convo Panels
    [SerializeField] TMP_Text CrewConversationPanelText;
    [SerializeField] TMP_Text VolunteerConversationPanelText;

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
        if (PCcanMove)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        }
        else if (!PCcanMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        //Jump
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isOnGround)
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
        if (collision.CompareTag("Crew") && PCcanMove)
        {
            SpeechBubble.SetActive(true);
            CrewConversationPanel.SetActive(true);
            CrewText();

            // Once conversation panel is active, slow game scale to 0.2 for 5 seconds
            Time.timeScale = 0.9f;
            Debug.Log("Crew ACTIVE");
            TakeDamage(2);
        }
        if (collision.CompareTag("Volunteer"))
        {
            SpeechBubble.SetActive(true);
            VolunteerConversationPanel.SetActive(true);
            VolunteerText();
            Time.timeScale = 0.9f;
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
            PCcanMove = true;
        }
        if (collision.CompareTag("Volunteer"))
        {
            VolunteerConversationPanel.SetActive(false);
            Time.timeScale = 1;
            PCcanMove = true;
        }
    
    }
    int iRandomNumber()
    {
        return Random.Range(0, 3);
    }

    private void CrewText()
    {
        // Get a random number between 0 and 3  
        if (iRandomNumber() == 0)
        {
            CrewConversationPanelText.text = "Crew: Ello there matey!";
        }
        else if (iRandomNumber() == 1)
        {
            CrewConversationPanelText.text = "Crew: Ahoy there!";
        }
        else if (iRandomNumber() == 2)
        {
            CrewConversationPanelText.text = "Crew: Hey, watch where you're going!";
        }
        else if (iRandomNumber() == 3)
        {
            CrewConversationPanelText.text = "Crew: Fock off shoeless!";
        }
    }

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
            VolunteerConversationPanelText.text = "Volunteer: Aren´t you cold?";
        }
        else if (iRandomNumber() == 3)
        {
            VolunteerConversationPanelText.text = "Volunteer: Fock off shoeless!";
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
