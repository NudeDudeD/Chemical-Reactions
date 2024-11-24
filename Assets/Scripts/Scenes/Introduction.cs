using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Introduction : MonoBehaviour
{
    [SerializeField] private Graphic _tintGraphic;

    private void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        _tintGraphic.CrossFadeAlpha(0f, .5f, false);
        yield return new WaitForSeconds(1f); 
        _tintGraphic.CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1f);
        SceneSwitcher.LoadLaboratory();
    }
}