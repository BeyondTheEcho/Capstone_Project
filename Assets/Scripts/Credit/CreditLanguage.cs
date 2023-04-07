using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditLanguage : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_BackButtonText;
    [SerializeField] private TMPro.TMP_Text m_QuitButtonText;
    [SerializeField] private TMPro.TMP_Text m_ThanksText;
    [SerializeField] private TMPro.TMP_Text m_MentorsThankText;
    [SerializeField] private TMPro.TMP_Text m_OurPorfText;
    // Start is called before the first frame update
    void Start()
    {
        LanguageSettings.ChangeFont(LanguageSettings.m_FinalFont);
        m_QuitButtonText.text = LanguageSettings.s_Instance.GetLocalizedString("QUIT");
        m_BackButtonText.text = LanguageSettings.s_Instance.GetLocalizedString("TOMenu");
        m_ThanksText.text = LanguageSettings.s_Instance.GetLocalizedString("THANKS");
        m_MentorsThankText.text = LanguageSettings.s_Instance.GetLocalizedString("THANKSMentor");
        m_OurPorfText.text = LanguageSettings.s_Instance.GetLocalizedString("OurProf");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
