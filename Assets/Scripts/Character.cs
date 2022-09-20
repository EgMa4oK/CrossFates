using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private Controls _controls;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controls = new Controls();
        _controls.Enable();
    }

    void Update()
    {
        _rigidbody.velocity = _controls.Player.Axis.ReadValue<Vector2>() * _speed;
    }
}
