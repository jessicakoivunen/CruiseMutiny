using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveRight : MonoBehaviour
{
    private float speed = 10f;
    private Vector2 direction = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        direction.x = speed;
        transform.Translate(direction * Time.deltaTime);
    }
}
