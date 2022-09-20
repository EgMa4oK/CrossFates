using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private float _detectionDistance = 5f;
    public IdleState(Character character, IStateSwitcher stateMachine, Transform transform) :
        base(character, stateMachine, transform)
    { 
    }


    public override void Enter()
    {
        Debug.Log("standing here");
    }


    public override void UpdateLogic()
    {
        if (DistanceToCharacter < _detectionDistance)
        {
            _stateMachine.SwitchState<FightState>();
        }
    }

    public override void UpdatePhysics()
    {
    }
    
}
