using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tool : InteractableBase, IDropable
{
    [SerializeField] private float m_DespawnTime = 30.0f;
    [SerializeField] private ToolType m_ToolType;
    private Coroutine m_DespawnCoroutine;
    private string m_getText;
    void Awake()
    {
        StoreRef();
    }

    void Start()
    {
        m_DespawnCoroutine = StartCoroutine(DespawnCountdown());
    }

    private IEnumerator DespawnCountdown()
    {
        yield return new WaitForSeconds(m_DespawnTime);
        Destroy(this);
    }

    public override void OnInteract(Player player)
    {
        StopCoroutine(m_DespawnCoroutine);

        player.m_HeldItem = this;

        gameObject.transform.position = player.transform.position;
        gameObject.transform.parent = player.transform;

        DeleteRef();

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        DeleteRef();
    }

    public override string ReturnTextPrompt()
    {
        m_getText = LanguageSettings.s_Instance.GetLocalizedString($"Press 'F' to pickup the ");
        
        return m_getText + $" {m_ToolType.ToString()}.";
    }

    public void OnDrop(Player player)
    {
        player.m_HeldItem = null;

        gameObject.transform.parent = null;

        gameObject.SetActive(true);

        StoreRef();

        m_DespawnCoroutine = StartCoroutine(DespawnCountdown());
    }
}
