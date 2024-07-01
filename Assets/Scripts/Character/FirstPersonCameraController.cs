using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputManager))]
public class FirstPersonCameraController : MonoBehaviour
{
    private const float _MinRotationX = -90f;
    private const float _MaxRotationX = 90f;

    private float _rotationX;
    private float _rotationY;

    public float SensitivityX;
    public float SensitivityY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _rotationX = transform.rotation.eulerAngles.x;
        _rotationY = transform.rotation.eulerAngles.y;

        GetComponent<InputManager>().RotatePlayer.performed += Rotate;
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
