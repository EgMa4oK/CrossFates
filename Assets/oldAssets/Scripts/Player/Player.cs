using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
   
    [SerializeField] private PlayerStats stats;
    public PlayerStats Stats => stats;

    private void Start()
    {

        stats.OnHealthChange += Die;
    }

    private void Die()
    {
        if (stats.Health <= 0)
        {
            Respawn();
        }
    }

    public void TakeDamage(int damage)
    {
        stats.Health -= damage;
    }

    private void Respawn()
    {
        Stats.Respawn();
        SceneManager.LoadScene("SampleScene");
    }

}
