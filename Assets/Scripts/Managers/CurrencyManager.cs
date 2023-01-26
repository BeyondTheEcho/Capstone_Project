using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    public int m_TotalCurrency = 50;

    [SerializeField] private TMP_Text m_CurrencyText;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        m_CurrencyText.text = m_TotalCurrency.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            AddCurrency(5);
        }
        if (Input.GetKeyDown("p"))
        {
            RemoveCurrency(5);
        }
    }

    private void AddCurrency(int amount)
    {
        m_TotalCurrency += amount;
        m_CurrencyText.text = m_TotalCurrency.ToString();
    }
    private void RemoveCurrency(int amount)
    {
        m_TotalCurrency -= amount;
        m_CurrencyText.text = m_TotalCurrency.ToString();
    }
}
