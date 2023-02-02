using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFeature : MonoBehaviour
{
    public GameObject m_PauseCanvas;
    public static bool m_InPause=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (m_InPause == false)
            {
                m_InPause = true;
                m_PauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }

        }
    }

    public void BackToGame()
    {
        m_InPause = false;
        m_PauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    
}
