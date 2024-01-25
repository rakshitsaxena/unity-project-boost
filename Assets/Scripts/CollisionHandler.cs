using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float m_loadLevelDelay = 1f;
    [SerializeField] private AudioClip crash;
    [SerializeField] private AudioClip success;

    [SerializeField] private ParticleSystem crashParticles;
    [SerializeField] private ParticleSystem sucesssParticles;

    AudioSource audioSource;

    bool isTrasitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) {
        if(isTrasitioning) { return; }

        tag = other.gameObject.tag;

        switch(tag){
            case "Friendly":
                Debug.Log("You have collided with launchpad");
                break;
            case "Finish":
                Debug.Log("Congratulations! You have finished this level");
                startSuccessSequence();
                break;
            default:
                Debug.Log("Sorry you blew up!");
                startCrashSequence();
                break;
            
        }
    }

    private void startCrashSequence()
    {
        //TODO: Add particle effect upon crash
        //TODO: Add sound after crash 
        isTrasitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false; //remove move control from the player after crash
        Invoke("ReloadLevel", m_loadLevelDelay);
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        
    }

    private void startSuccessSequence()
    {
        //TODO: Add success effect
        //TODO: Add SFX
        isTrasitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false; //remove move control from the player after crash
        Invoke("LoadNextLevel", m_loadLevelDelay);
        audioSource.PlayOneShot(success);
        sucesssParticles.Play();
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1; 
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // in the end load the first level again
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex);
    }
}
