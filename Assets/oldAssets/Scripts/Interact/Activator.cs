using UnityEngine.Events;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] private UnityEvent onActivate;
    [SerializeField] private bool canActivateWhenEnemy;
    [SerializeField] LayerMask enemy_layer;
    [SerializeField] private bool canRepeat;

    private int count = 0;

    public bool CanActivate()
    {
        if (!canRepeat && count >= 1)
        {
            return false;
        }
        if (canActivateWhenEnemy)
        {
            return true;
        }
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 100, enemy_layer);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>())
            {
                return false;
            }
        }
        return true;

    }

    public virtual void Activate()
    {
        if (CanActivate())
        {
            onActivate.Invoke();
            count++;
        }
    }


}
