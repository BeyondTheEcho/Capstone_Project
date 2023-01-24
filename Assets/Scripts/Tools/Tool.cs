using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tool : InteractableBase
{
    [SerializeField] private float m_DespawnTime = 30.0f;
    [SerializeField] private ToolType m_ToolType;
    public GameObject getLocalizationText;
    private string getText;

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
        getText = getLocalizationText.GetComponent<LocalizationManager>().GetPickUpHintText();
        return getText + $" {m_ToolType.ToString()}.";
    }
}
