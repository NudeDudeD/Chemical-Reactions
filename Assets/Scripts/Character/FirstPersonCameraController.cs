using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCameraController : MonoBehaviour
{
    private InputManager _inputManager;
    private float _rotationX;
    private float _rotationY;

    [SerializeField] private float _MinRotationX;
    [SerializeField] private float _MaxRotationX;

    public float SensitivityX;
    public float SensitivityY;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
    }

    private void Start()
    {
        _rotationX = transform.rotation.eulerAngles.x;
        _rotationY = transform.rotation.eulerAngles.y;

        _inputManager.PlayerRotate.performed += Rotate;
    }

    private void Rotate(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        Vector2 rotation = new Vector2(-input.y * SensitivityX, input.x * SensitivityY);

        _rotationX += rotation.x;
        if (_rotationX >= 360f)
            _rotationX -= 360f;
        _rotationX = Mathf.Clamp(_rotationX, _MinRotationX, _MaxRotationX);
        _rotationY += rotation.y;
        if (_rotationY >= 360f)
            _rotationY -= 360f;

        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
    }
}
