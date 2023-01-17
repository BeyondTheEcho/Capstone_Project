using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Serialized Vars
    [SerializeField] private float m_Speed = 10.0f;

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

        velocity.x = Input.GetAxis("Horizontal");
        velocity.y = Input.GetAxis("Vertical");

        m_Rigidbody.MovePosition(m_Rigidbody.position + velocity * m_Speed * Time.fixedDeltaTime);
    }
}
