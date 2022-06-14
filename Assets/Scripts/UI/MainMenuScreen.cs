using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField, Scene] private string _gameScene;

    public void StartGame()
    {
        SceneManager.LoadScene(_gameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
