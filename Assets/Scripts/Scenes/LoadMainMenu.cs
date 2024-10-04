using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    private const string _sceneName = "Main Menu";

    public void Load() => SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
}