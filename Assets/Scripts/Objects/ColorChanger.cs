using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : InteractableBase
{
    //Machine Settings
    [Header("Machine Settings")]
    [SerializeField] private MachineColor m_MachineColor;

    //UI Vars
    [Header("UI Settings")]
    [SerializeField] private Image m_ProgressBar;
    [SerializeField] private GameObject m_ProgressBarContainer;

    [SerializeField] private GameObject m_DoneParticles;

    private VialStates m_VialStates = VialStates.Unprocessed;
    private Vials m_Vial = null;
    private float m_MachineProcessingDelay => m_MachineColor switch
    {
        MachineColor.Red => 3.5f,
        MachineColor.Green => 7.5f,
        MachineColor.Blue => 13.5f,
        _ => 10.5f,
    };

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

        yield return StartCoroutine(ProgressCountdown(m_MachineProcessingDelay));

        if (m_MachineColor == MachineColor.Blue)
        { 
            if (vial.m_VialColor == Vials.VialColor.Red)
            {
                vial.m_VialColor = Vials.VialColor.Purple;
            }
            else if (vial.m_VialColor == Vials.VialColor.Green) 
            {
                vial.m_VialColor = Vials.VialColor.Teal;
            }
            else
            {
                vial.m_VialColor = Vials.VialColor.Blue;
            }     
        }

        if (m_MachineColor == MachineColor.Red)
        {
            if (vial.m_VialColor == Vials.VialColor.Blue)
            {
                vial.m_VialColor = Vials.VialColor.Purple;
            }
            else if (vial.m_VialColor == Vials.VialColor.Green)
            {
                vial.m_VialColor = Vials.VialColor.Orange;
            }
            else
            {
                vial.m_VialColor = Vials.VialColor.Red;
            }
        }

        if (m_MachineColor == MachineColor.Green) 
        {
            if (vial.m_VialColor == Vials.VialColor.Blue)
            {
                vial.m_VialColor = Vials.VialColor.Teal;
            }
            else if (vial.m_VialColor == Vials.VialColor.Red)
            {
                vial.m_VialColor = Vials.VialColor.Orange;
            }
            else
            {
                vial.m_VialColor = Vials.VialColor.Green;
            }
        }

        m_Vial = vial;
        m_VialStates = VialStates.Ready;
    }

    IEnumerator ProgressCountdown(float delay)
    {
        float progress = 0;

        while (progress <= delay) 
        {
            yield return new WaitForSeconds(1.0f);
            progress += 1.0f;

            m_ProgressBar.fillAmount = progress / delay;
        }
    }

    private void InputVial(Player player)
    {
        if (player.m_HeldItem == null) { return; }

        if (player.m_HeldItem.gameObject.TryGetComponent<Vials>(out Vials vial))
        {
            //Guards for invalid vial inputs
            if (vial.m_VialColor == Vials.VialColor.Empty) return;
            if (vial.m_VialColor == Vials.VialColor.Purple) return;
            if (vial.m_VialColor == Vials.VialColor.Orange) return;
            if (vial.m_VialColor == Vials.VialColor.Teal) return;

            //Guards prevents reprocessing the same color
            if (m_MachineColor == MachineColor.Red && vial.m_VialColor == Vials.VialColor.Red) return;
            if (m_MachineColor == MachineColor.Blue && vial.m_VialColor == Vials.VialColor.Blue) return;
            if (m_MachineColor == MachineColor.Green && vial.m_VialColor == Vials.VialColor.Green) return;

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
            m_ProgressBarContainer.gameObject.SetActive(true);
        }
        else if (m_VialStates == VialStates.Ready)
        {
            m_ProgressBarContainer.gameObject.SetActive(true);
            m_DoneParticles.SetActive(true);
        }
        else
        {
            m_ProgressBarContainer.gameObject.SetActive(false);
            m_DoneParticles.SetActive(false);
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
