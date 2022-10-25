using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CrossFates.PlayerStates;

namespace CrossFates {

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerCharacter : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private PlayerState _currentState;
        private List<PlayerState> _allStates;
        private Controls _controls;
        [SerializeField] float _speed = 1;
        [SerializeField, Min(1)] float _speedModifier = 2;
        [SerializeField, Min(1)] float _dashModifier = 4;
        public Vector2 LastDirection;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _controls = new Controls();
            _controls.Enable();
            _allStates = new List<PlayerState>
            {
                new IdleState(this, _rigidbody, _controls),
                new AttackState(this, _rigidbody),
                new StunState(this, _rigidbody),
                new WalkingState(this, _rigidbody, _speed, _controls),
                new RunningState(this, _rigidbody, _speed * _speedModifier, _controls),
                new ParryState(this, _rigidbody),
                new DashState(this, _rigidbody, _speed * _dashModifier),
            };
            _currentState = _allStates[0];
        }

        public void SwitchState<T>() where T : PlayerState
        {
            var state = _allStates.FirstOrDefault(s => s is T);
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        private void Update()
        {
            _currentState.Update();
        }
    }
}