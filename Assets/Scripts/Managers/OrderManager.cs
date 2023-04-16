using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;
using static Vials;

public class OrderManager : MonoBehaviour
{
    //Static Instance
    public static OrderManager s_Instance { get; private set; }

    //Order Vars
    [Header("Order Settings")]
    [SerializeField] private Order[] m_Orders;
    [SerializeField] private GameObject m_OrderPrefab;
    private int m_MaxOrderSize = 4;
    private int m_MinOrderSize = 1;
    private float m_OrderOffsetPosition = 5.0f;
    private Vector3 m_InitialOrderPosition = new Vector3(-20.0f, 10.5f, 0.0f);
    private int m_OrderSpawnDelay = 15;

    [Header("Vial Sprites")]
    [SerializeField] private Sprite m_VialEmpty;
    [SerializeField] private Sprite m_VialFilled;
    [SerializeField] private Sprite m_VialFilledRed;
    [SerializeField] private Sprite m_VialFilledGreen;
    [SerializeField] private Sprite m_VialFilledBlue;
    [SerializeField] private Sprite m_VialFilledPurple;
    [SerializeField] private Sprite m_VialFilledOrange;
    [SerializeField] private Sprite m_VialFilledTeal;

    [Header("Score Value")]
    public int m_OrderValue;
    public int m_OrderValueMinus;

    public static Sprite GetSprite(VialColor color) => color switch
    {
        VialColor.Empty => s_Instance.m_VialEmpty,
        VialColor.Filled => s_Instance.m_VialFilled,
        VialColor.Red => s_Instance.m_VialFilledRed,
        VialColor.Green => s_Instance.m_VialFilledGreen,
        VialColor.Blue => s_Instance.m_VialFilledBlue,
        VialColor.Purple => s_Instance.m_VialFilledPurple, //red blue = purple
        VialColor.Orange => s_Instance.m_VialFilledOrange, //red green = orange
        VialColor.Teal => s_Instance.m_VialFilledTeal,     //green blue = teal
        _ => throw new InvalidOperationException($"Unknown VialColor {color}")
    };

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
        //Magic number 2 is neccesary to ensure that only color vials are selected for orders
        order.m_VialColor = (VialColor)Random.Range(2, Enum.GetValues(typeof(VialColor)).Length);

        order.SetOrderQuantity((int)Random.Range(m_MinOrderSize, m_MaxOrderSize));
    }

    public bool TryDeliverVial(VialColor color)
    {
        for (int i = m_Orders.Length - 1; i >= 0; i--) 
        {
            if (m_Orders[i] != null)
            {
                if (m_Orders[i].TryDeliverVial(color)) return true;
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
                GameManager.s_Instance.UpdateScore(m_OrderValue);
                return;
            }
        }
    }
}