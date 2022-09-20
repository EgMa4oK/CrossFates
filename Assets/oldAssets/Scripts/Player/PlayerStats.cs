using UnityEngine;
using System;

[CreateAssetMenuAttribute(fileName = "new PlayerStats", menuName = "PlayerStats", order = 50)]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private int maxHealth;
    private float health;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float friction;

    [SerializeField] private float damage;
    [SerializeField] float stun;
    [SerializeField] float ourNormalStun = 0.4f;

    public Action OnHealthChange;


    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value > MaxHealth)
            {
                value = MaxHealth;
            }
            health = value;
            OnHealthChange?.Invoke();
        }
    }

    public float MaxSpeed => maxSpeed;
    public float Acceleration => acceleration;
    public float Friction => friction;

    public float Damage => damage;
    public float Stun => stun;
    public float OurNormalStun => ourNormalStun;

    private void OnEnable()
    {
        Respawn();
    }

    public void Respawn()
    {
        health = maxHealth;
    }
}
