using UnityEngine;

public class AnimationSignal : MonoBehaviour
{
    public event System.Action OnSignal;

    public void SignalTrigger()
    {
        OnSignal?.Invoke();
    }
}
