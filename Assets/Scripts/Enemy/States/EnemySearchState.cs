using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace CrossFates.EnemyStates {
    public class SearchState : EnemyState
    {
        private NavMeshAgent _agent;
        private Vector3 _targetPosition;
        private FieldOfView _fieldOfView;
        public SearchState(Enemy stateMachine) : base(stateMachine)
        {
            _agent = stateMachine.Agent;
            _fieldOfView = stateMachine.FieldOfView;
        }
        public override void Enter()
        {
            base.Enter();
            _stateMachine.AllyFindTarget += OnEnemyFinded;
            _targetPosition = _stateMachine.LastTargetPosition;
            Debug.Log($"{_targetPosition}, {_stateMachine.LastTargetPosition}");
            _agent.SetDestination(_targetPosition);
            Debug.Log("searching...");
        }
        public override void UpdateLogic()
        {

            base.UpdateLogic();
            _stateMachine.Facing.CheckDirection(_agent.desiredVelocity);
            if ((_transform.position - _targetPosition).magnitude <= 0.15)
            {
                _stateMachine.SwitchState<IdleState>();
            }
            if (_fieldOfView.VisibleTargets.Contains(_targetTransform))
            {
                _stateMachine.SwitchState<FightState>();
            }

        }

        public void OnEnemyFinded(Vector2 targetPosition)
        {
            _agent.SetDestination(_targetPosition);
        }

        public override void Exit()
        {
            _stateMachine.AllyFindTarget -= OnEnemyFinded;
            _agent.SetDestination(_transform.position);
            Debug.Log("On target last position");
        }

    }

}

