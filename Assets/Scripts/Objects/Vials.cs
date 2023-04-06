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

            m_Renderer.sprite = OrderManager.GetSprite(value);
        }  
    }

    private VialColor m_BackingVialColor = VialColor.Empty;

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

    //Other code requires that this enum begins indexing at 0
    public enum VialColor
    {
        Empty,
        Filled,
        Red,
        Green,
        Blue
    }
}
