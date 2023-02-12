using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DropOffManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Item item))
        {
            if (item.GetItemType() == OrderManager.s_Instance.GetCurrentOrderItemType())
            {
                if (item.GetItemStage() == item.GetItemMaxStage())
                {
                    OrderManager.s_Instance.SubtractOrderItem();
                }
            }
            
            ClearItem(col.gameObject);
        }
        else
        {
            ClearItem(col.gameObject);
        }
    }

    private void ClearItem(GameObject go)
    {
        InteractablesManager.s_Instance.RemoveItemFromBelt(go.GetComponent<InteractableBase>());
        Destroy(go);
    }
}
