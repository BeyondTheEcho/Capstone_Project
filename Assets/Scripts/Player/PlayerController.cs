using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Serialized Vars
    [SerializeField] private float m_Speed = 10.0f;
    public PlayerNumber playerNumber;

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
        if (playerNumber == PlayerNumber.PlayerOne)
        {
            velocity.x = Input.GetAxisRaw("Horizontal");
            velocity.y = Input.GetAxisRaw("Vertical");

            m_Rigidbody.MovePosition(m_Rigidbody.position + velocity * m_Speed * Time.fixedDeltaTime);
        }

        if (playerNumber == PlayerNumber.PlayerTwo)
        {
            velocity.x = Input.GetAxisRaw("Horizontal2");
            velocity.y = Input.GetAxisRaw("Vertical2");

            m_Rigidbody.MovePosition(m_Rigidbody.position + velocity * m_Speed * Time.fixedDeltaTime);
        }
    }

    public enum PlayerNumber
    {
        PlayerOne,
        PlayerTwo,
        PlayerThree,
        PlayerFour
    }
}
