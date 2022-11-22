using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CrossFates.EnemyStates
{
    public class IdleState : EnemyState
    {

        private FieldOfView _fieldOfView;

        public IdleState(Enemy stateMachine) :
            base(stateMachine)
        {
            _fieldOfView = stateMachine.FieldOfView;
        }


        public override void Enter()
        {
            _stateMachine.AllyFindTarget += OnEnemyFinded;
        }

        public override void Exit()
        {
            base.Exit();
            _stateMachine.AllyFindTarget -= OnEnemyFinded;
        }


        public override void UpdateLogic()
        {
            if (_fieldOfView.VisibleTargets.Contains(_targetTransform))
            {
                _stateMachine.SwitchState<FightState>();
            }
        }

        private void OnEnemyFinded(Vector2 targetPosition)
        {
            _stateMachine.LastTargetPosition = targetPosition;
            _stateMachine.SwitchState<SearchState>();
        }
    }

}
