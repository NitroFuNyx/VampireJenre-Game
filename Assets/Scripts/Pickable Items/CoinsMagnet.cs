
public class CoinsMagnet : PickableItem
{
    protected override void PlayerCollision_ExecuteReaction()
    {
        poolItemComponent.AudioManager.PlaySFXSound_PickUpItem();
        poolItemComponent.PickableItemsManager.CollectAllPickableResources();
        poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
    }

    protected override void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        itemPickedUp = false;
    }
}
