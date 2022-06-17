using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SimpleTrigger : MonoBehaviour
{
    [SerializeField] private string _targetTag = "Player";
    public event System.Action OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (string.IsNullOrEmpty(_targetTag) || other.CompareTag(_targetTag))
        {
            OnTrigger?.Invoke();
        }
    }
}
