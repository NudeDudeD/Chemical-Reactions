﻿using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    private GameObject _selectedObject;
    [SerializeField] private float _selectDistance = 2f;

    public bool TryGetSelectedComponent<T>(out T component)
    {
        if (_selectedObject == null)
        {
            component = default;
            return false;
        }
        else
            return _selectedObject.TryGetComponent(out component);
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _selectDistance))
            _selectedObject = hit.transform.gameObject;
        else
            _selectedObject = null;
    }
}