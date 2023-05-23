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
    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.layer == Layers.EnemyZombie||other.gameObject.layer == Layers.EnemySkeleton||other.gameObject.layer == Layers.EnemyGhost)
            poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);

    }
    private void PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction()
    {
        Move();
    }
    #endregion Pool Item Component Events Reactions

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.layer == Layers.ObstaclesOnMap )
    //     {
    //         //poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
    //
    // }
    // }
    
}
