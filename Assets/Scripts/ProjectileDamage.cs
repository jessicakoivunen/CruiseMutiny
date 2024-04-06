using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileDamage : MonoBehaviour
{
    public AudioClip coal_hit;
    private AudioSource audioSource;
    int iEnemyHealth = 3;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            audioSource.PlayOneShot(coal_hit, 1.0f);
            iEnemyHealth--;
            Destroy(other.gameObject);

            if (iEnemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
