using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LanguageSettings : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text playButtontext;
    [SerializeField] private TMPro.TMP_Text quitButtontext;
    [SerializeField] private Languages m_Language;

    public static LanguageSettings s_Instance { get; private set; }

    Dictionary<string, string> French = new Dictionary<string, string>(); 

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
        French.Add("Clock In!", "Horloge d'entrée!");
        French.Add("Quit to Desktop", "Quitter sur le bureau");
        French.Add($"Press 'F' to pickup the ", $"Appuyez sur 'F' pour récupérer le ");
        French.Add("example", "exemple");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnglishChosen()
    {
        m_Language = Languages.English;
        playButtontext.text = "Clock In!";
        quitButtontext.text = "Quit to Desktop";
    }

    public void FrenchChosen()
    {
        m_Language = Languages.French;
        playButtontext.text = GetLocalizedString("Clock In!");
        quitButtontext.text = GetLocalizedString("Quit to Desktop");
    }

    public string GetLocalizedString(string sourceStr)
    {
        switch (m_Language)
        {
            case Languages.English:
                return sourceStr;

            case Languages.French:
                if (French.ContainsKey(sourceStr))
                {
                    return French[sourceStr];
                }
                else
                {
                    return "!!!MISSING!!!";
                }
        }
        return "";
    }
}
