using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineInput : MonoBehaviour
{
    [SerializeField] private Machine m_Machine;
    [SerializeField] private Transform m_MidBelt;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Item item))
        {
            RemoveItemFromBelt(col.gameObject);
            StartCoroutine(WaitForItem(item, col.gameObject));
        }
    }

    IEnumerator WaitForItem(Item item, GameObject go)
    {
        while (true)
        {
            if (go.transform.position == m_MidBelt.transform.position)
            {
                //m_Machine.RunMachine(item, this);
                RemoveItemFromBelt(go);

                yield break;
            }

            go.transform.position = Vector3.MoveTowards(go.transform.position, m_MidBelt.transform.position, 0.5f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
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
