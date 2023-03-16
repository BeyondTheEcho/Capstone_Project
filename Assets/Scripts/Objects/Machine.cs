using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Machine : InteractableBase
{
    private float m_MachineProcessingDelay = 3.5f;
    private VialStates m_VialStates = VialStates.Empty;
    private Vials m_Vial = null;
    private string[] m_VialFillStates = {"Filling", "Full"};
    private string m_VialProcessing = "Vial is ";

    //UI Vars
    [Header("UI Settings")]
    [SerializeField] private TMP_Text m_ProcessingText;

    private void Start()
    {
        StoreRef();
    }

    private void Update()
    {
        UpdateUI();
    }

    IEnumerator FillVial(Vials vial)
    {
        m_VialStates = VialStates.Filling;
        
        vial.transform.position = transform.position;
        vial.transform.SetParent(transform);

        yield return new WaitForSeconds(m_MachineProcessingDelay);

        vial.m_VialColor = Vials.VialColor.Filled;

        m_Vial = vial;
        m_VialStates = VialStates.Full;
    }

    public override void OnInteract(Player player)
    {
        if (m_VialStates == VialStates.Empty)
        {
            InputVial(player);
        }
        else if (m_VialStates == VialStates.Full)
        {
            OutputVial(player);
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
        m_VialStates = VialStates.Empty;
    }

    public override string ReturnTextPrompt()
    {
        throw new System.NotImplementedException();
    }

    private void UpdateUI()
    {   
        if (m_VialStates == VialStates.Filling)
        {
            m_ProcessingText.text = $"{m_VialProcessing}{m_VialFillStates[0]}";
        }
        else if (m_VialStates == VialStates.Full)
        {
            m_ProcessingText.text = $"{m_VialProcessing}{m_VialFillStates[1]}";
        }
        else
        {
            m_ProcessingText.text = "";
        }
    }

    private void OnDestroy()
    {
        DeleteRef();
    }

    private enum VialStates
    {
        Empty = 0,
        Filling,
        Full,

        NUM_STATES
    }
}