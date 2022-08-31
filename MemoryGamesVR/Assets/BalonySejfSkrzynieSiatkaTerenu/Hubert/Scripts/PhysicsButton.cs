using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = 5.0f;
    [SerializeField] private float deadZone = 2.0f;

    private bool _isPreseed;
    private Vector3 _startPos;
    private ConfigurableJoint _join;

    public UnityEvent onPressed, onReleased;
    void Start()
    {
        _startPos = transform.localPosition;
        _join = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        if (!_isPreseed && GetValue() + threshold >= 10)
            Pressed();
        if (_isPreseed && GetValue() - threshold <= 0)
            Released();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _join.linearLimit.limit;
        float x = Vector3.Distance(_startPos, transform.localPosition);
        if (Math.Abs(value) < deadZone)
            value = 0;
        return Mathf.Clamp(value, -100f, 100f);
    }

    private void Pressed()
    {
        _isPreseed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }

    private void Released()
    {
        _isPreseed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }
}
