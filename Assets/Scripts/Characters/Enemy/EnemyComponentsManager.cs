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
        // reset enemy
    }

    private void PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction()
    {
        collisionsManager.ChangeColliderActivationState(true);
        movementManager.MoveToPlayer();
        animationsManager.SetAnimation_Moving();
    }
    #endregion Pool Item Component Events Reactions
}
