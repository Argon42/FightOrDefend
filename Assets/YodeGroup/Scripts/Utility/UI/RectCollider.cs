using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(BoxCollider), typeof(RectTransform))]
public class RectCollider : MonoBehaviour
{
    [SerializeField] private bool colliderEnabled = true;
    [SerializeField] private bool isTrigger;

    private BoxCollider _collider;
    private RectTransform _rectTransform;

    public bool IsTrigger
    {
        get => isTrigger;
        set => isTrigger = value;
    }

    public bool ColliderEnabled
    {
        get => colliderEnabled;
        set => colliderEnabled = value;
    }

    private void OnEnable()
    {
        _collider = GetComponent<BoxCollider>();
        _rectTransform = (RectTransform) transform;
        _collider.hideFlags = HideFlags.NotEditable;
    }

    private void OnDisable()
    {
        _collider.hideFlags = HideFlags.None;
    }

    private void LateUpdate()
    {
        _collider.isTrigger = IsTrigger;
        _collider.size = _rectTransform.rect.size;
        _collider.center = _rectTransform.rect.center;
        _collider.enabled = ColliderEnabled;
    }
}