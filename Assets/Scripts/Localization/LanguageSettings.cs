using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using System.IO;

public class LanguageSettings : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_PlayButtonText;
    [SerializeField] private TMPro.TMP_Text m_QuitButtonText;

    //set default to English, so programmer can directly start from game scene
    [SerializeField] private Languages m_Language = Languages.English;
    [SerializeField] private TextAsset m_FrenchTXT;
    [SerializeField] private TextAsset m_EnglishTXT;

    public static LanguageSettings s_Instance { get; private set; }

    readonly Dictionary<string, string> m_LocalizationDictionary_French = new Dictionary<string, string>();
    readonly Dictionary<string, string> m_LocalizationDictionary_English = new Dictionary<string, string>(); 

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
        ReadFile(m_FrenchTXT, m_LocalizationDictionary_French);
        ReadFile(m_EnglishTXT, m_LocalizationDictionary_English);
        DontDestroyOnLoad(this);
    }

    void ReadFile(TextAsset txtFile, Dictionary<string, string> languageDictionary)
    {
        //end of the line, could be mac, pc, or linux
        string[] splitStringLine = new string[] { "\r\n", "\r", "\n" };

        //split the key and value. ex. START, Clock In!
        char[] splitKeyValue = new char[] { '^' };

        //store all the lines in the file
        string[] Lines = txtFile.text.Split(splitStringLine, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < Lines.Length; i++)
        {
            print("Line= " + Lines[i]);
            string[] line = Lines[i].Split(splitKeyValue, System.StringSplitOptions.None);
            string key=line[0];
            string value=line[1];
            languageDictionary.Add(key, value);

        }
    
    }

    public string GetLocalizedString(string key)
    {
        switch (m_Language)
        {
            case Languages.English:
                if (m_LocalizationDictionary_French.ContainsKey(key))
                {
                    return m_LocalizationDictionary_English[key];
                }
                else
                {
                    return "!!!No key value pair found in dictionary!!!";
                }
                

            case Languages.French:
                if (m_LocalizationDictionary_French.ContainsKey(key))
                {
                    return m_LocalizationDictionary_French[key];
                }
                else
                {
                    return "!!!No key value pair found in dictionary!!!";
                }
        }
        return "";
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnglishChosen()
    {
        m_Language = Languages.English;
        m_PlayButtonText.text = m_LocalizationDictionary_English["START"];
        m_QuitButtonText.text = m_LocalizationDictionary_English["QUIT"];
    }

    public void FrenchChosen()
    {
        m_Language = Languages.French;
        m_PlayButtonText.text = m_LocalizationDictionary_French["START"];
        m_PlayButtonText.fontSize = 15;
        m_QuitButtonText.text = m_LocalizationDictionary_French["QUIT"];
        m_QuitButtonText.fontSize = 15;
    }

   
}
