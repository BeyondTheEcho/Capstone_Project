using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalyerTat : MonoBehaviour
{
    public GameObject[] m_playersTat;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameManager.m_PlayersCount; i++)
        {
            m_playersTat[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
