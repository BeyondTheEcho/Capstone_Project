using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Belts : InteractableBase
{
    public Transform m_NextWaypoint;

    void Awake()
    {
        StoreRef();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceItemOnBelt(Player player)
    {
        MonoBehaviour mono = (MonoBehaviour) player.m_HeldItem;

        InteractableBase interactable = (InteractableBase) player.m_HeldItem;

        if (mono.TryGetComponent(out Transform itemTransform))
        {
            itemTransform.parent = null;

            itemTransform.position = gameObject.transform.position;

            player.m_HeldItem = null;

            itemTransform.gameObject.SetActive(true);

            interactable.StoreRef();
        }
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
    }
}
