using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrossFates.PlayerStates
{
    public class DashState : PlayerState
    {
        private float _speed;
        public DashState(PlayerCharacter character, Rigidbody2D rigidbody, float speed) : base(character, rigidbody)
        {
            _speed = speed;
        }
        public override void Enter()
        {
            _rigidbody.velocity = _character.LastDirection * _speed;
            Timer();
        }
        public override void Exit()
        {
            base.Exit();
            _rigidbody.velocity = Vector2.zero;
        }
        private async void Timer()
        {
            await Task.Delay(50);
            _character.SwitchState<IdleState>();
        }
    }
}