using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Playables;
using TMPro;
using System.Linq;
using System;

public class Player : MonoBehaviour
{
    //Public Vars
    [SerializeField] public IDropable m_HeldItem = null;

    //Private Serialized Vars
    [SerializeField] private float m_InteractRange = 1.7f; //Default Value lines up with colliders

    //Private Vars
    private ToolManager m_ToolManager;
    private TMP_Text m_ToolPrompt;
    private PlayerNumber m_PlayerNumber;

    void Awake()
    {
        m_ToolPrompt = GetComponentInChildren<TMP_Text>();
        m_ToolManager = FindObjectOfType<ToolManager>();
    }

    void Start()
    {
        m_PlayerNumber = gameObject.GetComponent<PlayerController>().m_PlayerNumber;

        if(m_PlayerNumber == PlayerNumber.PlayerOne)
        {
            InputManager.s_Instance.Player_1_Interact += PlayerInteract;
            InputManager.s_Instance.Player_1_Drop += PlayerDrop;
        }
        else if(m_PlayerNumber == PlayerNumber.PlayerTwo)
        {
            InputManager.s_Instance.Player_2_Interact += PlayerInteract;
            InputManager.s_Instance.Player_2_Drop += PlayerDrop;
        }
        else if (m_PlayerNumber == PlayerNumber.PlayerThree)
        {
            InputManager.s_Instance.Player_3_Interact += PlayerInteract;
            InputManager.s_Instance.Player_3_Drop += PlayerDrop;
        }
        else if (m_PlayerNumber == PlayerNumber.PlayerFour)
        {
            InputManager.s_Instance.Player_4_Interact += PlayerInteract;
            InputManager.s_Instance.Player_4_Drop += PlayerDrop;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out InteractableBase interactable))
        {         
            InteractablesManager.s_Instance.AddPromptObject(this, interactable);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out InteractableBase interactable))
        {
            InteractablesManager.s_Instance.RemovePromptObject(this, interactable);
        }
    }

    public void UpdatePrompt(string prompt)
    {
        m_ToolPrompt.text = prompt;
    }

    public void PlayerInteract()
    {
        InteractableBase item = null;

        item = InteractablesManager.s_Instance.GetClosestInteractableInRange(this, m_InteractRange);

        if (item != null)
        {
            if (item.TryGetComponent(out Belts belt))
            {
                item = InteractablesManager.s_Instance.GetClosestPickupableInRange(this, m_InteractRange);
            }

            if (item != null)
            {
                if (InteractablesManager.s_Instance.m_ObjectOnConveyors.Contains(item))
                {
                    InteractablesManager.s_Instance.m_ObjectOnConveyors.Remove(item);
                }

                item.OnInteract(this);
            }
        }
    }

    public void PlayerDrop()
    {
        if (m_HeldItem != null)
        {
            InteractableBase item = null;

            item = InteractablesManager.s_Instance.GetClosestBeltInRange(this, m_InteractRange);

            if (item != null)
            {
                item.GetComponent<Belts>().PlaceItemOnBelt(this);
            }
            else
            {
                m_HeldItem.OnDrop(this);
            }
        }
    }
}
