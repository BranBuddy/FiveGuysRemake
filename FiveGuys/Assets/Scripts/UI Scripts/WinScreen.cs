using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
   
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }


    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}