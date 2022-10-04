using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace CrossFates
{
    public class RetreatState : BaseState
    {
        private float _fightDistance;
        private float _speed;
        private Rigidbody2D _rigidbody;
        private FieldOfView _fieldOfView;

        public RetreatState(Enemy stateMachine) : base(stateMachine)
        {
            _fightDistance = stateMachine.FightDistance;
            _speed = stateMachine.RetreatSpeed;
            _rigidbody = stateMachine.Rigidbody;
            _fieldOfView = stateMachine.FieldOfView;
        }

        public override void Enter()
        {
            Debug.Log("Retreat");
        }

        public override void UpdateLogic()
        {
            _rigidbody.velocity = (_transform.position - _targetTransform.position).normalized * _speed;
            if (DistanceToCharacter >= _fightDistance)
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