using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MouseControl : MonoBehaviour
{
    Vector3 m_InputDirection = new Vector3();
    Vector3 m_Position = new Vector3();

    //singleton
    public static MouseControl Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        m_InputDirection.x = Input.GetAxis("MouseX");
        m_InputDirection.y = Input.GetAxis("MouseY");

        float multiplier = Time.deltaTime;

        //Used for pause menu
        if (multiplier <= 0)
        {
            multiplier = 1.0f;
        }
        
        if (SceneManager.GetActiveScene().name == "GameSceneFinal")
        {
            m_Position.x = transform.position.x + m_InputDirection.x * 10.0f * multiplier;
            m_Position.y = transform.position.y + m_InputDirection.y * 10.0f * multiplier;
        }
        else
        {
            m_Position.x = transform.position.x + m_InputDirection.x * 1000.0f * multiplier;
            m_Position.y = transform.position.y + m_InputDirection.y * 1000.0f * multiplier;
        }

        m_Position.x = Mathf.Clamp(m_Position.x, 27, 1900);
        m_Position.y = Mathf.Clamp(m_Position.y, 46, 1049); //Clamp may cause issues in other scenes
        transform.position = m_Position;

        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = m_Position;

        List<RaycastResult> results = new List<RaycastResult>();

        if (Input.GetButtonDown("MenuInteraction") || Input.GetMouseButtonDown(0))
        {
            EventSystem.current.RaycastAll(data, results);
            if (results[0].gameObject != null)
            {
               Button currentButton = results[0].gameObject.GetComponent<Button>();
               if (currentButton != null)
               {
                    currentButton.onClick.Invoke();
               }
            }
        }
    }
}