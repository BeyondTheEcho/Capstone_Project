using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    [SerializeField] private Image m_TimerBar;
    [SerializeField] private SpriteRenderer m_OrderItemSprite;
    [SerializeField] private TMP_Text m_OrderQuantityText;

    private float m_FillValue;
    private int m_OrderQuantity;

    // Update is called once per frame
    void Update()
    {
        m_TimerBar.fillAmount = m_FillValue;
        m_OrderQuantityText.text = $"x {m_OrderQuantity}";
    }

    public void SetOrderSprite(Sprite s)
    {
        m_OrderItemSprite.sprite = s;
    }

    public void SetOrderQuantity(int value) 
    {
        m_OrderQuantity = value;
    }
}
