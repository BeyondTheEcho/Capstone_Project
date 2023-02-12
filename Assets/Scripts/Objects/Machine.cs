using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Machine : MonoBehaviour
{
    [SerializeField] private Transform m_OutputBeltTransform;
    [SerializeField] private int m_MachineProcessingStage = 0;
    private float m_BeltMoveSpeed = 0.5f;
    private float m_MachineProcessingDelay = 3.5f;

    public void RunMachine(Collider2D col)
    {
        StartCoroutine(ProcessOutputItem(col));
    }

    IEnumerator ProcessOutputItem(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Item item))
        {
            while (true)
            {
                if (item.transform.position == m_OutputBeltTransform.position)
                {
                    break;
                }

                if (item.GetItemStage() == m_MachineProcessingStage)
                {
                    yield return new WaitForSeconds(m_MachineProcessingDelay);

                    item.ProgressItemStage();
                }

                item.transform.position = Vector3.MoveTowards(item.transform.position, m_OutputBeltTransform.position, m_BeltMoveSpeed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (true)
            {
                if (col.transform.position == m_OutputBeltTransform.position)
                {
                    break;
                }

                col.transform.position = Vector3.MoveTowards(col.transform.position, m_OutputBeltTransform.position, m_BeltMoveSpeed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}