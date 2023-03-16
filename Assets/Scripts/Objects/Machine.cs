using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Machine : InteractableBase
{
    private float m_MachineProcessingDelay = 3.5f;
    private bool m_VialIsReady = false;
    private Vials m_Vial = null;

    private void Start()
    {
        StoreRef();
    }

    IEnumerator FillVial(Vials vial)
    {
        vial.transform.position = transform.position;
        vial.transform.SetParent(transform);

        yield return new WaitForSeconds(m_MachineProcessingDelay);

        vial.m_VialColor = Vials.VialColor.Filled;

        m_Vial = vial;
        m_VialIsReady = true;
    }

    public override void OnInteract(Player player)
    {
        if (m_VialIsReady == false)
        {
            InputVial(player);
        }
        else
        {
            OutputVial(player);
        }
        else
        {
            SoundManager.s_Instance.PlayAlarm();
        }

        
    }

    private void InputVial(Player player)
    {
        if (player.m_HeldItem == null) { return; }

        if (player.m_HeldItem.gameObject.TryGetComponent<Vials>(out Vials vial))
        {
            if (vial.m_VialColor != Vials.VialColor.Empty) { return; }

            player.m_HeldItem = null;

            StartCoroutine(FillVial(vial));
        }

        return;
    }

    private void OutputVial(Player player)
    {
        if (player.m_HeldItem != null) { return; }

        player.m_HeldItem = m_Vial;

        m_Vial.transform.position = player.transform.position;
        m_Vial.transform.parent = player.transform;

        m_Vial = null;
        m_VialIsReady = false;
    }

    public override string ReturnTextPrompt()
    {
        throw new System.NotImplementedException();
    }

    private void OnDestroy()
    {
        DeleteRef();
    }
}