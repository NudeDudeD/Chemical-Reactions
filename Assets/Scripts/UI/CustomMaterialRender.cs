using UnityEngine;

public class CustomMaterialRender : MonoBehaviour
{
    private static CustomMaterialRender _instance;
    [SerializeField] private Renderer _renderer;

    public static Material Material
    {
        get
        {
            return _instance._renderer.sharedMaterial;
        }
        set
        {
            _instance._renderer.sharedMaterial = value;
        }
    }

    public static void SetActive(bool activity)
    {
        if (_instance == null)
            return;

        _instance.gameObject.SetActive(activity);
    }

    private void Awake()
    {
        _instance = this;
    }
}