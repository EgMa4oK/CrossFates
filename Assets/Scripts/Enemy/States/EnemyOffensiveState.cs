using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;

namespace CrossFates.EnemyStates
{
    public class OffensiveState : BattleState
    {
        private float _fightDistance;
        private float _speed;
        private Rigidbody2D _rigidbody;
        private FieldOfView _fieldOfView;
        private Action _shoot;
        private NavMeshAgent _agent;

        public OffensiveState(Enemy stateMachine, Action shoot) : base(stateMachine)
        {
            _fightDistance = stateMachine.FightDistance;
            _speed = stateMachine.OffensiveSpeed;
            _rigidbody = stateMachine.Rigidbody;
            _fieldOfView = stateMachine.FieldOfView;
            _shoot = shoot;
            _agent = stateMachine.Agent;
        }

        public override void Enter()
        {
            Debug.Log("Attack");
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            _agent.SetDestination(_targetTransform.position);
            _shoot.Invoke();
            _stateMachine.Facing.CheckDirection(_agent.desiredVelocity);
            if (DistanceToCharacter <= _fightDistance)
            {
                _stateMachine.SwitchState<FightState>();
            }
        }

        public override void Exit()
        {
            base.Exit();
            _agent.SetDestination(_transform.position);
        }

    }

}