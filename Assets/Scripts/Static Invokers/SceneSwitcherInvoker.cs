using UnityEngine;

public class SceneSwitcherInvoker : MonoBehaviour
{
    public void LoadMainMenu() => SceneSwitcher.LoadMainMenu();
    public void LoadLaboratory() => SceneSwitcher.LoadLaboratory();
    public void Exit() => SceneSwitcher.Exit();
}