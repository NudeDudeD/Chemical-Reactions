using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ObjectSelector))]
public class Grabber : MonoBehaviour
{
    private ObjectSelector _selector;
    private Rigidbody _grabbedRigidbody;
    private float _zoomDistance;

    [SerializeField] private Transform _target;
    [SerializeField] private float _grabStability = 0.8f;
    [SerializeField] private float _moveSpeed = 0.125f;
    [SerializeField] private float _zoomMultiplier = 0.0004f;
    [SerializeField] private float _minZoomDistance = -0.2f;
    [SerializeField] private float _maxZoomDistance = 0.7f;

    private void Awake()
    {
        _selector = GetComponent<ObjectSelector>();
    }

    private void Start()
    {
        PlayerInputHolder.GrabAndRelease += GrabReleaseAction;
        PlayerInputHolder.ZoomGrabbed += ZoomInOut;
    }

    private void GrabReleaseAction()
    {
        if (_grabbedRigidbody == null)
            TryGrab();
        else
            Release();
    }

    private void ZoomInOut(float zoomValue)
    {
        _zoomDistance += zoomValue * _zoomMultiplier;
        _zoomDistance = Mathf.Clamp(_zoomDistance, _minZoomDistance, _maxZoomDistance);
    }

    private void FixedUpdate()
    {
        if (_grabbedRigidbody != null)
        {
            Vector3 desiredPosition = _target.position + _target.forward * _zoomDistance;
            _grabbedRigidbody.position = Vector3.MoveTowards(_grabbedRigidbody.position, desiredPosition, _moveSpeed);
            _grabbedRigidbody.rotation = Quaternion.RotateTowards(_grabbedRigidbody.rotation, Quaternion.LookRotation(Vector3.forward, Vector3.up), 180f * _grabStability);
        }
    }

    private void TryGrab()
    {
        if (!_selector.TryGetSelectedComponent(out GrabableObject grabableObject))
            return;
        _grabbedRigidbody = grabableObject.GetComponent<Rigidbody>();
        _grabbedRigidbody.useGravity = false;
        _zoomDistance = 0f;
    }

    private void Release()
    {
        _grabbedRigidbody.useGravity = true;
        _grabbedRigidbody.velocity = Vector3.zero;
        _grabbedRigidbody = null;
    }
}