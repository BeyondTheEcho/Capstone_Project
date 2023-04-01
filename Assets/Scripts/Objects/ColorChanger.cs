using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorChanger : InteractableBase
{
    //Machine Settings
    [Header("Machine Settings")]
    [SerializeField] private MachineColor m_MachineColor;

    //UI Vars
    [Header("UI Settings")]
    [SerializeField] private TMP_Text m_ProcessingText;

    private VialStates m_VialStates = VialStates.Unprocessed;
    private Vials m_Vial = null;
    private float m_MachineProcessingDelay = 3.5f;

    private void Start()
    {
        StoreRef();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public override void OnInteract(Player player)
    {
        if (m_VialStates == VialStates.Unprocessed)
        {
            InputVial(player);
        }
        else if (m_VialStates == VialStates.Ready)
        {
            OutputVial(player);
        }
    }

    IEnumerator ChangeColor(Vials vial)
    {
        m_VialStates = VialStates.Processing;

        vial.transform.position = transform.position;
        vial.transform.SetParent(transform);

        yield return new WaitForSeconds(m_MachineProcessingDelay);

        if (m_MachineColor == MachineColor.Blue) { vial.m_VialColor = Vials.VialColor.Blue; }
        if (m_MachineColor == MachineColor.Red) { vial.m_VialColor = Vials.VialColor.Red; }
        if (m_MachineColor == MachineColor.Green) { vial.m_VialColor = Vials.VialColor.Green; }

        m_Vial = vial;
        m_VialStates = VialStates.Ready;
    }

    private void InputVial(Player player)
    {
        if (player.m_HeldItem == null) { return; }

        if (player.m_HeldItem.gameObject.TryGetComponent<Vials>(out Vials vial))
        {
            if (vial.m_VialColor == Vials.VialColor.Empty) { return; }

            player.m_HeldItem = null;

            StartCoroutine(ChangeColor(vial));
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
        m_VialStates = VialStates.Unprocessed;
    }

    private void UpdateUI()
    {
        if (m_VialStates == VialStates.Processing)
        {
            m_ProcessingText.text = LanguageSettings.s_Instance.GetLocalizedString("Processing"); 
        }
        else if (m_VialStates == VialStates.Ready)
        {
            m_ProcessingText.text = string.Format(LanguageSettings.s_Instance.GetLocalizedString("Ready"), LanguageSettings.s_Instance.GetLocalizedString(m_MachineColor.ToString()));
            //m_ProcessingText.text = $"{m_MachineColor} Vial is Ready";
        }
        else
        {
            m_ProcessingText.text = "";
        }
    }

    public override string ReturnTextPrompt()
    {
        return LanguageSettings.s_Instance.GetLocalizedString("Interact");
    }

    private void OnDestroy()
    {
        DeleteRef();
    }

    public enum MachineColor
    {
        Red,
        Green,
        Blue
    }

    private enum VialStates
    {
        Unprocessed,
        Processing,
        Ready,
    }
}
