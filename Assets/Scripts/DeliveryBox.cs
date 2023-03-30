using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryBox : InteractableBase
{
    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.enabled = false;
        StoreRef();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteract(Player player)
    {
        if (player.m_HeldItem == null) { return; }

        if (player.m_HeldItem.gameObject.TryGetComponent<Vials>(out Vials vial)) 
        {
            if (OrderManager.s_Instance.TryDeliverVial(vial.m_VialColor))
            {
                Destroy(vial);
                player.m_HeldItem = null;
            }
        }
    }

    public override string ReturnTextPrompt()
    {
        return LanguageSettings.s_Instance.GetLocalizedString("PickUp");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        m_Animator.enabled = true;
        m_Animator.Play("ChestOpen");
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        m_Animator.Play("ChestClose");
    }

    private void OnDestroy()
    {
        DeleteRef();
    }
}
