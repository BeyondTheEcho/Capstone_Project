using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LocalizationGrabber : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    { 
        TMP_Text textbox = this.GetComponent<TMP_Text>();
        textbox.text  = LanguageSettings.s_Instance.GetLocalizedString(textbox.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
