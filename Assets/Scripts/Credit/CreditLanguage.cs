using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditLanguage : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_BackButtonText;
    [SerializeField] private TMPro.TMP_Text m_QuitButtonText;
    [SerializeField] private TMPro.TMP_Text m_ThanksText;
    // Start is called before the first frame update
    void Start()
    {
        m_QuitButtonText.text = LanguageSettings.s_Instance.GetLocalizedString("QUIT");
        m_BackButtonText.text = LanguageSettings.s_Instance.GetLocalizedString("TOMenu");
        m_ThanksText.text = LanguageSettings.s_Instance.GetLocalizedString("THANKS");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
