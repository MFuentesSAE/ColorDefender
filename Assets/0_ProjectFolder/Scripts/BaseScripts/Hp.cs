using UnityEngine;
using UnityEngine.Events;

public class Hp : MonoBehaviour
{
    [Header("Settings")]
    public int startingHp;
    
    [Space]
    public UnityEvent onRemoveHpEvent, onDeathEvent;

    [Header("Debug")]
    [SerializeField]
    protected int currentHp;
    
    
    public void InstaKill()
    {
        ReduceHp(startingHp);
    }

    protected virtual void Start()
    {
        currentHp = startingHp;
    }

    public virtual void ReduceHp(int amount)
    {
        currentHp -= amount;
        currentHp = ClampHp(currentHp);

        if (currentHp <= 0)
        {
            Death();
            return;
        }

        onRemoveHpEvent?.Invoke();
    }

    public virtual void AumentHp(int amount)
    {
        currentHp += amount;
        currentHp = ClampHp(currentHp);
    }

    protected virtual int ClampHp(int hpValue)
    {
        return Mathf.Clamp(currentHp, 0, startingHp);
    }

    protected virtual void Death()
    {
        onDeathEvent?.Invoke();
    }
}
