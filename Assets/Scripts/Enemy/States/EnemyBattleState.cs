using System.Linq;
using UnityEngine;

namespace CrossFates.EnemyStates
{
    public class BattleState : EnemyState
    {
        private FieldOfView _fieldOfView;
        public BattleState(Enemy stateMachine) : base(stateMachine)
        {
            _fieldOfView = stateMachine.FieldOfView;
        }
        public override void Enter()
        {
            base.Enter();

        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            var colliders = Physics2D.OverlapCircleAll(_transform.position, 8f);
            foreach (var collider in colliders)
            {
                var target = collider.GetComponent<Enemy>();
                if (target != null)
                {
                    target.Alert(_targetTransform.position);
                }
            }
            if (!_fieldOfView.VisibleTargets.Contains(_targetTransform))
            {
                _stateMachine.LastTargetPosition = _targetTransform.position;
                _stateMachine.SwitchState<SearchState>();
            }
        }
    }
}
