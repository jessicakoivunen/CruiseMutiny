using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Removes any object that goes out of bounds.
/// The boundary are just colliders placed at the edges of the screen.
/// </summary>
public class OutOfBounds : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)  // Destroys objects with colliders
    {
        Destroy(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision) // Destroys objects with triggers
    {
        Destroy(collision.gameObject);
    }
}
