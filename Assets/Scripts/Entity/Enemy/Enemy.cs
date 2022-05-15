using UnityEngine;

/// <summary>
/// Class that handles the Enemy
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// The event that is called when the Enemy is defeated
    /// </summary>
    public event System.Action OnDeath;

    /// <summary>
    /// When the Enemy is defeated, it will fall down and then be destroyed
    /// </summary>
    public void Die()
    {
        // Invoke the OnDeath event
        OnDeath?.Invoke();
    }
}
