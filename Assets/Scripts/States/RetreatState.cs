using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;

namespace CrossFates
{
    public class RetreatState : BaseState
    {
        private float _fightDistance;
        private float _speed;
        private Rigidbody2D _rigidbody;
        private FieldOfView _fieldOfView;
        private NavMeshAgent _agent;

        public RetreatState(Enemy stateMachine) : base(stateMachine)
        {
            _fightDistance = stateMachine.FightDistance;
            _speed = stateMachine.RetreatSpeed;
            _rigidbody = stateMachine.Rigidbody;
            _fieldOfView = stateMachine.FieldOfView;
            _agent = stateMachine.Agent;
        }

        public override void Enter()
        {
            Debug.Log("Retreat");
        }

        public override void UpdateLogic()
        {
            _agent.SetDestination(_targetTransform.position + (_transform.position - _targetTransform.position).normalized * _fightDistance);
            if (DistanceToCharacter >= _fightDistance)
            {
                _stateMachine.SwitchState<FightState>();
            }
            else if (!_fieldOfView.VisibleTargets.Contains(_targetTransform))
            {
                _stateMachine.LastTargetPosition = _targetTransform.position;
                _stateMachine.SwitchState<SearchState>();
            }
        }

        public override void Exit()
        {
            base.Exit();
            _agent.SetDestination(_transform.position);
        }

    }

}