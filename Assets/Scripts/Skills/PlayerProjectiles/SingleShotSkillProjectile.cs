using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SingleShotSkillProjectile : SkillParameterBase, ISkillProjectile
{
    [SerializeField] private Rigidbody projectileRigidBody;

    [Header("Radius")] [SerializeField] private float radius;
    private Collider[] _enemyTargets;
    private int _enemyCounter = 0;
    [Header("Parametres")] [SerializeField]

    private Transform _thisTransform;
    private TargetsHolder _targetsHolder;
    private Collider target;

    private void Start()
    {
        SubscribeOnEvents();
        _thisTransform = transform;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }


    public void Move()
    {
        if(target!=null)
            _thisTransform.LookAt(target.transform.position,Vector3.up);
        projectileRigidBody.AddForce(_thisTransform.forward * speed,ForceMode.Acceleration);
    }
    public TargetsHolder TargetHolder
    {
        set => _targetsHolder = value;
    }
    
    #region Event subsctiption

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

    #endregion
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_thisTransform.position, radius);
    }


    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.layer == Layers.EnemyZombie||other.gameObject.layer == Layers.EnemySkeleton||other.gameObject.layer == Layers.EnemyGhost)
          poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);

    }


    #region Pool Item Component Events Reactions
    private void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
       projectileRigidBody.velocity = Vector3.zero;
       transform.rotation = Quaternion.identity;
    }

    private void PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction()
    {
        if (_targetsHolder == null)
        
        {
            poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
        }

        else
        {
            target = _targetsHolder.GetTarget();
        }
        Move();
    }
    #endregion Pool Item Component Events Reactions
   
}
