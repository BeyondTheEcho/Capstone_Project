using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    Scene scene;
    string sceneName;
    public AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();

    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
    
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {

        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        if (sceneName == "Main Menu")
        {
            audioSource.Stop();
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
        else if (sceneName == "Game")
        {
            audioSource.Stop();
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Application.LoadLevel("Game");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
