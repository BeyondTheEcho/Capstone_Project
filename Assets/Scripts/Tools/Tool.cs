using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tool : InteractableBase
{
    [SerializeField] private float m_DespawnTime = 30.0f;
    [SerializeField] private ToolType m_ToolType;

    void Awake()
    {
        StoreRef();
    }

    void Start()
    {
        Destroy(gameObject, m_DespawnTime);
    }

    public override void OnInteract(Player player)
    {
        player.m_CurrentTool = m_ToolType;
        DestroyTool();
    }

    public void DestroyTool()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        DeleteRef();
    }

    public override string ReturnTextPrompt()
    {
        return $"Press 'F' to pickup the {m_ToolType.ToString()}.";
    }
}
