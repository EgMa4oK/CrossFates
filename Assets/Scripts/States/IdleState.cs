using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private float _findDistance;
    public IdleState(Character character, Enemy stateMachine, Transform transform, float findDistance) :
        base(character, stateMachine, transform)
    {
        _findDistance = findDistance;
    }


    public override void Enter()
    {
        Debug.Log("standing here");
    }


    public override void UpdateLogic()
    {
        if (DistanceToCharacter < _findDistance)
        {
            _stateMachine.SwitchState<FightState>();
        }
    }    
}
