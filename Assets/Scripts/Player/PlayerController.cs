using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Serialized Vars
    [SerializeField] private float m_Speed = 10.0f;
    public PlayerNumber m_PlayerNumber;
    
    //Refs
    private Rigidbody2D m_Rigidbody;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 velocity;

        if (m_PlayerNumber == PlayerNumber.PlayerOne)
        {
            velocity.x = Input.GetAxisRaw("Horizontal");
            velocity.y = Input.GetAxisRaw("Vertical");

            m_Rigidbody.MovePosition(m_Rigidbody.position + velocity * m_Speed * Time.fixedDeltaTime);
           
        }

        if (m_PlayerNumber == PlayerNumber.PlayerTwo) 
        {
            velocity.x = Input.GetAxis("Horizontal2");
            velocity.y = Input.GetAxis("Vertical2");

            m_Rigidbody.MovePosition(m_Rigidbody.position + velocity * m_Speed * Time.fixedDeltaTime);
        }
        
    }
}
