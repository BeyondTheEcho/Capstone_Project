using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSpawner : MonoBehaviour
{
    [SerializeField] private float m_SpawnRate = 5.0f;
    [SerializeField] private GameObject m_Demon;

    private bool m_GameRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (m_GameRunning)
        {
            Instantiate(m_Demon,gameObject.transform,true);
            yield return new WaitForSeconds(m_SpawnRate);
        }
    }
}
