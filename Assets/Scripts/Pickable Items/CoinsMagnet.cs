
public class CoinsMagnet : PickableItem
{
    protected override void PlayerCollision_ExecuteReaction()
    {
        poolItemComponent.PickableItemsManager.CollectAllPickableResources();
        poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
    }

    protected override void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        itemPickedUp = false;
    }
}
