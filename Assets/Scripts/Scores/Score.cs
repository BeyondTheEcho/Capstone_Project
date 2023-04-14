using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI m_ScoreText;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ScoreText();
    }

    public void ScoreText()
    {
        m_ScoreText.text = "Score: " + GameManager.s_Instance.GetScore().ToString();
    }
}
