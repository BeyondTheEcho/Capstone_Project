using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    [SerializeField] public GameObject[] m_Tools;
    [SerializeField] private GameObject[] m_ToolSpawns;


    void Start()
    {
        InitialToolSpawn();
    }

    void InitialToolSpawn()
    {
        int i = 0;

        foreach (var tool in m_Tools)
        {
            Instantiate(tool, m_ToolSpawns[i].transform);
            
            i++;
        }
    }

}
