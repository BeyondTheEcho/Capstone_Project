using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] float m_TimeNum = 15;
    public GameObject[] m_TimeUI;
    public Sprite[] m_SpriteTexture;

    private int m_Num0;
    private int m_Num1;
    private int m_Num2;
    private int m_Num3;

    int m_Minutes;
    int m_Seconds;

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
        m_Minutes = (int)m_TimeNum / 60;
        m_Seconds = (int)m_TimeNum % 60;
        m_Num0 = (int)(m_Minutes / 10);
        m_Num1 = (int)(m_Minutes % 10);
        m_Num2 = (int)(m_Seconds / 10);
        m_Num3 = (int)(m_Seconds % 10);

        m_TimeUI[0].GetComponent<Image>().sprite = m_SpriteTexture[m_Num0];
        m_TimeUI[1].GetComponent<Image>().sprite = m_SpriteTexture[m_Num1];
        m_TimeUI[2].GetComponent<Image>().sprite = m_SpriteTexture[m_Num2];
        m_TimeUI[3].GetComponent<Image>().sprite = m_SpriteTexture[m_Num3];

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
