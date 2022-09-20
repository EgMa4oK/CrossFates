using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyMove enemyMove;
    private float cooldown = 1;
    [SerializeField] private LayerMask PlayerLayer;
    private GameObject player;
    [SerializeField] private int damage = 2;
    [SerializeField] private GameObject attackVFX;
    [SerializeField] float attackRadius = 0.9f;
    [SerializeField] float attackArea = 2f;
    [SerializeField] float stun = 1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyMove = GetComponent<EnemyMove>();
    }

    void Update()
    {
        if (enemyMove.Distance < attackRadius && Time.time > cooldown) 
        {

            Vector3 axis = ((Vector2)(player.transform.position - transform.position)).normalized * attackRadius;
            Collider2D playerColider = Physics2D.OverlapCircle(transform.position + axis, attackArea, PlayerLayer);
            if (playerColider != null)
            {
                cooldown = Time.time + 2f;
                player.GetComponent<Player>().TakeDamage(damage);
                player.GetComponent<PlayerAttack>().Stun(stun);
                StartCoroutine(Effect(axis));
            }
            
        }

    }

    private IEnumerator Effect(Vector2 axis)
    {
        GameObject VFX;
        VFX = Instantiate(attackVFX, (Vector2)transform.position + (axis / 2), Quaternion.FromToRotation(Vector2.left, axis));
        yield return new WaitForSeconds(1f);
        Destroy(VFX);
    }

    public void Stun(float stun)
    {
        if (Time.time + stun > cooldown) 
        { 
            cooldown = Time.time + stun; 
        }
    }
}
