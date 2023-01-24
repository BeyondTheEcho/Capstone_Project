using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InteractableBase
{
    [SerializeField] private ItemType m_ItemType;

    //Iron Sprites
    [SerializeField] private Sprite[] m_IronSprites;

    private int m_CurrentItemStage = 0;
    private int m_MaxItemStage = 2;
    private SpriteRenderer m_SpriteRenderer;

    void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        UpdateItem();
    }

    void Update()
    {
        
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
        throw new System.NotImplementedException();
    }

    public override string ReturnTextPrompt()
    {
        return $"Press 'F' to pickup {m_ItemType.ToString()}.";
    }

    private enum ItemType
    {
        IronPlate,
    }
}
