using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    Scene m_Scene;
    string m_SceneName;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private List<AudioClip> m_AudioClips = new List<AudioClip>();

    [Header("Sound Clips")]

    [SerializeField] private AudioClip m_Alarm;
    [SerializeField] private AudioClip m_Clamp;
    [SerializeField] private AudioClip m_Heat_treating;

    public static SoundManager s_Instance { get; private set; }

    private void Awake()
    {
    
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(this);
        }
        else
        {
            s_Instance = this;
        }
    }

    void Start()
    {

        m_Scene = SceneManager.GetActiveScene();
        m_SceneName = m_Scene.name;
        if (m_SceneName == "Main Menu")
        {
            m_AudioSource.Stop();
            m_AudioSource.clip = m_AudioClips[0];
            m_AudioSource.Play();
        }
        else if (m_SceneName == "Game")
        {
            m_AudioSource.Stop();
            m_AudioSource.clip = m_AudioClips[1];
            m_AudioSource.Play();
        }
    }
    
    public void PlayAlarm()
    {
        m_AudioSource.PlayOneShot(m_Alarm, 0.5f);
    }

    public void PlayClamp()
    {
        m_AudioSource.PlayOneShot(m_Clamp, 0.5f);
    }

    public void PlayHeatTreat()
    {
        m_AudioSource.PlayOneShot(m_Heat_treating, 0.5f);
    }
}
