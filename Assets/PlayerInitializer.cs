using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public GameObject[] m_PlayerObjects;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameManager.m_PlayersAdded.Length; i++)
        {
            m_PlayerObjects[i].SetActive(GameManager.m_PlayersAdded[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
