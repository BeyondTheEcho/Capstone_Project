using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip Alarm;
    public AudioClip Clamp;
    public AudioClip Heat_treating;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
