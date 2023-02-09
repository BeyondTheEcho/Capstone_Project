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
    public IDropable m_HeldItem;

    //Private Serialized Vars
    [SerializeField] private float m_InteractRange = 1.7f; //Default Value lines up with colliders

    //Private Vars
    private ToolManager m_ToolManager;
    private TMP_Text m_ToolPrompt;

    void Awake()
    {
        m_ToolPrompt = GetComponentInChildren<TMP_Text>();
        m_ToolManager = FindObjectOfType<ToolManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("PickUp"))
        {
            InteractableBase item = null;
                
            item = InteractablesManager.s_Instance.ReturnClosestInteractableInRange(this, m_InteractRange);

            if (item != null)
            {
                if (item.TryGetComponent(out Belts belt))
                {
                    item = InteractablesManager.s_Instance.ReturnClosestPickupableInRange(this, m_InteractRange);
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

        if ((Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown("Drop")))
        {
            if (m_HeldItem != null)
            {
                InteractableBase item = null;

                item = InteractablesManager.s_Instance.ReturnClosestBeltInRange(this, m_InteractRange);

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

}
