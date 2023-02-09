using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : InteractableBase, IDropable
{
    [SerializeField] private ItemType m_ItemType;

    //Iron Sprites
    [SerializeField] private Sprite[] m_IronSprites;

    private int m_CurrentItemStage = 0;
    private int m_MaxItemStage = 2;
    private SpriteRenderer m_SpriteRenderer;

    void Awake()
    {
        StoreRef();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        UpdateItem();
    }

    private void UpdateItem()
    {
        if (m_ItemType == ItemType.IronPlate)
        {
            m_SpriteRenderer.sprite = m_IronSprites[m_CurrentItemStage];
        }
    }

    public void ProgressItemStage()
    {
        if (m_CurrentItemStage != m_MaxItemStage)
        {
            m_CurrentItemStage++;

            UpdateItem();
        }
    }

    public override void OnInteract(Player player)
    {
        player.m_HeldItem = this;

        gameObject.transform.position = player.transform.position;
        gameObject.transform.parent = player.transform;

        gameObject.SetActive(false);
    }

    public override string ReturnTextPrompt()
    {
        return $"Press 'F' to pickup {m_ItemType.ToString()}.";
    }

    private enum ItemType
    {
        IronPlate,
    }

    public void OnDrop(Player player)
    {
        player.m_HeldItem = null;

        gameObject.transform.parent = null;

        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        DeleteRef();
    }
}
