using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditList : MonoBehaviour
{
    public GameObject[] counts;
    public float timeBetween = 3.5f; // Time in seconds

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
        foreach (GameObject count in counts)
        {
            count.SetActive(true);
            yield return new WaitForSeconds(timeBetween); // Waits for the time set in timeBetween, affected by timeScale.
            count.SetActive(false);
        }
    }
}
