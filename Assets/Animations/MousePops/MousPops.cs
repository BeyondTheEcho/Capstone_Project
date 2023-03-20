using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousPops : MonoBehaviour
{
    [SerializeField] GameObject m_mouse;
    [SerializeField] GameObject m_AnnoyingCanvas;

    public static bool m_mouseInScene = true;
    public static float pusnishmentCountDown = 0;
    public static bool startPunishmentCount=true;

    void Start()
    {
        Vector3 mousePos = new Vector3(Random.Range(20, -20.2f), Random.Range(7, -8.5f), 0);
        Instantiate(m_mouse, mousePos, Quaternion.Euler(Random.Range(0, 30), 0, 0));
        m_mouseInScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        GenMouse();
        CheckPunishment();
    }

    void GenMouse()
    {
        if (m_mouseInScene==false)
        {
            StartCoroutine(NextMousePops());
            m_mouseInScene = true;
        }
    }

    IEnumerator NextMousePops()
    {
        int wait_time = Random.Range(1, 10);
        Debug.Log("wait time: "+ wait_time);
        yield return new WaitForSeconds(wait_time);
        startPunishmentCount=true;
        Vector3 mousePos = new Vector3(Random.Range(20, -20.2f), Random.Range(7, -8.5f), 0);
        Instantiate(m_mouse, mousePos, Quaternion.Euler(Random.Range(0, 30), 0, 0));
    }

    void CheckPunishment()
    {
        if (pusnishmentCountDown >= 5)
        {
            m_AnnoyingCanvas.SetActive(true);
        }
        else 
        {
            m_AnnoyingCanvas.SetActive(false);
        }

        if (startPunishmentCount)
        {
            pusnishmentCountDown += Time.deltaTime;
        }
    }


}
