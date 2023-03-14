using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vials : InteractableBase, IDropable
{
    public void OnDrop(Player player)
    {
        player.m_HeldItem = null;

        gameObject.transform.parent = null;

        gameObject.SetActive(true);

        StoreRef();
    }

    public override void OnInteract(Player player)
    {
        player.m_HeldItem = this;

        transform.position = player.transform.position;
        transform.parent = player.transform;

        DeleteRef();

        gameObject.SetActive(false);
    }

    public override string ReturnTextPrompt()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        StoreRef();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AttachItemToPlayer(Player player)
    {
        player.m_HeldItem = this;
    }

    private void OnDestroy()
    {
        DeleteRef();
        RemoveDropableFromList();
    }

    public void RemoveDropableFromList()
    {
        InteractablesManager.s_Instance.RemoveIDropable(this);
    }

    public void AddDropableToList()
    {
        InteractablesManager.s_Instance.AddIDropable(this);
    }
}
