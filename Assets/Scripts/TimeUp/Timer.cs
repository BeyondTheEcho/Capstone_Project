using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeNum=15;
    public GameObject[] timeUI;
    public Sprite[] spriteTexture;

    private int num0;
    private int num1;
    private int num2;
    private int num3;

    int minutes;
    int seconds;
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
        timeNum = timeNum - Time.deltaTime;
        minutes = (int)timeNum / 60;
        seconds = (int)timeNum % 60;
        num0 = (int)(minutes / 10);
        num1 = (int)(minutes % 10);
        num2 = (int)(seconds / 10);
        num3 = (int)(seconds % 10);

        timeUI[0].GetComponent<Image>().sprite = spriteTexture[num0];
        timeUI[1].GetComponent<Image>().sprite = spriteTexture[num1];
        timeUI[2].GetComponent<Image>().sprite = spriteTexture[num2];
        timeUI[3].GetComponent<Image>().sprite = spriteTexture[num3];

        if (timeNum < 0 )
        {
            SceneManager.LoadScene("TimeUP");
        }
    }

    public void AddSec(int sec)
    {
        timeNum += sec;
    }

    public void ReduceSec(int sec)
    {
        timeNum -= sec;
    }
}
