using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D), typeof(FieldOfView))]
public class Enemy : MonoBehaviour, IStateSwitcher
{
    [SerializeField] private Character _target;
    [SerializeField] private float _retreatDistance;
    [SerializeField] private float _fightDistance;
    [SerializeField] private float _offensiveDistance;

    [SerializeField] private float _retreatSpeed;
    [SerializeField] private float _offensiveSpeed;

    [SerializeField] private Rigidbody2D _projectile;
    [SerializeField] private float _shotCD;
    [SerializeField] private float _projctileSpeed;

    private FieldOfView _fieldOfView;
    private Rigidbody2D _rigidbody;
    private BaseState _currentState;
    private List<BaseState> _allStates;

    private Transform _transform;
    private float _lastShotTime = 0f;

    public FieldOfView FieldOfView => _fieldOfView;
    public Transform Transform => _transform;
    public Rigidbody2D Rigidbody => _rigidbody;
    public Character Target => _target;
    public float RetreatDistance => _retreatDistance;
    public float FightDistance => _fightDistance;
    public float OffensiveDistance => _offensiveDistance;
    public float RetreatSpeed => _retreatSpeed;
    public float OffensiveSpeed => _offensiveSpeed;



    private void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = transform;
        _allStates = new List<BaseState>()
        {
            new IdleState(this),
            new FightState(this, Shoot),
            new RetreatState(this),
            new OffensiveState(this, Shoot),          
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

    public void SwitchState<T>() where T : BaseState
    {
        var state = _allStates.FirstOrDefault(s => s is T);

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }




}
