using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrossFates.PlayerStates
{
    public class DashState : PlayerState
    {
        private Vector2 _direction;
        private float _speed;
        private int _dashTime = 50;
        private LayerMask _obstacleLayer;

        public DashState(PlayerCharacter character, Rigidbody2D rigidbody, float speed, LayerMask obstacleLayer) : base(character, rigidbody)
        {
            _speed = speed;
            _obstacleLayer = obstacleLayer;
        }
        public override void Enter()
        {
            _direction = _character.LastDirection.normalized;
            
            
            Timer();
        }
        public override void Exit()
        {
            base.Exit();
            _rigidbody.velocity = Vector2.zero;
            _character.DashTime = Time.time;
        }
        private async void Timer()
        {
            await Task.Delay(_dashTime);
            _character.SwitchState<IdleState>();
        }

        public override void PhysicUpdate()
        {
            var distance = _speed * Time.fixedDeltaTime;
            
            RaycastHit2D hit = Physics2D.Raycast(_rigidbody.position, _direction, distance, _obstacleLayer.value);
            if (hit.collider != null)
            {
                var position = hit.point;
                var ourPosition = _rigidbody.ClosestPoint(position);
                _rigidbody.MovePosition(hit.point + _rigidbody.position - ourPosition);
            }
            else 
            {
                _rigidbody.MovePosition(_rigidbody.position + _direction * distance);
            }

        }
    }
}