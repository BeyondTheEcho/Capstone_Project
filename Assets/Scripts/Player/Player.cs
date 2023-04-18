using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Playables;
using TMPro;
using System.Linq;
using System;

using UnityEditor.Animations;

public class Player : MonoBehaviour
{
    //Inspector Vars
    [Header("Player Settings")]
    [SerializeField] private float m_InteractRange = 1.7f;

    [Header("References")]
    [SerializeField] private SpriteRenderer m_PopupSpriteRenderer;
    [SerializeField] private SpriteRenderer m_ButtonSpriteRenderer;
    [SerializeField] private GameObject m_ItemPopup;
    [SerializeField] private GameObject m_TextPrompt;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem m_WalkParticle;

    [Header("Player Animator Controllers")]
    [SerializeField] private AnimatorController m_PlayerOneAnim;
    [SerializeField] private AnimatorController m_PlayerTwoAnim;
    [SerializeField] private AnimatorController m_PlayerThreeAnim;
    [SerializeField] private AnimatorController m_PlayerFourAnim;

    [Header("Player Sprites")]
    [SerializeField] private Sprite m_PlayerOneSprite;
    [SerializeField] private Sprite m_PlayerTwoSprite;
    [SerializeField] private Sprite m_PlayerThreeSprite;
    [SerializeField] private Sprite m_PlayerFourSprite;

    //Public Vars
    public IDropable m_HeldItem = null;

    //Private Vars
    private float m_InteractDelay = 0.5f;
    private Coroutine m_InteractDelayCoroutine;
    private PlayerController m_PlayerController;
    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;

    //Backing Vars
    private float m_SpeedBacking = 10.0f;
    private PlayerNumber m_PlayerNumberBacking = PlayerNumber.PlayerOne;

    //Properties
    public float m_Speed
    {
        get { return m_SpeedBacking; }
        set
        {
            m_SpeedBacking = value;
            m_PlayerController.m_Speed = value;
        }
    }
    public PlayerNumber m_PlayerNumber
    {
        get { return m_PlayerNumberBacking; }
        set
        {
            m_PlayerNumberBacking = value;
            m_PlayerController.m_PlayerNumber = value;
        }
    }

    private Vector3 m_PreviousPos = new Vector3();
    private Vector3 m_CurrentPos = new Vector3();

    private Vector3 m_PreviousParticlePos = new Vector3();
    private Vector3 m_CurrentParticlePos = new Vector3();

    private Vector3 m_RightVector3 = new Vector3(1, 1, 1);
    private Vector3 m_LeftVector3 = new Vector3(-1, 1, 1);

    void Awake()
    {
        m_PlayerController = GetComponent<PlayerController>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(DustParticleCheck());
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

        if (directionVector.x > 0.0f)
        {
            gameObject.transform.localScale = m_RightVector3;
            m_ItemPopup.transform.localScale = m_LeftVector3;
            m_TextPrompt.transform.localScale = m_RightVector3;
        }
        else if (directionVector.x < 0.0f)
        {
            gameObject.transform.localScale = m_LeftVector3;
            m_ItemPopup.transform.localScale = m_RightVector3;
            m_TextPrompt.transform.localScale = m_LeftVector3;;
        }
    }

    IEnumerator DustParticleCheck()
    {
        while (true)
        {
            m_PreviousParticlePos = m_CurrentParticlePos;
            m_CurrentParticlePos = gameObject.transform.position;

            Vector3 directionVector = (m_CurrentParticlePos - m_PreviousParticlePos).normalized;

            if (directionVector.x != 0.0f)
            {
                m_WalkParticle.Play();
            }
            else
            {
                m_WalkParticle.Stop();
            }

            yield return new WaitForSeconds(0.1f);
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

    public void ShowXButton(bool shown) 
    {
        m_ButtonSpriteRenderer.gameObject.SetActive(shown);
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

    public void SetupPlayer(PlayerNumber num)
    {
        m_PlayerNumber = num;

        if (m_PlayerNumber == PlayerNumber.PlayerOne)
        {
            m_SpriteRenderer.sprite = m_PlayerOneSprite;
            m_Animator.runtimeAnimatorController = m_PlayerOneAnim;

            InputManager.s_Instance.Player_1_Interact += PlayerInteract;
            InputManager.s_Instance.Player_1_Drop += PlayerDrop;
        }

        if (m_PlayerNumber == PlayerNumber.PlayerTwo)
        {
            m_SpriteRenderer.sprite = m_PlayerTwoSprite;
            m_Animator.runtimeAnimatorController = m_PlayerTwoAnim;

            InputManager.s_Instance.Player_2_Interact += PlayerInteract;
            InputManager.s_Instance.Player_2_Drop += PlayerDrop;
        }

        if (m_PlayerNumber == PlayerNumber.PlayerThree)
        {
            m_SpriteRenderer.sprite = m_PlayerThreeSprite;
            m_Animator.runtimeAnimatorController = m_PlayerThreeAnim;

            InputManager.s_Instance.Player_3_Interact += PlayerInteract;
            InputManager.s_Instance.Player_3_Drop += PlayerDrop;
        }

        if (m_PlayerNumber == PlayerNumber.PlayerFour)
        {
            m_SpriteRenderer.sprite = m_PlayerFourSprite;
            m_Animator.runtimeAnimatorController = m_PlayerFourAnim;

            InputManager.s_Instance.Player_4_Interact += PlayerInteract;
            InputManager.s_Instance.Player_4_Drop += PlayerDrop;
        }
    }
}
