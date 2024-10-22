using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MessageBox : UITransitioner
{
    public enum Buttons
    {
        Ok,
        OkCancel
    }

    public delegate void MessageBoxCallback(bool callback);

    private static MessageBox _instance;
    private static event MessageBoxCallback Callback = delegate { };

    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _message;
    [SerializeField] private GameObject _okButtons;
    [SerializeField] private GameObject _okCancelButtons;

    private static void CallOnce(bool callback, MessageBoxCallback methodToCall)
    {
        methodToCall.Invoke(callback);
        Callback -= (bool callback) => CallOnce(callback, methodToCall);
    }

    public static void Show(string title, string message, Buttons buttons = Buttons.Ok)
    {
        _instance._switchTo = _activeTransitioner;
        _instance.SetAsActiveTransitioner();
        switch (buttons)
        {
            case Buttons.Ok:
                _instance._okButtons.SetActive(true);
                break;
            case Buttons.OkCancel:
                _instance._okCancelButtons.SetActive(true);
                break;
        }
        _instance._title.text = title;
        _instance._message.text = message;
        _instance.gameObject.SetActive(true);
    }

    public static void Show(string title, string message, MessageBoxCallback methodToCall, Buttons buttons = Buttons.Ok)
    {
        Show(title, message, buttons);
        Callback += (bool callback) => CallOnce(callback, methodToCall);
    }

    private void Awake()
    {
        _instance = this;
        gameObject.SetActive(false);
    }

    private void TurnOffButtons()
    {
        _okButtons.SetActive(false);
        _okCancelButtons.SetActive(false);
    }

    public override void SetAsActiveTransitioner()
    {
        if (_activeTransitioner != null)
            _inputActions.Switch.performed -= _activeTransitioner.Switch;

        _inputActions.Switch.performed += Switch;
        gameObject.SetActive(true);
        _activeTransitioner = this;
    }

    public override void Switch()
    {
        base.Switch();
        TurnOffButtons();
        Callback.Invoke(true);
    }

    public void Switch(bool callback)
    {
        base.Switch();
        TurnOffButtons();
        Callback.Invoke(callback);
    }
}