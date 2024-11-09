using System.Collections.Generic;
using UnityEngine;

public class PopupHandler : MonoBehaviour
{
    [SerializeField] private List<Popup> _popups;
    private Canvas _canvas;

    private void Awake()
    {       
        _canvas = GetComponent<Canvas>();
        SceneSwitcher.OnSceneLoaded += SetCamera;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (Popup popup in _popups)
        {
            Popup popupObject = Instantiate(popup, transform);
            popupObject.Initialize();
        }
    }

    private void SetCamera(string sceneName) => _canvas.worldCamera = Camera.main;
}