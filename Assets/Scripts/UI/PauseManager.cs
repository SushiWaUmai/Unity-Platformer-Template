using UnityEngine;

public class PauseMenuManager : Singleton<PauseMenuManager>
{
    [SerializeField] private GameObject _pauseMenu;
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
}