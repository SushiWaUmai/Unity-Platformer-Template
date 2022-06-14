using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class PauseManagerLoader : MonoBehaviour
{
    [SerializeField, Scene] private string[] _pausableScenes;
    [SerializeField, Scene] private string _pauseMenuScene;

    private void OnValidate()
    {
        if (_pausableScenes?.Contains(_pauseMenuScene) == true)
        {
            Debug.LogWarning("PausableScenes should not contain PauseMenuScene");
        }
    }

    // On every scene load, check if the scene is pausable and if it is, load the pause menu scene
    // Use the event based system
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_pausableScenes.Contains(scene.name))
        {
            SceneManager.LoadScene(_pauseMenuScene);
        }
    }
}