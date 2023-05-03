
using System;
using System.Collections;
using UnityEngine;

public class MeteorSkillProjectile : SkillParameterBase , ISkillProjectile
{
    [SerializeField] private Rigidbody projectileRigidBody;
    [Header("Particles")] 
    [SerializeField] private ParticleSystem projectileVFX;
    [SerializeField] private ParticleSystem explosionVFX;

    

    public void Move()
    {
        projectileRigidBody.AddForce(transform.forward * speed,ForceMode.Impulse);
    }
   
    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == Layers.MapFloorLayer)
        {
            projectileVFX.gameObject.SetActive(false);


            explosionVFX.transform.SetParent(_dynamicEnvironment);

            explosionVFX.gameObject.SetActive(true);

            explosionVFX.transform.rotation = Quaternion.identity;
            Invoke(nameof(ReturningToPool),2.5f);

            Debug.Log("return");


        }
    }

    private void ReturningToPool()
    {
        poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
        Debug.Log("TIME");

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
   
    public void SetDefaultState()
    {
        projectileVFX.gameObject.SetActive(true);
        explosionVFX.gameObject.SetActive(false);
        explosionVFX.transform.SetParent(transform);
    }
    #region Pool Item Component Events Reactions
    private void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        SetDefaultState();
    }

    private void PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction()
    {
        projectileRigidBody.velocity = Vector3.zero;

        //Move();
    }
    #endregion Pool Item Component Events Reactions

    protected override void CollideWithMapObstacle()
    {
    }
}
