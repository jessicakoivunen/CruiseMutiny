using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileDamage : MonoBehaviour
{
    int iEnemyHealth = 3;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            iEnemyHealth--;
            Destroy(other.gameObject);

            if (iEnemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
