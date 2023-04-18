using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageLobby : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_BackButtonText;
    [SerializeField] private TMPro.TMP_Text m_PlayButtonText;
    [SerializeField] private TMPro.TMP_Text m_HintJoinText;
    [SerializeField] private TMPro.TMP_Text m_HintLeaveText;
    // Start is called before the first frame update
    void Start()
    {
        LanguageSettings.ChangeFont(LanguageSettings.m_FinalFont);
        m_HintJoinText.text = LanguageSettings.s_Instance.GetLocalizedString("HINTJOINLOBBY");
        m_HintLeaveText.text = LanguageSettings.s_Instance.GetLocalizedString("HINTLEAVELOBBY");
        m_PlayButtonText.text = LanguageSettings.s_Instance.GetLocalizedString("START");
        m_PlayButtonText.fontSize = 18;
        m_BackButtonText.text= LanguageSettings.s_Instance.GetLocalizedString("TOMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
