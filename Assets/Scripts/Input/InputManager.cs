using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls _playerControls;

    public InputAction PlayerRotate { get; private set; }

    private void Awake()
    {
        _playerControls = new PlayerControls();
        PlayerRotate = _playerControls.PlayerTransform.Rotate;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
}
