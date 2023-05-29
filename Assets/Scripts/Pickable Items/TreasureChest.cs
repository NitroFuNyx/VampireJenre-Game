using UnityEngine;

public class TreasureChest : PickableItem
{
    private void Start()
    {

    }

    protected override void PlayerCollision_ExecuteReaction()
    {
        poolItemComponent.ResourcesManager.AddResourceForPickingUpTreasureChest();
    }

    protected override void ResetItem()
    {
        itemPickedUp = false;
    }
}
