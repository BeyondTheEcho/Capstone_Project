using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
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
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame() //For not built version (https://gamedevbeginner.com/how-to-quit-the-game-in-unity/)
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
