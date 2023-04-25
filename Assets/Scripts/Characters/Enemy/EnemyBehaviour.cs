using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected PoolItem poolItemComponent;

    private void Awake()
    {
        CashComponents();
        CashHeirComponents();
    }

    public abstract void CashHeirComponents();

    private void CashComponents()
    {
        if (TryGetComponent(out PoolItem poolObject))
        {
            poolItemComponent = poolObject;
        }
    }
}
