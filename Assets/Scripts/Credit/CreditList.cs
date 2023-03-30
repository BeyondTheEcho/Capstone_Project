using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditList : MonoBehaviour
{
    [SerializeField] GameObject[] counts;
    [SerializeField] float timeBetween = 3.5f; // Time in seconds

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
        for (int i=0; i< counts.Length; i++)
        {
            counts[i].SetActive(true);
            yield return new WaitForSeconds(timeBetween); // Waits for the time set in timeBetween, affected by timeScale.
            if (i < counts.Length - 1)
            {
                counts[i].SetActive(false);

            }
        }
    }
}
