using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<T>();
                if (!_instance)
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }

            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (!_instance)
            _instance = this as T;
        else
            Destroy(gameObject);
    }
}
