using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Reference to the pause menu UI panel
    public GameObject pauseMenuUI;

    // Bool to track if the game is paused
    private bool isPaused = false;

    // Name of the main menu scene
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    void Start()
    {
        // Make sure the pause menu is hidden on start
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    void Update()
    {
        // Check for the Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Disable the pause menu UI
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        // Resume the game by setting timeScale to 1
        Time.timeScale = 1f;

        // Update the pause state
        isPaused = false;
    }

    public void StartGame()
    {
        Resume();
    }

    public void Pause()
    {
        // Enable the pause menu UI
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }

        // Pause the game by setting timeScale to 0
        Time.timeScale = 0f;

        // Update the pause state
        isPaused = true;
    }

    public void ReturnToMainMenu()
    {
        // Resume time before loading menu
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
