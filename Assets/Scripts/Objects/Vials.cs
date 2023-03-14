using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Vials : InteractableBase, IDropable
{
    public VialColor m_VialColor 
    {
        get 
        { 
            return m_BackingVialColor; 
        }
        set 
        { 
            m_BackingVialColor = value;

            m_Renderer.sprite = m_BackingVialColor switch
            {
                VialColor.Empty => m_VialEmpty,
                VialColor.Filled => m_VialFilled,
                VialColor.Red => m_VialFilledRed,
                VialColor.Green => m_VialFilledGreen,
                VialColor.Blue => m_VialFilledBlue,
                _ => throw new InvalidOperationException($"Unknown VialColor {value}")
            };
        }  
    }

    private VialColor m_BackingVialColor = VialColor.Empty;

    public Sprite m_VialEmpty;
    public Sprite m_VialFilled;
    public Sprite m_VialFilledRed;
    public Sprite m_VialFilledGreen;
    public Sprite m_VialFilledBlue;

    private SpriteRenderer m_Renderer;

    private void Awake()
    {
        m_Renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnDrop(Player player)
    {
        player.m_HeldItem = null;

        gameObject.transform.parent = null;

        gameObject.SetActive(true);

        StoreRef();
    }

    public override void OnInteract(Player player)
    {
        player.m_HeldItem = this;

        transform.position = player.transform.position;
        transform.parent = player.transform;

        DeleteRef();

        gameObject.SetActive(false);
    }

    public override string ReturnTextPrompt()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        StoreRef();
    }
    
    public void AttachItemToPlayer(Player player)
    {
        player.m_HeldItem = this;
    }

    private void OnDestroy()
    {
        DeleteRef();
        RemoveDropableFromList();
    }

    public void RemoveDropableFromList()
    {
        InteractablesManager.s_Instance.RemoveIDropable(this);
    }

    public void AddDropableToList()
    {
        InteractablesManager.s_Instance.AddIDropable(this);
    }

    public enum VialColor
    {
        Empty,
        Filled,
        Red,
        Green,
        Blue
    }
}
