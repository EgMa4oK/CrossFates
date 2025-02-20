using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CrossFates
{
    public class PlayerAttack : MonoBehaviour
    {
        private float reset;
        private float cooldown = 0.2f;
        private int countLight = 0;

        private Vector2 axis = new Vector2(0, 0);
        [SerializeField] LayerMask enemy_layer;
        [SerializeField] Attack lightAttack;

        private IEnumerator StandingHere;
        private Controls _input;



        [SerializeField] private float _ourNormalStun = 2;
        [SerializeField] private float _baseDamage = 2;
        [SerializeField] private float _stunTime = 2;

        private void Awake()
        {
            _input = InputManager.Input;
            
        }

        private void OnEnable()
        {
            _input.Character.LightAttack.performed += LightAttack;
        }

        private void OnDisable()
        {
            _input.Character.LightAttack.performed -= LightAttack;
        }


        private void LightAttack(InputAction.CallbackContext a)
        {
            if (Time.time > cooldown)
            {
                axis = _input.Character.MousePosition.ReadValue<Vector2>();
                axis = ((Vector2)(Camera.main.ScreenToWorldPoint(axis) - transform.position)).normalized * lightAttack.AttackRange;
                effect(lightAttack.AttackVFX);

                if (Time.time >= reset) { countLight = 1; }

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

                foreach (Collider2D collider in Physics2D.OverlapCircleAll((Vector2)transform.position + axis, lightAttack.AttackRadius))
                {
                    ITakesDamage target = collider.GetComponent<ITakesDamage>();
                    if (target != null)           
                    collider.GetComponent<ITakesDamage>().ApplyDamage(_baseDamage * lightAttack.DamageModifier);
                }

            }
        }



        public void Stun(float stun)
        {
            StopCoroutine(StandingHere);
            StartCoroutine(StandingHere);
            cooldown = Time.time + stun;
        }

        private void effect(GameObject attackVFX)
        {
            GameObject a;
            a = Instantiate(attackVFX, (Vector2)transform.position + axis, Quaternion.FromToRotation(Vector2.left, axis));
            Destroy(a, 1f);
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere((Vector2)transform.position + axis.normalized * lightAttack.AttackRange, lightAttack.AttackRadius);
        }
    }
}
