using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput input;
    private float reset;
    private float cooldown;
    private int countLight = 0;
    
    private Vector2 axis = new Vector2(0, 0);
    [SerializeField] LayerMask enemy_layer;
    
    [SerializeField] Attack lightAttack;
    [SerializeField] Attack heavyAttack;

    private PlayerStats stats;

    private IEnumerator StandingHere;

    private void Awake()
    {
        input = InputManager.Input;
        stats = GetComponent<Player>().Stats;
        StandingHere = MoveReturn(stats.OurNormalStun);
    }

    private void OnEnable()
    {
        input.Player.LightAttack.performed += LightAttack;
        input.Player.HeavyAttack.performed += HeavyAttack;
    }

    private void OnDisable()
    {
        input.Player.LightAttack.performed -= LightAttack;
        input.Player.HeavyAttack.performed -= HeavyAttack;
    }


    private void LightAttack(InputAction.CallbackContext a)
    {
        if (Time.time > cooldown)
        {

            StopCoroutine(StandingHere); 

            axis = input.Player.MousePosition.ReadValue<Vector2>();
            axis = ((Vector2)(Camera.main.ScreenToWorldPoint(axis) - transform.position)).normalized * lightAttack.AttackRange;
            StartCoroutine(effect(lightAttack.AttackVFX));

            StandingHere = MoveReturn(stats.OurNormalStun);
            StartCoroutine(StandingHere);

            if (Time.time >= reset) {countLight = 1;}

            reset = Time.time + 3.5f;

            if (countLight >= 4)
            {
                cooldown = Time.time + 0.9f;
                countLight = 1;
            }
            else 
            {
                cooldown = Time.time + 0.35f;
                countLight += 1;
            }

            foreach (Collider2D enemy in Physics2D.OverlapCircleAll((Vector2)transform.position + axis, lightAttack.AttackRadius, enemy_layer))
            {
                enemy.GetComponent<ITakesDamage>().TakeDamage(stats.Damage * lightAttack.DamageModifier);
                Enemy EnemyComp = enemy.GetComponent<Enemy>();
                if (EnemyComp != null)
                {
                    EnemyComp.Stun(stats.Stun, stats.Damage * lightAttack.DamageModifier);
                }
            }
            lightAttack.AttackSound.pitch = Random.Range(0.9f, 1.1f);
            lightAttack.AttackSound.Play();
        }
    }

    private void HeavyAttack(InputAction.CallbackContext a)
    {
        if (Time.time > cooldown)
        {

            if (Time.time >= reset)
            {
                countLight = 1;
            }
            else
            {
                reset = Time.time + 5;
            }

            StopCoroutine(StandingHere);

            axis = input.Player.MousePosition.ReadValue<Vector2>();
            axis = ((Vector2)(Camera.main.ScreenToWorldPoint(axis) - transform.position)).normalized * heavyAttack.AttackRange;
            StartCoroutine(effect(heavyAttack.AttackVFX));
            cooldown = Time.time + 1.25f;
            countLight = 1;

            StandingHere = MoveReturn(stats.OurNormalStun * 1.5f);
            StartCoroutine(StandingHere);

            foreach (Collider2D enemy in Physics2D.OverlapCircleAll((Vector2)transform.position + axis, heavyAttack.AttackRadius, enemy_layer))
            {
                enemy.GetComponent<ITakesDamage>().TakeDamage(stats.Damage * heavyAttack.DamageModifier);
                Enemy EnemyComp = enemy.GetComponent<Enemy>();
                if (EnemyComp != null)
                {
                    EnemyComp.Stun(stats.Stun * 2.5f, stats.Damage * heavyAttack.DamageModifier);
                }
            }
            heavyAttack.AttackSound.pitch = Random.Range(0.9f, 1.1f);
            heavyAttack.AttackSound.Play();
        }
    }

    public void Stun(float stun)
    {
        StopCoroutine(StandingHere);
        StandingHere = MoveReturn(stun);
        StartCoroutine(StandingHere);
        cooldown = Time.time + stun;
    }

    private IEnumerator effect(GameObject attackVFX)
    {
        GameObject a;
        a = Instantiate(attackVFX, (Vector2)transform.position + axis, Quaternion.FromToRotation(Vector2.left, axis));
        yield return new WaitForSeconds(1f);
        Destroy(a);

    }
    private IEnumerator MoveReturn(float sec)
    {
        input.Player.Move.Disable();
        yield return new WaitForSeconds(sec);
        input.Player.Move.Enable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + axis.normalized * lightAttack.AttackRadius, lightAttack.AttackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + axis.normalized * heavyAttack.AttackRadius, heavyAttack.AttackRadius);
    }
}
