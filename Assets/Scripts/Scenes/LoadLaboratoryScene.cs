using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLaboratoryScene : MonoBehaviour
{
    private const string _sceneName = "Laboratory Scene";

    public void Load()
    {
        DataStorage.Initialize();
        IconStorage.Initialize();

        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
    }
}