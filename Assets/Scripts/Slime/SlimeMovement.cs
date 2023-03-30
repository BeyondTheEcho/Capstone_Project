using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    private Rigidbody2D m_Rb;
    private float m_SpeedMagnitude = 10000.0f;
    private Vector2 m_MoveDirection = new();

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        m_Rb = GetComponent<Rigidbody2D>();

        StartCoroutine(StartMovement());
    }

    IEnumerator StartMovement()
    {
        while (true)
        {
            m_MoveDirection.x = Random.Range(-1.0f, 1.0f);
            m_MoveDirection.y = Random.Range(-1.0f, 1.0f);

            m_Rb.AddForce(m_MoveDirection * m_SpeedMagnitude);

            yield return new WaitForSeconds(Random.Range(1.0f, 1.5f)); 
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
