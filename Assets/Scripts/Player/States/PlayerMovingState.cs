using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates.PlayerStates
{
    public abstract class MovingState : PlayerState
    {
        private Vector2 _direction;
        private Controls _controls;
        private float _speed;
        public MovingState(PlayerCharacter character, Rigidbody2D rigidbody, float speed, Controls controls) : base(character, rigidbody)
        {
            _controls = controls;
            _speed = speed;
        }
        public override void Update() 
        {
            _direction = _controls.Character.Axis.ReadValue<Vector2>();
            Move();
            if (_direction == Vector2.zero)
            {
                _character.SwitchState<IdleState>();
            }

        }
        private void Move()
        {
            _rigidbody.velocity = _direction * _speed;
        }
        private void Dash()
        {

        }
        public override void Exit() 
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}