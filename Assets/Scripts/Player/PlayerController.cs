using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Serialized Vars
    [SerializeField] private float m_Speed = 10.0f;

    Vector2 velocity;


    public PlayerNumber m_PlayerNumber;
    

    //Refs
    private Rigidbody2D m_Rigidbody;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if(velocity.x != 0 || velocity.y != 0)
        //{
        //    SoundManager.s_Instance.PlayWalk();
        //}
        //else
        //{
        //    SoundManager.s_Instance.StopWalk();
        //}
    }

    void FixedUpdate()
    {
        if (m_PlayerNumber == PlayerNumber.PlayerOne)
        {
            velocity.x = Input.GetAxisRaw("Horizontal1");
            velocity.y = Input.GetAxisRaw("Vertical1");

            m_Rigidbody.MovePosition(m_Rigidbody.position + velocity * m_Speed * Time.fixedDeltaTime);

        }
    }
}
