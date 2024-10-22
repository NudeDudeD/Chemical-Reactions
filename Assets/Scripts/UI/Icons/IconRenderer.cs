using UnityEngine;

public class IconRenderer : MonoBehaviour
{
    private static IconRenderer _instance;

    public static Material Material { get => _instance._renderer.sharedMaterial; set => _instance._renderer.sharedMaterial = value; }

    [SerializeField] private Renderer _renderer;
    [SerializeField] private Camera _renderingCamera;

    public static void SetActive(bool activity)
    {
        if (_instance == null)
            return;

        _instance.gameObject.SetActive(activity);
    }

    public static Texture2D RenderCameraImage(Material material)
    {
        _instance._renderer.sharedMaterial = material;
        _instance._renderingCamera.Render();

        RenderTexture.active = _instance._renderingCamera.activeTexture;

        int width = RenderTexture.active.width;
        int height = RenderTexture.active.height;
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();
        RenderTexture.active = null;
        return texture;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
    }
}