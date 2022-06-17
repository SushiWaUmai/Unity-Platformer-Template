using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int _maxLifes = 3;
    public int MaxLifes { get { return _maxLifes; } }

    public int Lifes { get; private set; }

    public bool IsDead => Lifes <= 0;

    public event System.Action OnDeath;
    public event System.Action<int> OnHit;
    public event System.Action<int> OnHeal;
    public event System.Action<int> OnChangeLife;

    public void Heal(int heal)
    {
        if (IsDead)
            return;

        ChangeLife(heal + Lifes);
        OnHeal?.Invoke(Lifes);
    }

    public void TakeDamage(int damage)
    {
        if (IsDead)
            return;

        ChangeLife(Lifes - damage);

        if (IsDead)
        {
            Die();
            return;
        }

        OnHit?.Invoke(Lifes);
    }

    public void ChangeLife(int life)
    {
        Lifes = Mathf.Min(life, MaxLifes);
        OnChangeLife?.Invoke(Lifes);
    }

    public void Die()
    {
        OnDeath?.Invoke();
    }
}
