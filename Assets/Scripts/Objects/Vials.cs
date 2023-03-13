using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vials : InteractableBase, IDropable
{
    public void OnDrop(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void OnInteract(Player player)
    {
        throw new System.NotImplementedException();
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
        Debug.Log(player.m_HeldItem);
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
