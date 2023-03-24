using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        Debug.Log(Input.GetAxis("MouseX"));
        m_InputDirection.x = Input.GetAxis("MouseX");
        m_InputDirection.y = Input.GetAxis("MouseY");

        if (SceneManager.GetActiveScene().name == "GameSceneFinal")
        {
            m_Position.x = transform.position.x + m_InputDirection.x * 10.0f * Time.deltaTime;
            m_Position.y = transform.position.y + m_InputDirection.y * 10.0f * Time.deltaTime;
        }
        else
        {
            m_Position.x = transform.position.x + m_InputDirection.x * 1000.0f * Time.deltaTime;
            m_Position.y = transform.position.y + m_InputDirection.y * 1000.0f * Time.deltaTime;
        }

        transform.position = m_Position;

        Ray ray = Camera.main.ScreenPointToRay(gameObject.transform.position);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}