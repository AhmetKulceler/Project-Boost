using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentLevelIndex;

    void Start()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hit friendly");
                break;
            case "Finish":
                Debug.Log("Congrats!");
                LoadNextLevel();
                break;
            default:
                Debug.Log("Game Over");
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentLevelIndex);
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
