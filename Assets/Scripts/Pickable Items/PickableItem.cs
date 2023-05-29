using UnityEngine;

public abstract class PickableItem : MonoBehaviour
{
    [Header("Vfx")]
    [Space]
    [SerializeField] protected ParticleSystem pickingUpVfx;

    protected PoolItem poolItemComponent;

    protected bool itemPickedUp = false;

    private void Awake()
    {
        CashComponents();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.Player && !itemPickedUp)
        {
            itemPickedUp = true;
            pickingUpVfx.Play();
            PlayerCollision_ExecuteReaction();
        }
    }

    protected abstract void PlayerCollision_ExecuteReaction();
    protected abstract void ResetItem();

    private void CashComponents()
    {
        if (TryGetComponent(out PoolItem poolObject))
        {
            poolItemComponent = poolObject;
        }
    }
}
