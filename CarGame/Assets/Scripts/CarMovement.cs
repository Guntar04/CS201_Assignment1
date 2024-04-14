using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour
{
    [SerializeField] float carSpeed = 10.0f;
    [SerializeField] float carMaxSpeed = 30.0f;
    [SerializeField] float rotationSpeed = 80.0f;
    [SerializeField] float nitroFuelMax = 100.0f;
    [HideInInspector] public float nitroFuel = 100.0f;
    [HideInInspector] public float coinCount = 0;
    public float winTime = -1.0f;

    Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the GameObject
    }

    void Update()
    {
        // Check if the car is grounded (close to the ground)
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Apply gravity if the car is not grounded
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * 9.8f, ForceMode.Acceleration); // Apply gravity
        }

        // Adjust car movement based on input
        if (Input.GetKey(KeyCode.W))
        {
            // Gradually increase car speed over time
            carSpeed += Time.deltaTime * 5.0f;

            // Check if nitro is being used to accelerate the increase
            if (Input.GetKey(KeyCode.LeftShift) && nitroFuel > 0)
            {
                carSpeed += Time.deltaTime * 10.0f; // Accelerate speed with nitro
                nitroFuel -= 0.1f;
                print("Nitro Fuel: " + nitroFuel);
            }

            // Clamp car speed to maximum speed
            carSpeed = Mathf.Clamp(carSpeed, 0f, carMaxSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Gradually decrease car speed over time until it stops
            carSpeed -= Time.deltaTime * 20.0f;
            carSpeed = Mathf.Clamp(carSpeed, -10f, carMaxSpeed);
        }

        // Implement reverse movement
        else if (carSpeed <= 0 && Input.GetKey(KeyCode.S))
        {
            // Gradually increase reverse speed over time until it reaches a maximum of 10
            carSpeed += Time.deltaTime * 10.0f;
            carSpeed = Mathf.Clamp(carSpeed, -10f, 0f);
        }

        // If no input, gradually decrease car speed
        else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            carSpeed -= Time.deltaTime * 2.0f;
            carSpeed = Mathf.Clamp(carSpeed, 0f, carMaxSpeed);
        }

        // Apply forward movement
        transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);

        // Adjust car rotation based on input
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * (-rotationSpeed * Time.deltaTime));

            // Reduce car speed while drifting
            if (carSpeed > 0)
            {
                carSpeed -= Time.deltaTime * 5.0f;
                carSpeed = Mathf.Max(carSpeed, 0f);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));

            // Reduce car speed while drifting
            if (carSpeed > 0)
            {
                carSpeed -= Time.deltaTime * 5.0f;
                carSpeed = Mathf.Max(carSpeed, 0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            coinCount++;
            Destroy(other.gameObject);
            Debug.Log("Coin Collected");

            // Check if the player collected 10 coins
            if (coinCount == 10 && winTime < 0) // Ensure we only save the win time once
            {
                winTime = Time.timeSinceLevelLoad; // Save the current time
                Debug.Log("Win Time Set: " + winTime); // Add this line for debugging
            }
        }
        if (other.gameObject.tag == "Nitro")
        {
            if(nitroFuel < nitroFuelMax)
            {
                nitroFuel += 50.0f;
            }
            Destroy(other.gameObject);
            Debug.Log("Nitro Collected");
            Debug.Log("Nitro Fuel: " + nitroFuel);
        }
    }

    // Handle collision physics
    private void OnCollisionEnter(Collision collision)
    {
        // If colliding with an obstacle, reduce car speed
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Reduce car speed to simulate impact
            carSpeed *= 1f; // Adjust this value as needed
        }
    }
}
