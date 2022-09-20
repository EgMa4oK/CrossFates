using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour, ITakesDamage
{
    [SerializeField] float health;
    private IEnumerator StandingHere;

    void Start()
    {
        StandingHere = GetComponent<EnemyMove>().Stun(0);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Stun(float stun, float damage)
    {
        if (health - damage > 0)
        {
            StopCoroutine(StandingHere);
            GetComponent<EnemyAttack>().Stun(stun);
            StandingHere = GetComponent<EnemyMove>().Stun(stun);
            StartCoroutine(StandingHere);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
        
}
