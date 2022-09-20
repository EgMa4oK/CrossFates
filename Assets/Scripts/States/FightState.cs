using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : BaseState
{
    private float _lostDistance = 8f;

    public FightState(Character character, IStateSwitcher stateMachine, Transform transform) : base(character, stateMachine, transform)
    {
    }

    public override void Enter()
    {
        Debug.Log("Another Test Subject");
    }

    public override void UpdateLogic()
    {

        if (DistanceToCharacter > _lostDistance)
        {
            _stateMachine.SwitchState<IdleState>();
        }

    }

}
