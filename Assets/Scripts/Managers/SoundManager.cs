using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    Scene scene;
    string sceneName;
    public AudioSource audioSource;
    public AudioClip Alarm;
    public AudioClip Clamp;
    public AudioClip Heat_treating;
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
    
    public void PlayAlarm()
    {
        audioSource.PlayOneShot(Alarm, 0.5f);
    }

    public void PlayClamp()
    {
        audioSource.PlayOneShot(Clamp, 0.5f);
    }

    public void PlayHeatTreat()
    {
        audioSource.PlayOneShot(Heat_treating, 0.5f);
    }
}
