using System;
using UnityEngine;

public class MissilesSkillProjectile : SkillParameterBase, ISkillProjectile
{
    [SerializeField] private Rigidbody projectileRigidBody;

    public void Move()
    {
        projectileRigidBody.AddForce(transform.forward * speed,ForceMode.Acceleration);
    }

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }


    public void SetObjectDirection()
    {
        throw new System.NotImplementedException();
    }
    
    private void SubscribeOnEvents()
    {
        if(poolItemComponent != null)
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

    protected override void CollideWithMapObstacle()
    {
        poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
    }
}
