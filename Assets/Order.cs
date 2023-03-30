using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    [SerializeField] private Image m_TimerBar;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private TMP_Text m_OrderQuantityText;

    private float m_FillValue;
    private int m_OrderQuantity;
    private float m_MaxTime = 30.0f;
    private float m_TimeRemaining;
    private float m_TimePerVial = 20.0f;

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
            OrderManager.s_Instance.RemoveOrder(this);
        }

        m_TimeRemaining -= Time.deltaTime;

        m_FillValue = m_TimeRemaining / m_MaxTime;

        m_TimerBar.fillAmount = m_FillValue;
        m_OrderQuantityText.text = $"x {m_OrderQuantity}";
    }

    public void SetOrderSprite(Sprite s)
    {
        m_SpriteRenderer.sprite = s;
    }

    public void SetOrderQuantity(int value) 
    {
        m_OrderQuantity = value;
        m_MaxTime = m_TimePerVial * m_OrderQuantity;
    }

    public bool TryDeliverVial(Sprite sprite)
    {
        if (sprite == m_SpriteRenderer.sprite)
        {
            m_OrderQuantity--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
