using TMPro;
using UnityEngine;

public class MessageBox : UITransitioner
{
    private static MessageBox _instance;

    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _message;

    public static void Show(string title, string message)
    {
        _instance.SetActive();
        _instance._title.text = title;
        _instance._message.text = message;
        _instance.gameObject.SetActive(true);
    }

    private void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        _instance = canvas.transform.GetChild(canvas.transform.childCount - 1).GetComponent<MessageBox>();
    }

    public override void Switch()
    {
        _switchTo.SetActive();
        _onSwitch.Invoke();
        gameObject.SetActive(false);
    }

    public override void SetActive()
    {
        _switchTo = _activeTransitioner;
        _inputActions.Switch.performed -= _activeTransitioner.Switch;

        _activeTransitioner = this;
        _inputActions.Switch.performed += Switch;
    }
}