using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstacleLayer;
        [SerializeField, Min(0)] private float _damage;
        [SerializeField, Min(0)] private float _speed;
        private Rigidbody2D _rigidbody;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = transform.right * _speed;
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider == null) return;

            if (1 << collider.gameObject.layer == _obstacleLayer.value)
            {
                print(collider.gameObject.layer);
                Destroy(gameObject);
            }

            var player = collider.gameObject.GetComponent<PlayerCharacter>();
            if (player != null)
            {
                player.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }

    }
}
