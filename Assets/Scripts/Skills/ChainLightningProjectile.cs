using System.Collections.Generic;
using UnityEngine;

public class ChainLightningProjectile : SkillParameterBase, ISkillProjectile
{
    [SerializeField] private Rigidbody projectileRigidBody;

    [Header("Radius")] [SerializeField] private float radius;
    private List<Collider> enemyList;
    private int _enemyCounter = 0;

    [Header("Parametres")] [SerializeField]
    private int jumpsCount;

    private Transform _thisTransform;

    private bool isShot;
    private Collider target;
    private TargetsHolder _targetsHolder;

   


    private void Start()
    {
        SubscribeOnEvents();
        _thisTransform = transform;
    }

   

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
    public TargetsHolder TargetHolder
    {
        set => _targetsHolder = value;
    }

    public int JumpsCount
    {
        get => jumpsCount;
        set => jumpsCount = value;
    }

    private void FixedUpdate()
    {
        if (isShot)
        {
           
            if(_targetsHolder.GetTarget()==null)
            {
                poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
            }

            else if (target == null)
            {
                target = _targetsHolder.GetTarget();

            }
            else if (target.enabled == false || target.gameObject.layer==Layers.DeadEnemy)
            {
                target = _targetsHolder.GetTarget();
            }
            else
            {
                projectileRigidBody.MovePosition(_thisTransform.position +
                                                 (target.transform.position - _thisTransform.position).normalized *
                                                 speed *
                                                 Time.fixedDeltaTime);
            }
            
        }
    }

    public void Move()
    {
        //projectileRigidBody.AddForce(_thisTransform.forward * speed, ForceMode.Acceleration);
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
        if (jumpsCount > 0)
        {
            if (other.gameObject.layer == Layers.EnemyZombie || other.gameObject.layer == Layers.EnemySkeleton ||
                other.gameObject.layer == Layers.EnemyGhost)
            {
                JumpsCount = jumpsCount - 1;
                _targetsHolder.RemoveTarget(other);

                target = _targetsHolder.GetTarget();
            }
        }

        if (jumpsCount <= 0)
        {
            poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
        }
    }

    #region Pool Item Component Events Reactions

    private void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        projectileRigidBody.velocity = Vector3.zero;
        _thisTransform.rotation = Quaternion.identity;
        isShot = false;
    }

    private void PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction()
    {
        if (_targetsHolder == null || _targetsHolder.GetTarget() == null
        )
        {
            poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
        }

        else
        {
            isShot = true;
            target = _targetsHolder.GetTarget();

        }
        

        // Move();
    }

    #endregion Pool Item Component Events Reactions
}