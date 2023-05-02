using UnityEngine;


public abstract class SkillParameterBase : MonoBehaviour
{
     protected PoolItem poolItemComponent;
    
    [SerializeField]private protected float speed;
    [SerializeField]private protected int projectileCount;
    
    [SerializeField]private protected Skills skillType;

    public int ProjectileCount => projectileCount;

  

    private void Awake()
    {
        CashComponents();
    }

    private void CashComponents()
    {
        if (TryGetComponent(out PoolItem poolObject))
        {
            poolItemComponent = poolObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
