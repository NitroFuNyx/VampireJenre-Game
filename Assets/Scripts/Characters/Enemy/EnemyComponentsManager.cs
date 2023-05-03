using UnityEngine;

public class EnemyComponentsManager : EnemyBehaviour
{
    [Header("Components")]
    [Space]
    [SerializeField] private EnemyAnimationsManager animationsManager;

    private EnemyMovementManager movementManager;
    private EnemyCollisionsManager collisionsManager;

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    public override void CashHeirComponents()
    {
        if (TryGetComponent(out EnemyMovementManager enemyMovementManager))
        {
            movementManager = enemyMovementManager;
        }
        if (TryGetComponent(out EnemyCollisionsManager enemyCollisionsManager))
        {
            collisionsManager = enemyCollisionsManager;
        }
    }

    private void SubscribeOnEvents()
    {
        if(poolItemComponent != null)
        {
            poolItemComponent.OnItemResetRequired += PoolItemComponent_ResetRequired_ExecuteReaction;
            poolItemComponent.OnObjectAwakeStateSet += PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction;
        }

        collisionsManager.OnPlayerOutOfHp += CollisionManager_PlayerOutOfHp_ExecuteReaction;
        collisionsManager.OnDamageReceived += CollisionManager_DamageReceived_ExecuteReaction;

        animationsManager.OnDieAnimationFinished += AnimationManager_DieAnimationFinished_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        if (poolItemComponent != null)
        {
            poolItemComponent.OnItemResetRequired -= PoolItemComponent_ResetRequired_ExecuteReaction;
            poolItemComponent.OnObjectAwakeStateSet -= PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction;
        }

        collisionsManager.OnPlayerOutOfHp -= CollisionManager_PlayerOutOfHp_ExecuteReaction;
        collisionsManager.OnDamageReceived -= CollisionManager_DamageReceived_ExecuteReaction;

        animationsManager.OnDieAnimationFinished -= AnimationManager_DieAnimationFinished_ExecuteReaction;
    }

    #region Pool Item Component Events Reactions
    private void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        // reset enemy
        animationsManager.SetAnimation_Idle();
        collisionsManager.ResetComponent();
    }

    private void PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction()
    {
        collisionsManager.ChangeColliderActivationState(true);
        movementManager.MoveToPlayer();
        animationsManager.SetAnimation_Moving();
    }
    #endregion Pool Item Component Events Reactions

    #region Collision Manager Events Reaction
    private void CollisionManager_PlayerOutOfHp_ExecuteReaction()
    {
        Debug.Log($"Hit");
        movementManager.StopMoving();
        animationsManager.SetAnimation_Die();
    }

    private void CollisionManager_DamageReceived_ExecuteReaction()
    {

    }
    #endregion Collision Manager Events Reaction

    #region Animation Manager Events Reaction
    private void AnimationManager_DieAnimationFinished_ExecuteReaction()
    {
        poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
    }
    #endregion Animation Manager Events Reaction
}
