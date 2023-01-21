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
    public ToolType? m_CurrentTool { get; set; }

    //Private Serialized Vars
    [SerializeField] private float m_InteractRange = 3.0f;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            InteractableBase item = null;
                
            item = InteractablesManager.s_Instance.ReturnClosestInteractableInRange(this, m_InteractRange);

            if (item != null)
            {
                item.OnInteract(this);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            DropTool();
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

    void DropTool()
    {
        int i = 0;

        foreach (var tool in m_ToolManager.m_Tools)
        {
            if (m_ToolManager.m_Tools[i].name == m_CurrentTool.ToString())
            {
                Instantiate(m_ToolManager.m_Tools[i], transform.position, Quaternion.identity);
                m_CurrentTool = null;
            }

            i++;
        }
    }
}
