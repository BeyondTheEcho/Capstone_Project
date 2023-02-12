using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineInput : MonoBehaviour
{
    [SerializeField] private Machine m_Machine;

    private void OnTriggerEnter2D(Collider2D col)
    {
        m_Machine.RunMachine(col);
    }
}
