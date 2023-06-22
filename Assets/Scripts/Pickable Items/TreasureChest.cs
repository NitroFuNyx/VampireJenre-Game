
public class TreasureChest : PickableItem
{
    protected override void PlayerCollision_ExecuteReaction()
    {
        //poolItemComponent.ResourcesManager.AddResourceForPickingUpTreasureChest();
        poolItemComponent.AudioManager.PlaySFXSound_PickUpItem();
        poolItemComponent.PickableItemsManager.CollectTreasureChest();
        poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
    }

    protected override void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        itemPickedUp = false;
    }
}
