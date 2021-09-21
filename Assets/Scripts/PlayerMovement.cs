using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float acceleration = 1000f;
    [SerializeField] float rotationStrength = 150f;
    [SerializeField] AudioClip rocketEngine;

    [SerializeField] ParticleSystem rocketEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody playerRigidbody;
    AudioSource audioSource;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessAcceleration();
        ProcessRotation();
    }

    void ProcessAcceleration()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartAcceleration();
        }
        else
        {
            StopAcceleration();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    private void StartAcceleration()
    {
        playerRigidbody.AddRelativeForce(Vector3.up * acceleration * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(rocketEngine);
        }
        if (!rocketEngineParticles.isPlaying)
        {
            rocketEngineParticles.Play();
        }            

    }
    private void StopAcceleration()
    {
        audioSource.Stop();
        rocketEngineParticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationStrength);

        if (!rightThrusterParticles.isPlaying)
            rightThrusterParticles.Play();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationStrength);

        if (!leftThrusterParticles.isPlaying)
            leftThrusterParticles.Play();
    }    

    private void StopRotation()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    void ApplyRotation(float rotation)    // Rotate the rocket while preventing it to get effected by environment
    {
        playerRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        playerRigidbody.freezeRotation = false;
    }
}
