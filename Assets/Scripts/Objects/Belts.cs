using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Belts : InteractableBase
{
    public Transform m_NextWaypoint;

    void Start()
    {
        StoreRef();
        InteractablesManager.s_Instance.AddConveyorBelt(this);
    }

    public void PlaceItemOnBelt(Player player)
    {
        MonoBehaviour mono = (MonoBehaviour) player.m_HeldItem;

        InteractableBase interactable = (InteractableBase) player.m_HeldItem;

        if (mono.TryGetComponent(out Transform itemTransform))
        {
            InteractablesManager.s_Instance.AddItemToBelt(interactable);

            itemTransform.parent = null;

            itemTransform.position = gameObject.transform.position;

            player.m_HeldItem = null;

            itemTransform.gameObject.SetActive(true);

            interactable.StoreRef();
        }
    }

    public void PlaceItemOnBeltSystem(GameObject item)
    {
        InteractablesManager.s_Instance.AddItemToBelt(item.GetComponent<InteractableBase>());

        item.transform.position = gameObject.transform.position;
    }

    public override void OnInteract(Player player)
    {
        // Does Nothing For Now
    }

    public override string ReturnTextPrompt()
    {
        return $"Press 'F' to add or remove an object from the belt";
    }

    private void OnDestroy()
    {
        DeleteRef();
        InteractablesManager.s_Instance.RemoveConveyorBelt(this);
    }
}
