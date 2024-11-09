using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitcher
{
    private const string _laboratorySceneName = "Laboratory Scene";
    private const string _mainMenuSceneName = "Main Menu";
    public static event Action<string> OnSceneLoaded;

    private static void LoadScene(string sceneName)
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        sceneLoad.completed += (AsyncOperation obj) => OnSceneLoaded.Invoke(sceneName);
    }

    public static void LoadMainMenu()
    {
        LoadScene(_mainMenuSceneName);
    }

    public static void LoadLaboratory() 
    {
        ChemistryStorage.Initialize();
        IconStorage.Initialize();
        UIInputHolder.Initialize();
        PlayerInputHolder.Initialize();

        LoadScene(_laboratorySceneName);
    }

    public static void Exit() => Application.Quit();
}