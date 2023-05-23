using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerWaveSkillProjectile : SkillParameterBase, ISkillProjectile
{
    [SerializeField] private Rigidbody projectileRigidBody;
    [SerializeField] private LayerMask targetLayers;
    
    [Header("Radius")] [SerializeField] private float radius;
    private Collider[] _enemyTargets;
    private int _enemyCounter = 0;
    [Space]
    [Header("Particles")] 
    [SerializeField] private ParticleSystem slashPath;
    [SerializeField] private ParticleSystem slashWind;

    [Space] 
    [Header("Parameters")] 
    [Range(0, 5)]//If higher change duration of each particle
    [SerializeField]private float duration;
    private float timeCounter;

    private const float TimeGap = 0.1f;
    
    private Transform _thisTransform;
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
            poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
        
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    public void ChangeParticleLifeTime()
    {
        var path = slashPath.main;
        path.startLifetime = duration;
        var wind = slashWind.main;
        wind.startLifetime = duration;
        
        
    }
    public void ChangeSlashScale(Vector3 scale)
    {
        transform.localScale = scale;
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


   


    #region Pool Item Component Events Reactions
    private void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
       projectileRigidBody.velocity = Vector3.zero;
       transform.rotation = Quaternion.identity;
       timeCounter = 0;
    }

    private void PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction()
    {
        GetTargets();
        LockTarget();
        Move();
    }
    #endregion Pool Item Component Events Reactions
   
}
