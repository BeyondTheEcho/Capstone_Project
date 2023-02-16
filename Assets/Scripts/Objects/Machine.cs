using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (gameObject.tag == "Press")
            {
                SoundManager.s_Instance.PlayClamp();
            }
            if (gameObject.tag == "Heat")
            {
                SoundManager.s_Instance.PlayHeatTreat();
            }
            if (gameObject.tag == "Bolt")
            {
                SoundManager.s_Instance.PlayBolt();
            }
            yield return new WaitForSeconds(m_MachineProcessingDelay);
            item.ProgressItemStage();
            input.ReturnItemToBelt(item.gameObject);
            
            
        }
        else
        {
            SoundManager.s_Instance.PlayAlarm();
        }

        
    }
}