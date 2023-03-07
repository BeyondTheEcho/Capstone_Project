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
    private string m_GetText;

    void Start()
    {
        LanguageSettings.ChangeFont(LanguageSettings.m_FinalFont);
        StoreRef();
        AddDropableToList();
        m_DespawnCoroutine = StartCoroutine(DespawnCountdown());
    }

    private IEnumerator DespawnCountdown()
    {
        yield return new WaitForSeconds(m_DespawnTime);
        Destroy(gameObject);
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
        RemoveDropableFromList();
    }

    public override string ReturnTextPrompt()
    {
        m_GetText = string.Format(LanguageSettings.s_Instance.GetLocalizedString("PICK"), LanguageSettings.s_Instance.GetLocalizedString(m_ToolType.ToString())) ;
        
        return m_GetText;
    }

    public void OnDrop(Player player)
    {
        player.m_HeldItem = null;

        gameObject.transform.parent = null;

        gameObject.SetActive(true);

        StoreRef();

        m_DespawnCoroutine = StartCoroutine(DespawnCountdown());
    }

    public void AddDropableToList()
    {
        InteractablesManager.s_Instance.AddIDropable(this);
    }

    public void RemoveDropableFromList()
    {
        InteractablesManager.s_Instance.RemoveIDropable(this);
    }
}
