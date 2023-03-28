using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
//using static Unity.VisualScripting.Icons;

public class PauseFeature : MonoBehaviour
{
    public GameObject m_PauseCanvas;
    public static bool m_InPause;

    [SerializeField] private TMPro.TMP_Text m_BackButtonText;
    [SerializeField] private TMPro.TMP_Text m_QuitButtonText;

    [SerializeField] private TMPro.TMP_Text m_TakeABreakText;

    // Start is called before the first frame update
    void Start()
    {
        m_PauseCanvas.SetActive(false);
        m_InPause = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetButtonDown("Pause1") || Input.GetButtonDown("Pause2") || Input.GetButtonDown("Pause3") || Input.GetButtonDown("Pause4"))
        {
            if (m_InPause == false)
            {
                m_BackButtonText.text = LanguageSettings.s_Instance.GetLocalizedString("BACK");
                m_QuitButtonText.text = LanguageSettings.s_Instance.GetLocalizedString("TOMenu");
                m_TakeABreakText.text = LanguageSettings.s_Instance.GetLocalizedString("PAUSE");
                m_InPause = true;
                m_QuitButtonText.fontSize = 14;
                m_PauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }

        }
        else if (Input.GetButtonDown("UnPause1") || Input.GetButtonDown("UnPause2") || Input.GetButtonDown("UnPause3") || Input.GetButtonDown("UnPause4"))
        {
            BackToGame();
        }
    }

    public void BackToGame()
    {
        m_InPause = false;
        m_PauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;
        LanguageSettings.s_Instance.Reset();
        SceneManager.LoadScene("Main Menu");
    }

}
