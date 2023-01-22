using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class OrderManager : MonoBehaviour
{
    private int baseOrderSize = 10;
    private int difficulties = 2; //will take from players' choice, it's just a temporary here
    
    public TextMeshProUGUI timeText; //for development needs, easy to see value; won't show to player in the game
    private float timeNum=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChaosMultiplyer();
    }

    public void ChaosMultiplyer()
    {
        float chaos = Timer();
        float orderSize = 1+baseOrderSize * difficulties * chaos/10000;
        timeText.text = orderSize.ToString() + "s";
    }

    private float Timer()
    {
        timeNum = timeNum + Time.deltaTime;
        
        return timeNum;
    }
}
