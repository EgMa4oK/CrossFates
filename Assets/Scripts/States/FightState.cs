using System;
using UnityEngine;

namespace CrossFates
{
    public class FightState : BattleState
    {
        private float _retreatDistance;
        private float _offensiveDistance;
        private Action _shoot;

        public FightState(Enemy stateMachine, Action shoot) : base(stateMachine)
        {
            _retreatDistance = stateMachine.RetreatDistance;
            _offensiveDistance = stateMachine.OffensiveDistance;
            _shoot = shoot;
        }

        public override void Enter()
        {
            Debug.Log("Another Test Subject");
        }

        public override void UpdateLogic()
        {
            _stateMachine.Facing.CheckDirection(_targetTransform.position - _transform.position);

            if (DistanceToCharacter >= _offensiveDistance)
            {
                _stateMachine.SwitchState<OffensiveState>();
            }
            else if (DistanceToCharacter <= _retreatDistance)
            {
                _stateMachine.SwitchState<RetreatState>();
            }

            _shoot.Invoke();
            

        }

    }

}