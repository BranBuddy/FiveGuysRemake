using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    public AudioClip deathClip;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, 0.1f);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}