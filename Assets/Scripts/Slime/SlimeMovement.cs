using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    private Rigidbody2D m_Rb;
    [SerializeField] private float m_SpeedMagnitude = 1000.0f;
    private Vector2 m_MoveDirection = new();

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        m_Rb = GetComponent<Rigidbody2D>();

        StartCoroutine(StartMovement());
        //StartMovementcoroutine
        //create movement coroutine
            //Make while true loop
                //Short delay pause
                //0.5 Seconds
                //figure out random x y pos
                //create target that add random x y to position vec 3 declare vec 3 above loop
                //While target is not = to current
                    // loop counter
                    //current pos - targetpos + movespeed * time.deltatime //move close to target
                    // yeild return null

    }

    IEnumerator StartMovement()
    {
        while (true)
        {
            m_MoveDirection.x = Random.Range(-1.0f, 1.0f);
            m_MoveDirection.y = Random.Range(-1.0f, 1.0f);

            //var target = m_MoveDirection += transform.position;               


            Debug.Log(m_MoveDirection);

            m_Rb.AddForce(m_MoveDirection * m_SpeedMagnitude); //(transform.position, new Vector3(target.x, target.y, 0), m_MoveSpeed * Time.deltaTime);

            yield return new WaitForSeconds(Random.Range(1.0f, 1.5f)); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
