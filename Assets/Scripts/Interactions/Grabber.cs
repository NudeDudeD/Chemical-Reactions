using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputManager))]
public class Grabber : MonoBehaviour
{
    private Rigidbody _grabbedRigidbody;
    private float _zoomDistance;

    [SerializeField] private Transform _target;
    [SerializeField] private float _grabStability = 0.8f;
    [SerializeField] private float _grabDistance = 1f;
    [SerializeField] private float _moveSpeed = 0.125f;
    [SerializeField] private float _zoomMultiplier = 0.0002f;
    [SerializeField] private float _minZoomDistance = -0.3f;
    [SerializeField] private float _maxZoomDistance = 1f;

    private void Start()
    {
        InputManager inputManager = GetComponent<InputManager>();
        inputManager.GrabAndRelease.performed += GrabReleaseAction;
        inputManager.ZoomGrabbed.performed += ZoomInOut;
    }

    private void GrabReleaseAction(InputAction.CallbackContext context)
    {
        if (_grabbedRigidbody == null)
            Grab();
        else
            Release();
    }

    private void ZoomInOut(InputAction.CallbackContext context)
    {
        float zoom = context.ReadValue<float>();
        _zoomDistance += zoom * _zoomMultiplier;
        _zoomDistance = Mathf.Clamp(_zoomDistance, _minZoomDistance, _maxZoomDistance);
    }

    private void FixedUpdate()
    {
        if (_grabbedRigidbody != null)
        {
            Vector3 desiredPosition = _target.position + _target.forward * _zoomDistance;
            _grabbedRigidbody.position = Vector3.MoveTowards(_grabbedRigidbody.position, desiredPosition, _moveSpeed);
            _grabbedRigidbody.rotation = Quaternion.RotateTowards(_grabbedRigidbody.rotation, Quaternion.Euler(Vector3.up), 180f * _grabStability);
        }
    }

    private void Grab()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _grabDistance))
            if (hit.transform.TryGetComponent(out GrabableObject grabable))
            {
                _grabbedRigidbody = grabable.GetComponent<Rigidbody>();
                _grabbedRigidbody.useGravity = false;
                _zoomDistance = 0f;
            }
    }

    private void Release()
    {
        _grabbedRigidbody.useGravity = true;
        _grabbedRigidbody.velocity = Vector3.zero;
        _grabbedRigidbody = null;
    }
}