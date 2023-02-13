using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    //Difficulty (Defaulted until main menu branch is merged)
    private Difficulty m_Difficulty = Difficulty.medium;

    //Static Instance
    public static OrderManager s_Instance { get; private set; }

    //Order Vars
    [Header("Order Settings")]
    [SerializeField] private GameObject[] m_OrderItems;
    [SerializeField] private GameObject m_OrderSpawnBelt;
    private int m_BaseOrderSize = 10;
    private int m_CurrentOrderSize = 0;
    private int m_MaxOrderSize = 0;
    private int m_CurrentOrderItem;
    private float m_OrderSpawnDelay = 3f;

    //Chaos Vars
    private float m_Chaos = 0.0f;
    private float m_ChaosIncrement = 0.01f;
    private float m_ChaosIncrementRate = 3.0f;
    Coroutine m_ChaosCoroutine;

    //UI Vars
    [Header("UI Settings")]
    [SerializeField] private TMP_Text m_ChaosText;
    [SerializeField] private TMP_Text m_OrderText;

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
        m_ChaosCoroutine = StartCoroutine(IncrementChaos());
    }

    void Update()
    {
        UpdateUI();

        if (m_CurrentOrderSize == 0 && PauseFeature.m_InPause == false)
        {
            GenerateOrder();
        }
    }

    private IEnumerator SpawnOrder(int items)
    {
        for (int i = items; i != 0; i--)
        {
            yield return new WaitForSeconds(m_OrderSpawnDelay);

            GameObject item = Instantiate(m_OrderItems[m_CurrentOrderItem], m_OrderSpawnBelt.transform.position, Quaternion.identity);

            m_OrderSpawnBelt.GetComponent<Belts>().PlaceItemOnBeltSystem(item);
        }
    }

    private void GenerateOrder()
    {
        CalculateMaxOrderSize();

        m_CurrentOrderSize = Random.Range(1, m_MaxOrderSize);

        int orderItemMax = m_OrderItems.Length - 1;

        m_CurrentOrderItem = Random.Range(0, orderItemMax);

        StartCoroutine(SpawnOrder(m_CurrentOrderSize));
    }

    public void SubtractOrderItem()
    {
        m_CurrentOrderSize--;
    }

    public ItemType GetCurrentOrderItemType()
    {
        m_OrderItems[m_CurrentOrderItem].TryGetComponent(out Item item);
        return item.GetItemType();
    }

    private void UpdateUI()
    {
        m_ChaosText.text = $"Chaos: {m_Chaos}";
        m_OrderText.text = $"Order Size: {m_CurrentOrderSize}";
    }

    IEnumerator IncrementChaos()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_ChaosIncrementRate);

            m_Chaos += m_ChaosIncrement;
        }
    }

    public void CalculateMaxOrderSize()
    {
        m_MaxOrderSize = (int)((m_BaseOrderSize * (int) m_Difficulty) * m_Chaos);
    }

    private enum Difficulty
    {
        easy = 1,
        medium = 2,
        hard = 3
    }
}