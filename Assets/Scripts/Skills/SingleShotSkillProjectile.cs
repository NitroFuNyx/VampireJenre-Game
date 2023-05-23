using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SingleShotSkillProjectile : SkillParameterBase, ISkillProjectile
{
    [SerializeField] private Rigidbody projectileRigidBody;
    [SerializeField] private LayerMask targetLayers;

    [Header("Radius")] [SerializeField] private float radius;
    private Collider[] _enemyTargets;
    private int _enemyCounter = 0;
    [Header("Parametres")] [SerializeField]
    private int jumpsCount;

    private Transform _thisTransform;
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
        projectileRigidBody.AddForce(_thisTransform.forward * speed,ForceMode.Acceleration);
    }

    private void GetTargets()
    {
        _enemyTargets = Physics.OverlapSphere(_thisTransform.position, radius, targetLayers);
        _enemyCounter = 0;

    }

    private void IncreaseCounter()
    {
        if (_enemyTargets.Length < _enemyCounter)
            _enemyCounter++;
        else
            _enemyCounter = 0;
    }
    private void LockTarget()
    {
        if (_enemyTargets.Length !=0)
        {
            _thisTransform.forward = _enemyTargets[_enemyCounter].transform.position - _thisTransform.position;
        }
        else
        {
            _thisTransform.rotation = Quaternion.Euler(0, Random.Range(0, 361), 0);

        }

        IncreaseCounter();
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
        GetTargets();
        LockTarget();
        Move();
    }
    #endregion Pool Item Component Events Reactions
   
}
