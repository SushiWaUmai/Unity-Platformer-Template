using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class PauseMenuManager : Singleton<PauseMenuManager>
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField, Scene] private string _mainMenuScene;
    private bool _isPaused;

    public bool CanPause = true;

    public void SetPause(bool isPaused)
    {
        if (CanPause)
        {
            _isPaused = isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            _pauseMenu.SetActive(isPaused);
        }
    }

    public void TogglePause()
    {
        SetPause(!_isPaused);
    }

    public void BackToMainMenu()
    {
        SetPause(false);
        SceneManager.LoadScene(_mainMenuScene);
    }

    public void QuitGame() => Application.Quit();
}