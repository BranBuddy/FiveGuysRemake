using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    private PauseMenu menu;

    private void Start()
    {
        menu = GetComponent<PauseMenu>();
    }

    public void PlayGame()
    {
       
        SceneManager.LoadScene("WhiteBox");
        StartCoroutine(LoadScene());
       

    }

    public void Settings()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
    }
    
}