using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;
using UnityEngine.UI;

public class PauseFeature : MonoBehaviour
{
    public GameObject m_PauseCanvas;
    public static bool m_InPause=false;

    [SerializeField] private TMPro.TMP_Text m_BackButtonText;
    [SerializeField] private TMPro.TMP_Text m_QuitButtonText;

    [SerializeField] private TMPro.TMP_Text m_TakeABreakText;
    // Start is called before the first frame update
    void Start()
    {
        m_BackButtonText.text = LanguageSettings.s_Instance.GetLocalizedString("BACK");
        m_QuitButtonText.text = LanguageSettings.s_Instance.GetLocalizedString("QUIT");
        m_TakeABreakText.text = LanguageSettings.s_Instance.GetLocalizedString("PAUSE");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (m_InPause == false)
            {
                m_InPause = true;
                m_QuitButtonText.fontSize = 14;
                m_PauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }

        }
    }

    public void BackToGame()
    {
        m_InPause = false;
        m_PauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
                Application.Quit();
    }


}
