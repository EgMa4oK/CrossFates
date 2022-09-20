using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour, IStateSwitcher
{
    [SerializeField] private Character _character;

    private BaseState _currentState;
    private List<BaseState> _allStates;


    private void Start()
    {
        
        var t = transform;
        _allStates = new List<BaseState>()
        {
            new IdleState(_character, this, t),
            new FightState(_character, this, t),

        };
        _currentState = _allStates[0];

    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateLogic();
        }
    }

    private void FixedUpdate()
    {
        if (_currentState == null)
        {
            _currentState.UpdatePhysics();
        }
    }

    public void SwitchState<T>() where T: BaseState
    {
        var state = _allStates.FirstOrDefault(s => s is T);

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }






}
