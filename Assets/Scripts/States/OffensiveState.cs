using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OffensiveState : BaseState
{
    private float _fightDistance;
    private float _speed;
    private float _loseDistance;
    private Rigidbody2D _rigidbody;
    private Action _shoot;

    public OffensiveState(Character character, Enemy stateMachine, Rigidbody2D rigidbody, float fightDistance, float loseDistance, float speed, Action shoot) : base(character, stateMachine, rigidbody.transform)
    {
        _fightDistance = fightDistance;
        _speed = speed;
        _rigidbody = rigidbody;
        _shoot = shoot;
        _loseDistance = loseDistance;
    }

    public override void Enter()
    {
        Debug.Log("Attack");
    }

    public override void UpdateLogic()
    {
        _rigidbody.velocity = -(_transform.position - _characterTransform.position).normalized * _speed;
        _shoot.Invoke();
        if (DistanceToCharacter <= _fightDistance)
        {       
            _stateMachine.SwitchState<FightState>();
        }
        else if (DistanceToCharacter >= _loseDistance)
        {
            
            _stateMachine.SwitchState<IdleState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        _rigidbody.velocity = new Vector2(0, 0);
    }

}
