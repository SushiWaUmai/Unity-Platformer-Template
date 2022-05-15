using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private int _lifes;

    public event System.Action OnDie;
    public event System.Action<int> OnHit;

    public void Hit() => TakeDamage(1);

    public void TakeDamage(int damage)
    {
        _lifes -= damage;

        if (_lifes <= 0)
        {
            Die();
            return;
        }

        OnHit?.Invoke(_lifes);
    }

    public void Die()
    {
        OnDie?.Invoke();
    }
}
