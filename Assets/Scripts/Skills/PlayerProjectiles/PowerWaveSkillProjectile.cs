using System;
using UnityEngine;

public class PowerWaveSkillProjectile : SkillParameterBase, ISkillProjectile
{
    [SerializeField] private Rigidbody projectileRigidBody;
    
    [Header("Radius")] [SerializeField] private float radius;
    private Collider[] _enemyTargets;
    private int _enemyCounter = 0;
    [Space]
    [Header("Particles")] 
    [SerializeField] private ParticleSystem slashPath;
    [SerializeField] private ParticleSystem slashWind;
    private TargetsHolder _targetsHolder;

    [Space] 
    [Header("Parameters")] 
    [Range(0, 5)]//If higher change duration of each particle
    [SerializeField]private float duration;
    private float timeCounter;

    private const float TimeGap = 0.1f;
    
    private Transform _thisTransform;
    private Collider target;

    public event Action<PoolItem> OnItemReturnToPool;

    
    private void Start()
    {
        SubscribeOnEvents();
        _thisTransform = transform;
        ChangeParticleLifeTime();
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;
        if(duration+TimeGap<timeCounter)
        {
            poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
            OnItemReturnToPool?.Invoke(poolItemComponent);
        }
        
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
    public TargetsHolder TargetHolder
    {
        set => _targetsHolder = value;
    }
    public void ChangeParticleLifeTime()
    {
        var path = slashPath.main;
        path.startLifetime = duration;
        var wind = slashWind.main;
        wind.startLifetime = duration;
        
        
    }
    

    public void Move()
    {
        if(target!=null)
            _thisTransform.LookAt(target.transform.position,Vector3.up);
        projectileRigidBody.AddForce(_thisTransform.forward * speed,ForceMode.Acceleration);
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


   


    #region Pool Item Component Events Reactions
    private void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
       projectileRigidBody.velocity = Vector3.zero;
       transform.rotation = Quaternion.identity;
       timeCounter = 0;
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
