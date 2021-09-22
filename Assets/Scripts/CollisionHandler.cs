using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip crashExplosionAudio;
    [SerializeField] AudioClip successfulLandingAudio;

    [SerializeField] ParticleSystem crashExplosionParticles;
    [SerializeField] ParticleSystem successfulLandingParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    int currentLevelIndex;    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        ActionShortcuts();
    }

    private void ActionShortcuts()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            if (!isTransitioning)
                isTransitioning = true;
            else if (isTransitioning)
                isTransitioning = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<PlayerMovement>().enabled = false;

        audioSource.Stop();
        audioSource.PlayOneShot(crashExplosionAudio);

        crashExplosionParticles.Play();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentLevelIndex);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        GetComponent<PlayerMovement>().enabled = false;

        audioSource.Stop();
        audioSource.PlayOneShot(successfulLandingAudio);

        successfulLandingParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }    

    void LoadNextLevel()
    {
        int nextLevelIndex = currentLevelIndex + 1;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }
}
