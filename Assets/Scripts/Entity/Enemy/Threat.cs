using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Threat : MonoBehaviour
{
    public int Damage = 1;
    public event System.Action OnHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerLife>().TakeDamage(Damage);
            OnHit?.Invoke();
        }
    }
}