using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float m_Speed = 10.0f;
    public PlayerNumber m_PlayerNumber;
    Vector2 velocity;
    private Rigidbody2D m_Rigidbody;
    private AudioSource m_Source;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (m_PlayerNumber == PlayerNumber.PlayerOne)
        {
            velocity.x = Input.GetAxisRaw("Horizontal1");
            velocity.y = Input.GetAxisRaw("Vertical1");

            m_Rigidbody.MovePosition(m_Rigidbody.position + velocity * m_Speed * Time.fixedDeltaTime);
        }

        if (m_PlayerNumber == PlayerNumber.PlayerTwo) 
        {
            velocity.x = Input.GetAxis("Horizontal2");
            velocity.y = Input.GetAxis("Vertical2");

            m_Rigidbody.MovePosition(m_Rigidbody.position + velocity * m_Speed * Time.fixedDeltaTime);
        }

        if (m_PlayerNumber == PlayerNumber.PlayerThree)
        {
            velocity.x = Input.GetAxis("Horizontal3");
            velocity.y = Input.GetAxis("Vertical3");

            m_Rigidbody.MovePosition(m_Rigidbody.position + velocity * m_Speed * Time.fixedDeltaTime);
        }

        if (m_PlayerNumber == PlayerNumber.PlayerFour)
        {
            velocity.x = Input.GetAxis("Horizontal4");
            velocity.y = Input.GetAxis("Vertical4");

            m_Rigidbody.MovePosition(m_Rigidbody.position + velocity * m_Speed * Time.fixedDeltaTime);
        }

    }

}
