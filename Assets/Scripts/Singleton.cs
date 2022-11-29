using Unity.VisualScripting;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance;

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning($"There is another {typeof(T).Name} on scene!");
            return;
        }

        Instance = this as T;
    }
}