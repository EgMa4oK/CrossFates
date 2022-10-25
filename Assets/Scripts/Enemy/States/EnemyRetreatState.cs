using UnityEngine;
using UnityEngine.AI;

namespace CrossFates.EnemyStates
{
    public class RetreatState : BattleState
    {
        private float _fightDistance;
        private float _speed;
        private Rigidbody2D _rigidbody;
        private NavMeshAgent _agent;

        public RetreatState(Enemy stateMachine) : base(stateMachine)
        {
            _fightDistance = stateMachine.FightDistance;
            _speed = stateMachine.RetreatSpeed;
            _rigidbody = stateMachine.Rigidbody;  
            _agent = stateMachine.Agent;
        }

        public override void Enter()
        {
            Debug.Log("Retreat");
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            _agent.SetDestination(_targetTransform.position + (_transform.position - _targetTransform.position).normalized * _fightDistance);
            _stateMachine.Facing.CheckDirection(_agent.desiredVelocity);
            if (DistanceToCharacter >= _fightDistance)
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