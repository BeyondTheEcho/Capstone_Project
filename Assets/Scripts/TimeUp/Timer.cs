using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeNum;
    public GameObject[] timeUI;
    public Sprite[] spriteTexture;

    private int num0;
    private int num1;
    private int num2;
    // Start is called before the first frame update
    void Start()
    {
        timeNum = 15;
    }

    // Update is called once per frame
    void Update()
    {
        TimerCountDown();
    }

    void TimerCountDown()
    {

        timeNum = timeNum - Time.deltaTime;
        if (timeNum < 10 && timeNum >= 0)
        {
            timeUI[0].GetComponent<Image>().sprite = spriteTexture[(int)timeNum];
            timeUI[1].SetActive(false);
            timeUI[2].SetActive(false);
        }
        else if (timeNum < 100 && timeNum >= 10)
        {
            num0 = (int)(timeNum / 10);
            num1 = (int)(timeNum % 10);
            timeUI[0].GetComponent<Image>().sprite = spriteTexture[num0];
            timeUI[1].GetComponent<Image>().sprite = spriteTexture[num1];
            timeUI[1].SetActive(true);
            timeUI[2].SetActive(false);
        }
        else if (timeNum < 1000 && timeNum >= 100)
        {
            num0 = (int)(timeNum / 100);
            num1 = (int)((timeNum % 100) / 10);
            num2 = (int)((timeNum % 100) % 10);
            timeUI[0].GetComponent<Image>().sprite = spriteTexture[num0];
            timeUI[1].GetComponent<Image>().sprite = spriteTexture[num1];
            timeUI[2].GetComponent<Image>().sprite = spriteTexture[num2];
            timeUI[0].SetActive(true);
            timeUI[1].SetActive(true);
            timeUI[2].SetActive(true);
        }

        if (timeNum < 0 )
        {
            SceneManager.LoadScene("TimeUP");
        }
    }
}
