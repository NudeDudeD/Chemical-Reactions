using UnityEngine.SceneManagement;

public static class SceneSwitcher
{
    private const string _laboratorySceneName = "Laboratory Scene";
    private const string _mainMenuSceneName = "Main Menu";

    public static void LoadMainMenuStatic()
    {
        SceneManager.LoadScene(_mainMenuSceneName, LoadSceneMode.Single);
    }

    public static void LoadLaboratoryStatic() 
    {
        DataStorage.Initialize();
        IconStorage.Initialize();

        SceneManager.LoadSceneAsync(_laboratorySceneName, LoadSceneMode.Single);
    }
}