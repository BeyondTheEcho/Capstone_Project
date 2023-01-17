using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Playables;
using TMPro;

public class Player : MonoBehaviour
{
    private ToolManager m_ToolManager;

    private ToolType? m_CurrentTool = null;
    private TMP_Text m_ToolPrompt;

    private bool m_InToolRange = false;
    private Tool m_ToolRef;

    void Awake()
    {
        m_ToolPrompt = GetComponentInChildren<TMP_Text>();
        m_ToolManager = FindObjectOfType<ToolManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (m_InToolRange)
            {
                m_CurrentTool = m_ToolRef.ReturnToolType();
                m_ToolRef.DestroyTool();
                Debug.Log(m_CurrentTool.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            DropTool();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Tool tool))
        {
            //sets flag / stores ref
            m_InToolRange = true;
            m_ToolRef = tool;

            //Updates the tool prompt above the player
            m_ToolPrompt.enabled = true;
            m_ToolPrompt.text = $"Press F to pickup the {tool.ReturnToolType().ToString()}.";
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Tool tool))
        {
            //Sets flag
            m_InToolRange = false;

            //Updates the tool prompt above the player
            m_ToolPrompt.text = "";
            m_ToolPrompt.enabled = false;
        }
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

    public ToolType? GetCurrentTool()
    {
        return m_CurrentTool;
    }
}
