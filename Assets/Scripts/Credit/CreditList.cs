using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditList : MonoBehaviour
{
    [SerializeField] GameObject[] m_Counts;
    [SerializeField] float m_TimeBetween = 3.5f; // Time in seconds

    private void Start()
    {
        ShowTheCounting();
    }
    public void ShowTheCounting()
    { // You call this function
        StartCoroutine(Count());
    }

    public IEnumerator Count()
    {
        for (int i=0; i< m_Counts.Length; i++)
        {

            m_Counts[i].SetActive(true);

            LanguageSettings.ChangeFont(LanguageSettings.m_FinalFont);

            yield return new WaitForSeconds(m_TimeBetween); // Waits for the time set in timeBetween, affected by timeScale.
            if (i < m_Counts.Length - 1)
            {
                m_Counts[i].SetActive(false);

            }
        }
    }
}
