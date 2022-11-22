using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CrossFates
{
    public class HealthBar : MonoBehaviour
    {
        private Image healthBar;
        [SerializeField] PlayerCharacter player;
        private PlayerStats stats;

        private void Awake()
        {
            healthBar = GetComponent<Image>();
        }

        private void OnEnable()
        {
            player.HealthChanged += changeValue;
        }

        private void OnDisable()
        {
            player.HealthChanged -= changeValue;
        }

        private void changeValue(float health, float maxHealth)
        {
            healthBar.fillAmount = health / maxHealth;
        }

    }
}
