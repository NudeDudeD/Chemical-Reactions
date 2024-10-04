using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconFrame : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private RawImage _rawImage;

    public void Redraw(string text, Texture image)
    {
        _rawImage.texture = image;
        _text.text = text;
    }
}