using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private enum Languages
    {
        English, French
    }
    private Languages languageChosen; 
    public string GetPickUpHintText()
    {
        languageChosen = Languages.French;
        switch (languageChosen)
        {
            case Languages.English:
                return $"Press 'F' to pickup the ";

            case Languages.French:
                return $"Appuyez sur 'F' pour récupérer le ";
        }
        return "";
    }
}
