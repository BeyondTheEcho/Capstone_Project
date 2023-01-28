using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LanguageSettings : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_PlayButtontext;
    [SerializeField] private TMPro.TMP_Text m_QuitButtontext;
    [SerializeField] private Languages m_Language;

    public static LanguageSettings s_Instance { get; private set; }

    Dictionary<string, string> m_LocalizationDictionary_French = new Dictionary<string, string>(); 

    private void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(this);
        }
        else
        {
            s_Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        // Store all the text here. 
        m_LocalizationDictionary_French.Add("Clock In!", "Horloge d'entrée!");
        m_LocalizationDictionary_French.Add("Quit to Desktop", "Quitter sur le bureau");
        m_LocalizationDictionary_French.Add("Press 'F' to pickup the ", "Appuyez sur 'F' pour récupérer le ");
        m_LocalizationDictionary_French.Add("example", "exemple");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnglishChosen()
    {
        m_Language = Languages.English;
        m_PlayButtontext.text = "Clock In!";
        m_QuitButtontext.text = "Quit to Desktop";
    }

    public void FrenchChosen()
    {
        m_Language = Languages.French;
        m_PlayButtontext.text = GetLocalizedString("Clock In!");
        m_QuitButtontext.text = GetLocalizedString("Quit to Desktop");
    }

    public string GetLocalizedString(string sourceStr)
    {
        switch (m_Language)
        {
            case Languages.English:
                return sourceStr;

            case Languages.French:
                if (m_LocalizationDictionary_French.ContainsKey(sourceStr))
                {
                    return m_LocalizationDictionary_French[sourceStr];
                }
                else
                {
                    return "!!!MISSING!!!";
                }
        }
        return "";
    }
}
