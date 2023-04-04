using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Train()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void GoToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void GoToMenu()
    {
        LanguageSettings.s_Instance.Reset();
        SceneManager.LoadScene("Main Menu");
    }
    public void GoToStory()
    {
        SceneManager.LoadScene("StoryScene");
    }
    public void QuitGame() //For not built version (https://gamedevbeginner.com/how-to-quit-the-game-in-unity/)
    {
        Application.Quit();
    }

    public void ResetLanguage()
    {
        LanguageSettings.s_Instance.m_Language = Languages.English;
    }
}
