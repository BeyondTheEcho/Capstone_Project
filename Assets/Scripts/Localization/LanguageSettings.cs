using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//using System.IO;
//using static Unity.VisualScripting.Icons;

public class LanguageSettings : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_PlayButtonText;
    [SerializeField] private TMPro.TMP_Text m_QuitButtonText;

    //set default to English, so programmer can directly start from game scene
    [SerializeField] public Languages m_Language;
    [SerializeField] private TextAsset m_FrenchTXT;
    [SerializeField] private TextAsset m_EnglishTXT;
    [SerializeField] private TextAsset m_MandarinTXT;

    public static LanguageSettings s_Instance { get; private set; }

    readonly Dictionary<string, string> m_LocalizationDictionary_French = new Dictionary<string, string>();
    readonly Dictionary<string, string> m_LocalizationDictionary_English = new Dictionary<string, string>(); 
    readonly Dictionary<string, string> m_LocalizationDictionary_Mandarin = new Dictionary<string, string>();

    public TMP_FontAsset m_MandarinFont;
    public TMP_FontAsset m_DefaultFont;
    public static TMP_FontAsset m_FinalFont;

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
        m_FinalFont = m_DefaultFont;
        ReadFile(m_FrenchTXT, m_LocalizationDictionary_French);
        ReadFile(m_EnglishTXT, m_LocalizationDictionary_English);
        ReadFile(m_MandarinTXT, m_LocalizationDictionary_Mandarin);
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
            //print("Line= " + Lines[i]);
            string[] line = Lines[i].Split(splitKeyValue, System.StringSplitOptions.None);
            string key=line[0];
            string value=line[1];
            languageDictionary.Add(key, value);

        }
    
    }

    public string GetLocalizedString(string key)
    {
        ChangeFont(m_FinalFont);

        switch (m_Language)
        {
            case Languages.English:
                
                if (m_LocalizationDictionary_English.ContainsKey(key))
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
            case Languages.Mandarin:
                
                if (m_LocalizationDictionary_Mandarin.ContainsKey(key))
                {
                    return m_LocalizationDictionary_Mandarin[key];
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
        m_FinalFont = m_DefaultFont;
        ChangeFont(m_DefaultFont);
        m_PlayButtonText.text = m_LocalizationDictionary_English["START"];
        m_PlayButtonText.fontSize = 24;

        m_QuitButtonText.text = m_LocalizationDictionary_English["QUIT"];
        m_QuitButtonText.fontSize = 24;


    }

    public void FrenchChosen()
    {
        m_Language = Languages.French;
        m_FinalFont = m_DefaultFont;
        ChangeFont(m_DefaultFont);
        m_PlayButtonText.text = m_LocalizationDictionary_French["START"];
        m_PlayButtonText.fontSize = 15;
        m_QuitButtonText.text = m_LocalizationDictionary_French["QUIT"];
        m_QuitButtonText.fontSize = 15;

    }

    public void MandarinChosen()
    {
        ChangeFont(m_MandarinFont);
        m_Language = Languages.Mandarin;
        m_FinalFont = m_MandarinFont;
        m_PlayButtonText.text = m_LocalizationDictionary_Mandarin["START"];
        m_PlayButtonText.fontSize = 15;
        m_QuitButtonText.text = m_LocalizationDictionary_Mandarin["QUIT"];
        m_QuitButtonText.fontSize = 15;

    }

    public static void ChangeFont(TMP_FontAsset font)
    {
        TMP_Text[] t;
        t = GameObject.FindObjectsOfType<TMP_Text>();

        for (int i = 0; i < t.Length; i++)
        {
            t[i].font = font;
        }
    }

    public void Reset()
    {
        m_Language = Languages.English;
        Destroy(this);
    }
}
