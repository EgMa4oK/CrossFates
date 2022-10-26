using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using CrossFates.EnemyStates;
using System;

namespace CrossFates
{
    [RequireComponent(typeof(Rigidbody2D), typeof(FieldOfView), typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour, ITakesDamage
    {
        [SerializeField] private PlayerCharacter _target;
        [SerializeField] private float _retreatDistance;
        [SerializeField] private float _fightDistance;
        [SerializeField] private float _offensiveDistance;
        [SerializeField] private float _maxHealth;

        [SerializeField] private float _retreatSpeed;
        [SerializeField] private float _offensiveSpeed;

        [SerializeField] private Rigidbody2D _projectile;
        [SerializeField] private float _shotCD;
        [SerializeField] private float _projctileSpeed;
        [SerializeField] private Facing _facing;

        private FieldOfView _fieldOfView;
        private Rigidbody2D _rigidbody;
        private EnemyState _currentState;
        private List<EnemyState> _allStates;
        private float _health;
        private NavMeshAgent _agent;
        private SpriteRenderer _spriteRenderer;
        private Transform _transform;
        private float _lastShotTime = 0f;

        public event Action<Vector2> AllyFindTarget;
        public Facing Facing => _facing;
        public NavMeshAgent Agent => _agent;
        public FieldOfView FieldOfView => _fieldOfView;
        public Transform Transform => _transform;
        public Rigidbody2D Rigidbody => _rigidbody;
        public PlayerCharacter Target => _target;
        public Vector3 LastTargetPosition {get; set;}
        public float RetreatDistance => _retreatDistance;
        public float FightDistance => _fightDistance;
        public float OffensiveDistance => _offensiveDistance;
        public float RetreatSpeed => _retreatSpeed;
        public float OffensiveSpeed => _offensiveSpeed;



        private void Start()
        {
            _facing.Init();
            _health = _maxHealth;
            _agent = GetComponent<NavMeshAgent>();
            _fieldOfView = GetComponent<FieldOfView>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _transform = transform;
            _allStates = new List<EnemyState>()
            {
                new IdleState(this),
                new FightState(this, Shoot),
                new RetreatState(this),
                new OffensiveState(this, Shoot),
                new SearchState(this),
            };
            SwitchState<IdleState>();
            StartCoroutine(UpdateLogic());
        }

        private void Update()
        {
            _spriteRenderer.sprite = _facing.Sprite;
            _fieldOfView.Direction = _facing.Direction;
        } 
        

        private void Die()
        {
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            if (_currentState == null)
            {
                _currentState.UpdatePhysics();
            }
            
        }

        private void Shoot()
        {
            if (Time.time - _shotCD >= _lastShotTime)
            {
                var a = - _transform.position + _target.transform.position;
                var angle = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;
                var projectile = Instantiate<Rigidbody2D>(_projectile, transform.position, Quaternion.Euler(0, 0, angle));
                projectile.velocity = projectile.transform.right * _projctileSpeed;
                Destroy(projectile.gameObject, 5f);
                _lastShotTime = Time.time;
            }
        }

        private IEnumerator UpdateLogic()
        {
            while (true) 
            {
                if (_currentState != null)
                {
                    _currentState.UpdateLogic();
                }

                yield return new WaitForSeconds(0.2f);
            }
        }

        public void SwitchState<T>() where T : EnemyState
        {
            var state = _allStates.FirstOrDefault(s => s is T);
            if (_currentState != null)
            {
                _currentState.Exit();
            } 
            _currentState = state;
            _currentState.Enter();
        }

        public void ApplyDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        public void Alert(Vector2 position)
        {
            if (AllyFindTarget != null)
            {
                AllyFindTarget.Invoke(position);
            }         
        }

        public void Heal(float heal)
        {
            if (_health + heal > _maxHealth)
            {
                _health = _maxHealth;
            }
            else
            {
                _health += heal;
            }
        }

    }

}