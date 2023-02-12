using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MachineInput : MonoBehaviour
{
    [SerializeField] private Machine m_Machine;
    [SerializeField] private Transform m_MidBelt;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Item item))
        {
            m_Machine.RunMachine(item, this);
            RemoveItemFromBelt(col.gameObject);
        }
    }
    private void RemoveItemFromBelt(GameObject go)
    {
        InteractablesManager.s_Instance.RemoveItemFromBelt(go.GetComponent<InteractableBase>());
    }

    public void ReturnItemToBelt(GameObject go)
    {
        go.transform.position = m_MidBelt.position;
        InteractablesManager.s_Instance.AddItemToBelt(go.GetComponent<InteractableBase>());
    }
}
