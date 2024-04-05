using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float fSpeed = 5.0f;
    public float fJumpForce = 7.0f;
    public float fGravity = 5.0f;
    public bool gameOver = false;
    private Vector2 moveDirection = Vector2.zero;


    // Update is called once per frame
    void Update()
    {
        // The character jumps 2 unites when the W key is pressed

    if (gameOver == false)
        {
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x = -fSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x = fSpeed;
        }
        if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.W))) && moveDirection.y == 0)
        {
            moveDirection.y += fJumpForce;
        }
        if (moveDirection.y > 0)
        {
            moveDirection.y -= fGravity * Time.deltaTime;
        }
        else
        {
            moveDirection.y = 0;
        }
        transform.Translate(moveDirection * Time.deltaTime);

        // The character stops moving when the key is released
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            moveDirection.x = 0;
        }
    }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Security"))
        {
            Debug.Log("Security: Halt! Who goes there?");
            gameOver = true;
        }
    }
}
