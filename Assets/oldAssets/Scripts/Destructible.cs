using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, ITakesDamage
{
    [SerializeField] private float health;
    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                Destruct();
            }
        }
    }
    [SerializeField] Condition condition;

    public void TakeDamage(float damage)
    {
        if (condition.Performed)
        {
            Health -= damage;
        }
    }

    private void Destruct()
    {
        Destroy(gameObject);
    }





}
