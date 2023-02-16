using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Serialized Vars
    [SerializeField] private float m_Speed = 10.0f;
    public PlayerNumber playerNumber;
    Vector2 velocity;

    //Refs
    private Rigidbody2D m_Rigidbody;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(velocity.x > 0 || velocity.y > 0)
        {
            SoundManager.s_Instance.PlayWalk();
        }
    }

    void FixedUpdate()
    {
        

        if (playerNumber == PlayerNumber.PlayerOne)
        {
            velocity.x = Input.GetAxisRaw("Horizontal");
            velocity.y = Input.GetAxisRaw("Vertical");

            m_Rigidbody.MovePosition(m_Rigidbody.position + velocity * m_Speed * Time.fixedDeltaTime);
           
        }

        if (playerNumber == PlayerNumber.PlayerTwo) 
        {
            velocity.x = Input.GetAxis("Horizontal2");
            velocity.y = Input.GetAxis("Vertical2");

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
