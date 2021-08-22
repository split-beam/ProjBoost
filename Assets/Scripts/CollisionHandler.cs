using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] float explosionVolme = 0.5f;

    [SerializeField] AudioClip Explosion;
    [SerializeField] AudioClip Success;

    [SerializeField] ParticleSystem PartExplosion;
    [SerializeField] ParticleSystem PartSuccess;

    AudioSource audioSource;

    bool isTransitioning = false;

      void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return; }

        switch(collision.gameObject.tag)
        {
 
            case "Friendly":
                Debug.Log("Start!");
                break;

            case "Finish":
                ConfrimFinish();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        PartExplosion.Play();
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(Explosion, explosionVolme);
        Invoke("ReloadLevel", loadDelay);
    }

    void ConfrimFinish()
    {
        isTransitioning = true;
        PartSuccess.Play();
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(Success, 1);
        Invoke("LoadNextLevel", loadDelay);
    }

    void ReloadLevel()
    {
        //Use int for levels, strings for menus  SceneManager.LoadScene("Sandbox") ||  SceneManager.LoadScene("0"). Current example restarts current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
