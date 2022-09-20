using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Attack
{
    [SerializeField] private float damageModifier;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRadius;
    [SerializeField] private GameObject attackVFX;
    [SerializeField] private AudioSource attackSound;

    public float DamageModifier => damageModifier;
    public float AttackRange => attackRange;
    public float AttackRadius => attackRadius;
    public GameObject AttackVFX => attackVFX;
    public AudioSource AttackSound => attackSound;

}
