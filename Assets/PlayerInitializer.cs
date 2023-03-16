using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public GameObject[] m_PlayerObjects;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameManager.m_PlayersCount; i++)
        {
            m_PlayerObjects[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
