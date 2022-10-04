using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates
{
    public abstract class BaseState
    {

        protected readonly Enemy _stateMachine;
        protected readonly Character _target;
        protected readonly Transform _transform;
        protected readonly Transform _targetTransform;

        protected float DistanceToCharacter => Vector2.Distance(_transform.position, _targetTransform.position);

        public BaseState(Enemy stateMachine)
        {

            _stateMachine = stateMachine;
            _target = stateMachine.Target;
            _transform = stateMachine.Transform;
            _targetTransform = _target.transform;
        }

        public virtual void Enter() { }
        public virtual void UpdateLogic() { }
        public virtual void UpdatePhysics() { }
        public virtual void Exit() { }

    }

}