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
    [SerializeField] TextMeshProUGUI m_TimeSecText;
    [SerializeField] float m_TimeNum = 15;

    int m_Minutes;
    int m_Seconds;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TimerCountDown();
    }

    void TimerCountDown()
    {
        m_TimeNum = m_TimeNum - Time.deltaTime;
        int timeInt = (int)m_TimeNum;
        m_Minutes = (int)m_TimeNum / 60;
        m_Seconds = (int)m_TimeNum % 60;
        m_TimeMinText.text = m_Minutes.ToString();
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
