using UnityEngine;

public class SceneSwitcherInvoker : MonoBehaviour
{
    public void LoadMainMenu() => SceneSwitcher.LoadMainMenuStatic();
    public void LoadLaboratory() => SceneSwitcher.LoadLaboratoryStatic();
}