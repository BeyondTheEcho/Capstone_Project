using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class OrderManager : MonoBehaviour
{
    //Difficulty (Defaulted until main menu branch is merged)
    private Difficulty m_Difficulty = Difficulty.medium;

    //Order Vars
    private int m_BaseOrderSize = 10;
    private float m_OrderSize;

    //Chaos Vars
    private float m_Chaos = 0.0f;
    private float m_ChaosIncrement = 0.01f;
    private float m_ChaosIncrementRate = 3.0f;
    Coroutine m_ChaosCoroutine;

    void Start()
    {
        m_ChaosCoroutine = StartCoroutine(IncrementChaos());
    }

    void Update()
    {

    }

    IEnumerator IncrementChaos()
    {
        yield return new WaitForSeconds(m_ChaosIncrementRate);

        m_Chaos += m_ChaosIncrement;
    }

    public void CalculateOrderSize()
    {
        m_OrderSize = (m_BaseOrderSize * (int)m_Difficulty) * m_Chaos;
    }

    private enum Difficulty
    {
        easy = 1,
        medium = 2,
        hard = 3
    }
}