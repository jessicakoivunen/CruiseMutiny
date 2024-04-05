using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// There are tags for "Volunteer", who walk around back and forth in a small area, Crew, who walk around a larger area, and Security, who stand in place and trigger a dialogue when the player gets close.

public class AIController : MonoBehaviour
{
    public float speed;
    public Transform player;
    public float stoppingDistance;
    public float retreatDistance;
    public float retreatSpeed;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (tag == "Security")
        {
            speed = 0;
            stoppingDistance = 1;
            retreatDistance = 1;
            retreatSpeed = 1;


        }
        else if (tag == "Crew")
        {
            speed = 3;
            stoppingDistance = 3;
            retreatDistance = 2;
            retreatSpeed = 2;

        }
        else if (tag == "Volunteer")
        {
            speed = 1;
            stoppingDistance = 1;
            retreatDistance = 1;
            retreatSpeed = 1;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (tag == "Security")
            {
                Debug.Log("Security: Halt! Who goes there?");
            }
            else if (tag == "Crew")
            {
                Debug.Log("Crew: Hey, watch where you're going!");
            }
            else if (tag == "Volunteer")
            {
                Debug.Log("Volunteer: Hi there! Enjoying the festival?");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (tag == "Security")
            {
                Debug.Log("Security: That's right, keep moving!");
            }
            else if (tag == "Crew")
            {
                Debug.Log("Crew: Phew, that was close!");
            }
            else if (tag == "Volunteer")
            {
                Debug.Log("Volunteer: Have a great day!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, retreatDistance);
    }
}
