using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepeaterBool : MonoBehaviour
{
    [SerializeField] private UnityEventBool boolCallback;
    [SerializeField] private UnityEventBool invertedBoolCallback;

    public void Receive(bool value)
    {
        boolCallback?.Invoke(value);
        invertedBoolCallback?.Invoke(!value);
    }

    [Serializable]
    private class UnityEventBool : UnityEvent<bool>
    {
    }
}