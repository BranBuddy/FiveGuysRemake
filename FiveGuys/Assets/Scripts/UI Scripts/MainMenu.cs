using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject select;

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Settings()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    public void SelectToMain()
    {
        select.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}