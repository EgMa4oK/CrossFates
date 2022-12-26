using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CrossFates.PlayerStates;
using System;

namespace CrossFates {

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerCharacter : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private PlayerState _currentState;
        private List<PlayerState> _allStates;
        private Controls _controls;

        private float _health;
        [SerializeField]
        private float _maxHealth;

        [SerializeField]
        private LayerMask _obstaclesLayer;
        [SerializeField]
        private float _speed = 1;
        [SerializeField, Min(1)]
        private float _speedModifier = 2;
        [SerializeField, Min(1)]
        private float _dashModifier = 4;


        public Vector2 LastDirection;
        public event Action<float, float> HealthChanged;

        private void Start()
        {
            var level = LevelManager.Level; 
            transform.position = level == null? Vector2.zero : level.SpawnPoint;
            
            _health = _maxHealth;
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
                new DashState(this, _rigidbody, _speed * _dashModifier, _obstaclesLayer),
            };
            _currentState = _allStates[0];
        }
        private void OnDisable()
        {
            _currentState.Exit();
        }

        private void Update()
        {
            if (_currentState != null)
            {
                _currentState.Update();
            }
        }

        private void FixedUpdate()
        {
            if (_currentState != null)
            {
                _currentState.PhysicUpdate();
            }
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            HealthChanged.Invoke(_health, _maxHealth);
            if (_health <= 0)
            {
                Die();
            }
        }



        public void SwitchState<T>() where T : PlayerState
        {
            var state = _allStates.FirstOrDefault(s => s is T);
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}