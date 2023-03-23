using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] m_Players;
    public static bool[] m_PlayersAdded = new bool[4];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_Players.Length; i++)
        {
            m_PlayersAdded[i] = false;
        }

        InputManager.s_Instance.AddPlayerFunc = AddPlayer;
        InputManager.s_Instance.RemovePlayerFunc = RemovePlayer;
    }

    public void AddPlayer(int playerNumber)
    {
        if (!m_PlayersAdded[playerNumber])
        {
            m_PlayersAdded[playerNumber] = true;
            m_Players[playerNumber].SetActive(true);
        }
    }

    public void RemovePlayer(int playerNumberR) 
    {
        if (m_PlayersAdded[playerNumberR])
        {
            m_PlayersAdded[playerNumberR] = false;
            m_Players[playerNumberR].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
