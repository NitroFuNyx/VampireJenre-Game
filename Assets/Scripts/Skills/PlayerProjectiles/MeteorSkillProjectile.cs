using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeteorSkillProjectile : SkillParameterBase , ISkillProjectile
{
    [SerializeField] private Rigidbody projectileRigidBody;
    [Header("Particles")] 
    [SerializeField] private ParticleSystem projectileVFX;
    [SerializeField] private ParticleSystem explosionVFX;
    [SerializeField] private GameObject puddle;
    [Header("Parameters")]
    [Range(0.1f, 10)] 
    [SerializeField] private float explosionColliderRadius;
    [Range(0.1f, 10)] 
    [SerializeField] private float puddleLifeTime;

    [Header("References")] 
    [SerializeField] private ExplosionCollision explosionCollision;
    [SerializeField] private Collider sphereCollider;

    public event Action<PoolItem> OnItemReturnToPool;

    public float PuddleLifeTime
    {
        get => puddleLifeTime;
        set => puddleLifeTime = value;
    }

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


            explosionVFX.transform.SetParent(transform.parent);
            puddle.transform.SetParent(transform.parent);
            explosionVFX.gameObject.SetActive(true);
            StartCoroutine(DisablingSlash());
            puddle.transform.DOScale(1, 0.5f);
            explosionVFX.transform.rotation = Quaternion.identity;
            Invoke(nameof(UnScalePuddle),puddleLifeTime);
        
        }
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
   
    private IEnumerator DisablingSlash()
    {
        sphereCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        sphereCollider.enabled = false;


    }
    public  void SetDefaultStateOfPuddle()
    {
        puddle.transform.SetParent(transform);
        puddle.transform.localPosition = Vector3.zero;
        poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
        OnItemReturnToPool?.Invoke(poolItemComponent);

    }
    #region Pool Item Component Events Reactions
    private void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        projectileVFX.gameObject.SetActive(true);
        explosionVFX.transform.SetParent(transform);

        explosionVFX.gameObject.SetActive(false);
        explosionVFX.transform.position = transform.position;

        puddle.transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
        puddle.transform.SetParent(transform);
        puddle.transform.localPosition = Vector3.zero;
    }

    private void PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction()
    {
        projectileRigidBody.velocity = Vector3.zero;

        //Move();
    }
    #endregion Pool Item Component Events Reactions

    private void UnScalePuddle()
    {
        puddle.transform.DOScale(0.11f, 0.5f).OnComplete(() => { SetDefaultStateOfPuddle();});
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(puddle.transform.position,explosionColliderRadius);
    }
    
    
}
