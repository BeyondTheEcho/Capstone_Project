using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Vials;

public class Order : MonoBehaviour
{
    [SerializeField] private Image m_TimerBar;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private TMP_Text m_OrderQuantityText;
    [SerializeField] private int m_MissedOrderPenalty;

    private float m_FillValue;
    private int m_OrderQuantity;
    private float m_MaxTime = 120.0f;
    private float m_TimeRemaining;
    private float m_TimePerVial = 35.0f;
    private int m_OrderValue = 50;

    public VialColor m_VialColor
    {
        get
        {
            return m_BackingVialColor;
        }
        set
        {
            m_BackingVialColor = value;

            m_SpriteRenderer.sprite = OrderManager.GetSprite(value);
        }
    }

    private VialColor m_BackingVialColor = VialColor.Empty;

    private void Start()
    {
        m_TimeRemaining = m_MaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_OrderQuantity == 0)
        {
            OrderManager.s_Instance.RemoveOrder(this);
        }

        if (m_TimeRemaining <= 0)
        {
            GameManager.s_Instance.UpdateScoreMinus(m_MissedOrderPenalty);
            OrderManager.s_Instance.RemoveOrder(this);
        }

        m_TimeRemaining -= Time.deltaTime;

        m_FillValue = m_TimeRemaining / m_MaxTime;

        m_TimerBar.fillAmount = m_FillValue;
        m_OrderQuantityText.text = $"x {m_OrderQuantity}";
    }

    public void SetOrderQuantity(int value) 
    {
        m_OrderQuantity = value;
        m_MaxTime = m_TimePerVial * m_OrderQuantity;
    }

    public bool TryDeliverVial(VialColor color)
    {
        if (color == m_VialColor)
        {
            m_OrderQuantity--;

            if (m_OrderQuantity <= 0)
            {
                GameManager.s_Instance.UpdateScore(m_OrderValue);
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
