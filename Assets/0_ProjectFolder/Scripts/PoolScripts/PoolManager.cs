using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public GameObjectPool poolProjectile;

    private void Awake()
    {
        instance = this;
    }
}
