using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    [SerializeField] Player player;
    private PlayerStats stats;

    private void Awake()
    {
        healthBar = GetComponent<Image>();
        stats = player.Stats;
    }

    private void OnEnable()
    {
        stats.OnHealthChange += changeValue;
    }

    private void OnDisable()
    {
        stats.OnHealthChange -= changeValue;
    }

    private void changeValue()
    {
        healthBar.fillAmount = stats.Health / (float)stats.MaxHealth;
    }

}
