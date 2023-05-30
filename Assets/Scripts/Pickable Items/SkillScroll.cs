
public class SkillScroll : PickableItem
{
    protected override void PlayerCollision_ExecuteReaction()
    {
        //poolItemComponent.ResourcesManager.AddResourceForPickingUpTreasureChest();
        poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
    }

    protected override void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        itemPickedUp = false;
    }
}
