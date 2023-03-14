using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : InteractableBase
{
    private float m_MachineProcessingDelay = 3.5f;

    IEnumerator FillVial(Vials vial)
    {
        yield return new WaitForSeconds(m_MachineProcessingDelay);
    }

    public override void OnInteract(Player player)
    {
        if (player.m_HeldItem == null) { return; }

        if (player.m_HeldItem.gameObject.TryGetComponent<Vials>(out Vials vial))
        {
            if (vial.m_VialStage != Vials.VialStage.Empty) { return; }

            StartCoroutine(FillVial(vial));
        }

        return;
    }

    public override string ReturnTextPrompt()
    {
        throw new System.NotImplementedException();
    }
}