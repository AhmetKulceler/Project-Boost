using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float acceleration = 1000f;
    [SerializeField] float rotation = 150f;
    Rigidbody playerRigidbody;
    AudioSource rocketBoostAudio;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        rocketBoostAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessAcceleration();              // Accelerating the Rocket
        ProcessRotation();                  // Rotating the Rocket
    }

    void ProcessAcceleration()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.AddRelativeForce(Vector3.up * acceleration * Time.deltaTime);
            if (!rocketBoostAudio.isPlaying)
            {
                rocketBoostAudio.Play();
            }            
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            rocketBoostAudio.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotation);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotation);
        }
    }

    void ApplyRotation(float rotationAmount)    // Rotate the rocket while preventing it to get effected by environment
    {
        playerRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationAmount * Time.deltaTime);
        playerRigidbody.freezeRotation = false;
    }
}
