using UnityEngine;

public class DarkMatterSkillProjectile : SkillParameterBase, ISkillProjectile
{
    [SerializeField] private Rigidbody projectileRigidBody;

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    public void Move()
    {
       // transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
        projectileRigidBody.AddForce(transform.forward * speed, ForceMode.Acceleration);
    }

    #region Event subsctiption

    private void SubscribeOnEvents()
    {
        if (poolItemComponent != null)
        {
            poolItemComponent.OnItemResetRequired += PoolItemComponent_ResetRequired_ExecuteReaction;
            poolItemComponent.OnObjectAwakeStateSet += PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction;
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (poolItemComponent != null)
        {
            poolItemComponent.OnItemResetRequired -= PoolItemComponent_ResetRequired_ExecuteReaction;
            poolItemComponent.OnObjectAwakeStateSet -= PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction;
        }
    }

    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == Layers.Player)
            poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
    }
    #region Pool Item Component Events Reactions

    private void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        projectileRigidBody.velocity = Vector3.zero;
    }

    

    private void PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction()
    {
        Move();
    }

    #endregion Pool Item Component Events Reactions
}