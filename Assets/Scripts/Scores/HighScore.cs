using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI m_HighScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HighScoreText();
    }

    public void HighScoreText()
    {
        m_HighScoreText.text = "High Score: " + GameManager.s_Instance.GetHighScore().ToString();
    }
}
