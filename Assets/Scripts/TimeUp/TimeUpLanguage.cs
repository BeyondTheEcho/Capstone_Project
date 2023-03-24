using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUpLanguage : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_BackButtonText;
    [SerializeField] private TMPro.TMP_Text m_HeadingText;
    // Start is called before the first frame update
    void Start()
    {
        LanguageSettings.ChangeFont(LanguageSettings.m_FinalFont);
        m_HeadingText.text = LanguageSettings.s_Instance.GetLocalizedString("TimeUp");
        m_BackButtonText.text = LanguageSettings.s_Instance.GetLocalizedString("TOMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
