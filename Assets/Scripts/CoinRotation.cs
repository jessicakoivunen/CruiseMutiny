using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the coin and makes it bob up and down
/// * Remember to add a Rigidbody component to the coin in the Unity Editor
/// * Remember to add a Collider component to the coin in the Unity Editor
/// </summary>
public class CoinRotation : MonoBehaviour
{
    private Rigidbody rb = null; // Reference to the Rigidbody component
    public float rotationSpeed = 100f;  // The speed at which the coin rotates
    private float maxBob = 0.5f;    // The maximum height the coin bobs up and down
    private float bobSpeed = 2f;    // The speed at which the coin bobs up and down

    // Update is called once per frame
    void Update()
    {
        // Rotates the coin; Time.deltaTime makes the rotation smooth;
        // Vector3.up rotates the coin around the y-axis;
        // rotationSpeed is the speed of the rotation;
        // If you want to rotate the coin around the x-axis, use Vector3.right;
        // If you want to rotate the coin around the z-axis, use Vector3.forward
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.time * bobSpeed) * maxBob) + 1, transform.position.z); // Makes the coin bob up and down
        rb = GetComponent<Rigidbody>(); // Gets the Rigidbody component

        if (rb != null) // If the Rigidbody component is not null
        {
            rb.angularVelocity = Vector3.up * rotationSpeed;    // Rotates the coin
        }
    }
}
