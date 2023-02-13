using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Machine : MonoBehaviour
{
    [SerializeField] private Transform m_OutputBeltTransform;
    [SerializeField] private int m_MachineProcessingStage = 0;
    private float m_MachineProcessingDelay = 3.5f;

    public void RunMachine(Item item, MachineInput input)
    {
        StartCoroutine(ProcessOutputItem(item, input));
    }

    IEnumerator ProcessOutputItem(Item item, MachineInput input)
    {
        if (item.GetItemStage() == m_MachineProcessingStage)
        {
            yield return new WaitForSeconds(m_MachineProcessingDelay);
            item.ProgressItemStage();
            input.ReturnItemToBelt(item.gameObject);
        }
    }
}