using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputButtonExtension : MonoBehaviour
{
    [SerializeField] private InputActionReference input;
    [SerializeField] private UnityEvent onClick;

    private void OnEnable()
    {
        input.ToInputAction().Enable();
        input.action.performed += OnInput;
    }

    private void OnDisable()
    {
        input.action.performed -= OnInput;
    }

    private void OnInput(InputAction.CallbackContext obj)
    {
        onClick?.Invoke();
    }
}