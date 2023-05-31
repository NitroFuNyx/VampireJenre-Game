using System.Collections;
using UnityEngine;
using DG.Tweening;

public class LightningBoltSkill : SkillParameterBase , ISkillProjectile
{
    [Header("Particles")] 
    [SerializeField] private ParticleSystem projectileVFX;
    [Header("Parameters")]
    [Range(0.1f, 10)] 
    [SerializeField] private float explosionColliderRadius;
    [Range(0.1f, 10)] 
    [SerializeField] private float explosionLifeTime;
    [Range(0.1f, 5)] 
    [SerializeField] private float timeToExplode;
    [Header("References")] 
    [SerializeField] private ExplosionCollision explosionCollision;

    [SerializeField] private Collider sphereCollider;
   
    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
    private IEnumerator DisablingSlash()
    {
        sphereCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        sphereCollider.enabled = false;


    }
    private void Explode()
    {
        StartCoroutine(DisablingSlash());
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
   
    private void SetDefaultState()
    {
        poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);


    }
    #region Pool Item Component Events Reactions
    private void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        projectileVFX.gameObject.SetActive(true);

    }

    private void PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction()
    {
        Invoke(nameof(Explode),timeToExplode);

        Invoke(nameof(SetDefaultState),explosionLifeTime);
        //Move();
    }
    #endregion Pool Item Component Events Reactions

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,explosionColliderRadius);
    }
    
    

    public void Move()
    {
        
    }
}
