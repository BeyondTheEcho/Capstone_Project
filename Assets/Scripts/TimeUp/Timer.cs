using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;
using TMPro;


public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TimeMinText;
    [SerializeField] TextMeshProUGUI m_TimeTenMinText;
    [SerializeField] TextMeshProUGUI m_TimeSecText;
    [SerializeField] TextMeshProUGUI m_TimeTenSecText;
    [SerializeField] float m_TimeNum = 15;

    int m_Minutes;
    int m_TenMinutes;
    int m_Seconds;
    int m_TenSeconds;
    Scene m_Scene;
    string m_SceneName;
    // Start is called before the first frame update
    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        m_SceneName = m_Scene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_SceneName != "Tutorial")
        {
            TimerCountDown();
        }
    }

    void TimerCountDown()
    {
        m_TimeNum = m_TimeNum - Time.deltaTime;
        int timeInt = (int)m_TimeNum;
        m_Minutes = ((int)m_TimeNum / 60)%10;
        m_TenMinutes = ((int)m_TimeNum / 60)/10;
        m_TenSeconds = ((int)m_TimeNum % 60)/10;
        m_Seconds = ((int)m_TimeNum % 60)%10;
        
        m_TimeTenMinText.text = m_TenMinutes.ToString();
        m_TimeMinText.text = m_Minutes.ToString();
        m_TimeTenSecText.text = m_TenSeconds.ToString();
        m_TimeSecText.text = m_Seconds.ToString();

        if (m_TimeNum < 0 && GameManager.s_Instance.GetScore() <= 1000)
        {
            SceneManager.LoadScene("TimeUP");
        }

        if (m_TimeNum < 0 && GameManager.s_Instance.GetScore() >= 1000)
        {
            SceneManager.LoadScene("Winner");
        }
    }

    public void AddSec(int sec)
    {
        m_TimeNum += sec;
    }

    public void ReduceSec(int sec)
    {
        m_TimeNum -= sec;
    }
}
