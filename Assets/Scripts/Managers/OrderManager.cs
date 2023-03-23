using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;

public class OrderManager : MonoBehaviour
{
    //Static Instance
    public static OrderManager s_Instance { get; private set; }

    //Order Vars
    [Header("Order Settings")]
    [SerializeField] private Sprite[] m_OrderItems;
    [SerializeField] private Order[] m_Orders;
    [SerializeField] private GameObject m_OrderPrefab;
    private int m_MaxOrderSize = 5;

    private int m_OrderSpawnDelay = 30;

    private void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(this);
        }
        else
        {
            s_Instance = this;
        }
    }

    void Start()
    {
        m_Orders = new Order[m_MaxOrderSize];
        StartCoroutine(OrderGeneration());
    }

    void Update()
    { 
        
    }

    IEnumerator OrderGeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_OrderSpawnDelay);

            AddOrder();
        }
    }

    private void AddOrder()
    {

    }
}