using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : BaseState
{
    private float _retreatDistance;
    private float _offensiveDistance;
    private Action _shoot;

    public FightState(Character character, Enemy stateMachine, Transform transform, float retreatDistance, float offensiveDistance, Action shoot) : base(character, stateMachine, transform)
    {
        _retreatDistance = retreatDistance;
        _offensiveDistance = offensiveDistance;
        _shoot = shoot;
    }

    public override void Enter()
    {
        Debug.Log("Another Test Subject");
    }

    public override void UpdateLogic()
    {

        if (DistanceToCharacter >= _offensiveDistance)
        {
            _stateMachine.SwitchState<OffensiveState>();       
        }
        else if (DistanceToCharacter <=  _retreatDistance)
        {
            _stateMachine.SwitchState<RetreatState>();
        }
        _shoot.Invoke();

    }

}
