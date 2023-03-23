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
    [SerializeField] private Sprite[] m_OrderSprites;
    [SerializeField] private Order[] m_Orders;
    [SerializeField] private GameObject m_OrderPrefab;
    private int m_MaxOrderSize = 5;
    private int m_MinOrderSize = 1;
    private float m_OrderOffsetPosition = 5.0f;
    private Vector3 m_InitialOrderPosition = new Vector3(-20.0f, 10.5f, 0.0f);
    private int m_OrderSpawnDelay = 15;

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
            AddOrder();

            yield return new WaitForSeconds(m_OrderSpawnDelay);
        }
    }

    private void AddOrder()
    {
        for (int i = 0;  i < m_Orders.Length; i++)
        {
            if (m_Orders[i] == null)
            {
                Vector3 pos = m_InitialOrderPosition;

                pos.x += m_OrderOffsetPosition * i;

                var obj = Instantiate(m_OrderPrefab, pos, Quaternion.identity);

                m_Orders[i] = obj.GetComponent<Order>();

                InitializeOrder(m_Orders[i]);

                break;
            }
        }
    }

    private void InitializeOrder(Order order)
    {
        order.SetOrderSprite(m_OrderSprites[(int)Random.Range(0, m_OrderSprites.Length)]);

        order.SetOrderQuantity((int)Random.Range(m_MinOrderSize, m_MaxOrderSize));
    }

    public bool TryDeliverVial(Sprite sprite)
    {
        for (int i = m_Orders.Length - 1; i >= 0; i--) 
        {
            if (m_Orders[i]  != null)
            {
                if (m_Orders[i].TryDeliverVial(sprite)) { return true; }
            }
        }

        return false;
    }

    public void RemoveOrder(Order order)
    {
        foreach (var item in m_Orders)
        {
            if (item == order)
            {
                Destroy(item.gameObject);
                return;
            }
        }
    }
}