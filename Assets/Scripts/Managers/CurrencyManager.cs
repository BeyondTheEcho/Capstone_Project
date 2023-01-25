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
        Instance.m_CurrencyText.text = Instance.m_TotalCurrency.ToString();        
    }

    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            AddCurrency();
        }
        if (Input.GetKeyDown("p"))
        {
            RemoveCurrency();
        }
    }

    private void AddCurrency()
    {
        m_TotalCurrency += 5;
        m_CurrencyText.text = m_TotalCurrency.ToString();
    }
    private void RemoveCurrency()
    {
        m_TotalCurrency -= 5;
        m_CurrencyText.text = m_TotalCurrency.ToString();
    }
}
