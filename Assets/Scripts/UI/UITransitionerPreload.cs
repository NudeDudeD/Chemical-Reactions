using UnityEngine;

[RequireComponent(typeof(UITransitioner))]
public class UITransitionerPreload : MonoBehaviour
{
    private void Start()
    {
        UITransitioner transitioner = GetComponent<UITransitioner>();
        transitioner.SetAsActiveTransitioner();
    }
}