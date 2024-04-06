using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    //Audio
    public AudioClip jump_audio;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 1;
    }

    void RandomProjectile()
    {
        int rnd = Random.Range(0, projectiles.Length);
        Instantiate(projectiles[rnd], rb.position + Vector2.right*offset, projectiles[rnd].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //Move left/right
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        //Jump
        if (Input.GetKeyDown(KeyCode.W) && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isOnGround = false;
            playerAudio.PlayOneShot(jump_audio, 1.0f);
        }

        //Throw projectiles
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //shoot projectile
            RandomProjectile();
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
            Destroy(collision.gameObject);
        }

    }

    // Once conversation panel is active, pause the game for 5 seconds
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //NPC interactions
        if (collision.CompareTag("Crew"))
        {
            //Speech
            SpeechBubble.SetActive(true);
            CrewText();
            //Damage
            TakeDamage(2);
        }
        if (collision.CompareTag("Volunteer"))
        {
            //Speech
            SpeechBubble.SetActive(true);
            VolunteerText();
            //Damage
            TakeDamage(1);
        }
        if (collision.CompareTag("Security"))
        {
            // Check if player has enough score to pass
            if (score >= 5)
            {
                // if YES, subtract 5 from score and load next level
                score -= 5;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.Log("Security: Halt! Who goes there?");
            }
        }
    }

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
        if (health >= 1)
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
