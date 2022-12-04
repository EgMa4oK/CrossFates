using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates
{
    public class Destructible : MonoBehaviour, ITakesDamage
    {
        [SerializeField] private float health;
        public float Health
        {
            get => health;
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

        public void ApplyDamage(float damage)
        {
            if (condition == null || condition.Performed)
            {
                
                Health -= damage;
            }
        }

        private void Destruct()
        {
            Destroy(gameObject);
        }


    }
}
