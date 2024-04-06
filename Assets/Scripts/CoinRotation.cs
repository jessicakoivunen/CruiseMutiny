using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    private Rigidbody rb = null;
    public float rotationSpeed = 100f;
    private float maxBob = 0.5f;
    private float bobSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.time * bobSpeed) * maxBob) + 1, transform.position.z);
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.angularVelocity = Vector3.up * rotationSpeed;
        }

    }
}
