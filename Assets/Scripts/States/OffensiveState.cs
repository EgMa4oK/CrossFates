using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace CrossFates
{
    public class OffensiveState : BaseState
    {
        private float _fightDistance;
        private float _speed;
        private Rigidbody2D _rigidbody;
        private FieldOfView _fieldOfView;
        private Action _shoot;

        public OffensiveState(Enemy stateMachine, Action shoot) : base(stateMachine)
        {
            _fightDistance = stateMachine.FightDistance;
            _speed = stateMachine.OffensiveSpeed;
            _rigidbody = stateMachine.Rigidbody;
            _fieldOfView = stateMachine.FieldOfView;
            _shoot = shoot;
        }

        public override void Enter()
        {
            Debug.Log("Attack");
        }

        public override void UpdateLogic()
        {
            _rigidbody.velocity = -(_transform.position - _targetTransform.position).normalized * _speed;
            _shoot.Invoke();
            if (DistanceToCharacter <= _fightDistance)
            {
                _stateMachine.SwitchState<FightState>();
            }
            else if (!_fieldOfView.VisibleTargets.Contains(_targetTransform))
            {

                _stateMachine.SwitchState<IdleState>();
            }
        }

        public override void Exit()
        {
            base.Exit();
            _rigidbody.velocity = new Vector2(0, 0);
        }

    }

}