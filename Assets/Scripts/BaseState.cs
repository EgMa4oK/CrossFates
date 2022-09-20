using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    
    protected readonly Character _character; 
    protected readonly Enemy _stateMachine;
    protected readonly Transform _transform;
    protected readonly Transform _characterTransform;

    protected float DistanceToCharacter => Vector2.Distance(_transform.position, _characterTransform.position);

    public BaseState(Character character, Enemy stateMachine, Transform transform)
    {
        _character = character;
        _stateMachine = stateMachine;
        _transform = transform;
        _characterTransform = character.transform;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }

   

}
