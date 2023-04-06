using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Playables;
using TMPro;
using System.Linq;
using System;

public class Player : MonoBehaviour
{
    //Public Vars
    [SerializeField] public IDropable m_HeldItem = null;

    //Private Serialized Vars
    [SerializeField] private float m_InteractRange = 1.7f; //Default Value lines up with colliders
    [SerializeField] private SpriteRenderer m_PopupSpriteRenderer;
    [SerializeField] private GameObject m_ItemPopup;
    [SerializeField] private GameObject m_TextPrompt;

    //Private Vars
    private ToolManager m_ToolManager;
    private TMP_Text m_ToolPrompt;
    private PlayerNumber m_PlayerNumber;
    private float m_InteractDelay = 0.5f;
    private Coroutine m_InteractDelayCoroutine;

    private Vector3 m_PreviousPos = new Vector3();
    private Vector3 m_CurrentPos = new Vector3();

    private Vector3 m_RightVector3 = new Vector3(1, 1, 1);
    private Vector3 m_LeftVector3 = new Vector3(-1, 1, 1);

    void Awake()
    {
        m_ToolPrompt = GetComponentInChildren<TMP_Text>();
        m_ToolManager = FindObjectOfType<ToolManager>();
    }

    void Start()
    {
        m_PlayerNumber = gameObject.GetComponent<PlayerController>().m_PlayerNumber;
        
        if (m_PlayerNumber == PlayerNumber.PlayerOne)
        {
            InputManager.s_Instance.Player_1_Interact += PlayerInteract;
            InputManager.s_Instance.Player_1_Drop += PlayerDrop;
        }
        else if(m_PlayerNumber == PlayerNumber.PlayerTwo)
        {
            InputManager.s_Instance.Player_2_Interact += PlayerInteract;
            InputManager.s_Instance.Player_2_Drop += PlayerDrop;
        }
        else if (m_PlayerNumber == PlayerNumber.PlayerThree)
        {
            InputManager.s_Instance.Player_3_Interact += PlayerInteract;
            InputManager.s_Instance.Player_3_Drop += PlayerDrop;
        }
        else if (m_PlayerNumber == PlayerNumber.PlayerFour)
        {
            InputManager.s_Instance.Player_4_Interact += PlayerInteract;
            InputManager.s_Instance.Player_4_Drop += PlayerDrop;
        }
    }

    void Update()
    {
        if (m_HeldItem != null)
        {
            m_ItemPopup.gameObject.SetActive(true);
            m_PopupSpriteRenderer.gameObject.SetActive(true);
            m_PopupSpriteRenderer.sprite = m_HeldItem.gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            m_ItemPopup.gameObject.SetActive(false);
            m_PopupSpriteRenderer.gameObject.SetActive(false);
        }

        m_PreviousPos = m_CurrentPos;
        m_CurrentPos = gameObject.transform.position;

        Vector3 directionVector = (m_CurrentPos - m_PreviousPos).normalized;

        if (directionVector.x > 0)
        {
            gameObject.transform.localScale = m_RightVector3;
            m_ItemPopup.transform.localScale = m_LeftVector3;
            m_TextPrompt.transform.localScale = m_RightVector3;
        }

        if (directionVector.x < 0)
        {
            gameObject.transform.localScale = m_LeftVector3;
            m_ItemPopup.transform.localScale = m_RightVector3;
            m_TextPrompt.transform.localScale = m_LeftVector3;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out InteractableBase interactable))
        {         
            InteractablesManager.s_Instance.AddPromptObject(this, interactable);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out InteractableBase interactable))
        {
            InteractablesManager.s_Instance.RemovePromptObject(this, interactable);
        }
    }

    public void UpdatePrompt(string prompt)
    {
        m_ToolPrompt.text = prompt;
    }

    private IEnumerator InteractDelay()
    {
        yield return new WaitForSeconds(m_InteractDelay);

        m_InteractDelayCoroutine = null;
    }

    public void PlayerInteract()
    {
        if (m_InteractDelayCoroutine == null)
        {
            m_InteractDelayCoroutine = StartCoroutine(InteractDelay());

            InteractableBase item = null;

            item = InteractablesManager.s_Instance.GetClosestInteractableInRange(this, m_InteractRange);

            if (item != null)
            {
                item.OnInteract(this);
            }
        }
    }

    public void PlayerDrop()
    {
        if (m_HeldItem != null)
        {
            InteractableBase item = null;

            item = InteractablesManager.s_Instance.GetClosestBeltInRange(this, m_InteractRange);

            if (item != null)
            {
                item.GetComponent<Belts>().PlaceItemOnBelt(this);
            }
            else
            {
                m_HeldItem.OnDrop(this);
            }
        }
    }
}
