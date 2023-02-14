using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] m_Players;
    private int m_PlayersCount;
    // Start is called before the first frame update
    void Start()
    {
        m_PlayersCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (m_PlayersCount <= 3)
            {
                m_Players[m_PlayersCount].SetActive(true);

                m_PlayersCount++;
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (m_PlayersCount >= 1)
            {
                m_PlayersCount--;
                m_Players[m_PlayersCount].SetActive(false);
            }
        }
    }
}
