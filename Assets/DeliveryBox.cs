using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBox : InteractableBase
{
    // Start is called before the first frame update
    void Start()
    {
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
            //if (vial.GetVialSprite() == OrderManager.s_Instance.GetCurrentOrderSprite())
            //{
            //    OrderManager.s_Instance.SubtractOrderItem();
            //    Destroy(vial);
            //    player.m_HeldItem = null;
            //}
        }
    }

    public override string ReturnTextPrompt()
    {
        return "Press F to deliver vial";
    }

    private void OnDestroy()
    {
        DeleteRef();
    }
}
