using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RetreatState : BaseState
{
    private float _fightDistance;
    private float _speed;
    private Rigidbody2D _rigidbody;

    public RetreatState(Character character, Enemy stateMachine, Rigidbody2D rigidbody, float fightDistance, float speed) : base(character, stateMachine, rigidbody.transform)
    {
        _fightDistance = fightDistance;
        _speed = speed;
        _rigidbody = rigidbody;
    }

    public override void Enter()
    {
        Debug.Log("Retreat");
    }

    public override void UpdateLogic()
    {
        _rigidbody.velocity = (_transform.position - _characterTransform.position).normalized * _speed;
        if (DistanceToCharacter >= _fightDistance)
        {
            _stateMachine.SwitchState<FightState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        _rigidbody.velocity = new Vector2(0, 0);
    }

}
