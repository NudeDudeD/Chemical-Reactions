using UnityEngine;

[RequireComponent(typeof(UITransitioner))]
public class UITransitionerPreload : MonoBehaviour
{
    private void Awake()
    {
        UITransitioner transitioner = GetComponent<UITransitioner>();
        transitioner.SetActive();
    }
}