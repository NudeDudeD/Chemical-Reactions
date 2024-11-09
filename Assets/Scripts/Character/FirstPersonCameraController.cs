using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCameraController : MonoBehaviour
{
    private const float _minRotationX = -90f;
    private const float _maxRotationX = 90f;

    private float _rotationX;
    private float _rotationY;

    public float SensitivityX;
    public float SensitivityY;

    private void Start()
    {
        _rotationX = transform.rotation.eulerAngles.x;
        _rotationY = transform.rotation.eulerAngles.y;

        PlayerInputHolder.RotatePlayer += Rotate;
    }

    private void Rotate(Vector2 input)
    {
        Vector2 rotation = new Vector2(-input.y * SensitivityX, input.x * SensitivityY);

        _rotationX += rotation.x;
        _rotationX = Mathf.Clamp(_rotationX, _minRotationX, _maxRotationX);
        _rotationY += rotation.y;
        if (_rotationY >= 360f)
            _rotationY -= 360f;

        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
    }
}