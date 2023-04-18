using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public GameObject[] m_PlayerObjects;
    // Start is called before the first frame update
    void Start()
    {
        int playerCount = 0;
        for (int i = 0; i < GameManager.m_PlayersAdded.Length; i++)
        {
            if (GameManager.m_PlayersAdded[i])
            {
                playerCount++;
                m_PlayerObjects[i].SetActive(true);
            }
           
        }
        if (playerCount <= 0)
        {
            m_PlayerObjects[0].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
