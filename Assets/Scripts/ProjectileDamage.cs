using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for projectile damage; when the projectile hits the enemy, the enemy's health decreases by 1 HP.
/// Plays a sound when the projectile hits the enemy
/// * Remember to drag and drop the audio file into the script in the Unity Editor
/// </summary>

public class ProjectileDamage : MonoBehaviour
{
    public AudioClip coal_hit; // Reference to the audio clip
    private AudioSource audioSource; // Reference to the audio source
    int iEnemyHealth = 3;   // Set the enemy's health to 3 HP

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // Gets the audio source component
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Weapon")   // The projectiles have the tag "Weapon" in Unity. Can be used for melee weapons as well
        {
            audioSource.PlayOneShot(coal_hit, 1.0f);
            iEnemyHealth--;
            Destroy(other.gameObject);  // Destroys the projectile when it hits the enemy

            if (iEnemyHealth <= 0)  // Kills the enemy when the health reaches 0
            {
                Destroy(gameObject);
            }
        }
    }
}
