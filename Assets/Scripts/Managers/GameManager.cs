using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private static GameObject s_InstGO;

    private static GameManager s_Inst = null;

    public static GameManager s_Instance
    { 
        get
        {
            if (s_Inst == null)
            {
                s_InstGO = new GameObject("GameManager");
                s_InstGO.AddComponent <GameManager>();    
            }
                
            return s_Inst;
        } 
    }

    public GameObject[] m_Players;
    public static bool[] m_PlayersAdded = new bool[4];

    //vars for score system
    private int m_Score = 0;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        s_Inst = this;

        if (s_InstGO == null)
        {
            s_InstGO = GameObject.Find("GameManager");
        }

        if (s_InstGO != null)
        {
            DontDestroyOnLoad(s_InstGO);
        }
    }

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

    public void UpdateScore(int scoreToAdd)
    {
        m_Score += scoreToAdd;
    }

    public int GetScore()
    {
        return m_Score;
    }


    // Update is called once per frame
    void Update()
    {
      
    }
}
