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

    //Game over
    public bool gameOver = false;
    public GameObject GameOverScreen;

    //Score keeping
    [SerializeField] TMP_Text scoreText;
    int score = 0;

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
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Damage
        if (collision.CompareTag("Crew"))
        {
            TakeDamage(2);
        }
        if (collision.CompareTag("Volunteer"))
        {
            TakeDamage(1);
        }
        if (collision.CompareTag("Security"))
        {
            score = 0;
            scoreText.text = "DOUBLOONS:  " + score.ToString();
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
