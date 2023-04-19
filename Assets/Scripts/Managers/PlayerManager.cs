using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject m_PlayerPrefab;

    [Header("SpawnPositions")]
    [SerializeField] private Transform[] m_SpawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        for (int playerIndex = 0; playerIndex < GameManager.m_PlayersAdded.Length; playerIndex++)
        {
            if (GameManager.m_PlayersAdded[playerIndex])
            {
                var player = Instantiate(m_PlayerPrefab, m_SpawnPositions[playerIndex]);
                player.GetComponent<Player>().SetupPlayer((PlayerNumber)playerIndex);
            }
        }
    }
}
