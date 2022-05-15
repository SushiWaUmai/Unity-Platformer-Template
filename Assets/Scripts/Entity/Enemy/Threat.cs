using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Threat : MonoBehaviour
{
    public event System.Action OnHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerLife>().Hit();
            OnHit?.Invoke();
        }
    }
}