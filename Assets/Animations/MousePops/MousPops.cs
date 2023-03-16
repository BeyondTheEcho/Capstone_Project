using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousPops : MonoBehaviour
{
    [SerializeField] GameObject m_mouse;
    public static bool m_mouseInScene = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GenMouse();
    }

    void GenMouse()
    {
        if (!m_mouseInScene && PopWait.waitingOver)
        {
            Vector3 mousePos = new Vector3(Random.Range(20, -20.2f), Random.Range(7, -8.5f), 0);
            Instantiate(m_mouse, mousePos, Quaternion.Euler(Random.Range(0, 30), 0, 0));
            m_mouseInScene = true;
        }

        
    }


}
