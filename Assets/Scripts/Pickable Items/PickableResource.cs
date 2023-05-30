using UnityEngine;

public class PickableResource : PickableItem
{
    [Header("Resource Data")]
    [Space]
    [SerializeField] private ResourceBonusItemStruct resourceBonusData;

    public void SetResourceData(ResourceBonusItemStruct resourceData)
    {
        resourceBonusData = resourceData;
    }

    protected override void PlayerCollision_ExecuteReaction()
    {
        poolItemComponent.ResourcesManager.AddResourceForKillingEnemy(resourceBonusData);
        poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
    }

    protected override void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        itemPickedUp = false;
    }
}
