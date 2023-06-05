using UnityEngine;
using System.Collections;

public abstract class PickableItem : MonoBehaviour
{
    [Header("Vfx")]
    [Space]
    [SerializeField] protected ParticleSystem pickingUpVfx;
    [SerializeField] protected ParticleSystem idleVfx;
    [Header("Positions")]
    [Space]
    [SerializeField] private Transform vfxPlayPos;

    protected PoolItem poolItemComponent;

    protected bool itemPickedUp = false;

    private void Awake()
    {
        CashComponents();
    }

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnEnable()
    {
        ResetVfx();
        itemPickedUp = false;

        if(idleVfx)
        {
            idleVfx.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.Player && !itemPickedUp)
        {
            itemPickedUp = true;
            pickingUpVfx.transform.SetParent(null) ;
            pickingUpVfx.transform.position = vfxPlayPos.position;
            pickingUpVfx.Play();
            PlayerCollision_ExecuteReaction();
        }
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeOnEvents()
    {
        if (poolItemComponent != null)
        {
            poolItemComponent.OnItemResetRequired += PoolItemComponent_ResetRequired_ExecuteReaction;
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (poolItemComponent != null)
        {
            poolItemComponent.OnItemResetRequired -= PoolItemComponent_ResetRequired_ExecuteReaction;
        }
    }

    protected abstract void PlayerCollision_ExecuteReaction();

    protected abstract void PoolItemComponent_ResetRequired_ExecuteReaction();

    protected void ResetVfx()
    {
        pickingUpVfx.transform.SetParent(transform);
        pickingUpVfx.transform.localPosition = vfxPlayPos.localPosition;
    }

    private void CashComponents()
    {
        if (TryGetComponent(out PoolItem poolObject))
        {
            poolItemComponent = poolObject;
        }
    }
}
