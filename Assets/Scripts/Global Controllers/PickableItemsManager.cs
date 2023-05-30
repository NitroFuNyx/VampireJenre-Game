using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PickableItemsManager : MonoBehaviour
{
    [Header("Spawn Positions")]
    [Space]
    [SerializeField] private List<Transform> availableSpawnPositionsList = new List<Transform>();
    [SerializeField] private List<Transform> takenSpawnPositionsList = new List<Transform>();
    [Header("Spawn Items Main Data")]
    [Space] 
    [SerializeField] private int itemsSpawnAmount = 3;
    [Header("Holders")]
    [Space]
    [SerializeField] private Transform gemsHolder;
    [SerializeField] private Transform coinsHolder;

    private PoolItemsManager _poolItemsManager;
    private ResourcesManager _resourcesManager;

    #region Events Declaration
    public event System.Action OnSkillScrollCollected;
    #endregion Events Declaration

    #region Zenject
    [Inject]
    private void Construct(PoolItemsManager poolItemsManager, ResourcesManager resourcesManager)
    {
        _poolItemsManager = poolItemsManager;
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    [ContextMenu("Spawn Items")]
    public void SpawnItems()
    {
        for(int i = 0; i < itemsSpawnAmount; i++)
        {
            if(availableSpawnPositionsList.Count > 0)
            {
                Transform spawnPos = availableSpawnPositionsList[0];

                PoolItem item = _poolItemsManager.SpawnItemFromPool(GetPoolItemTypeToSpawn(), spawnPos.position, Quaternion.identity, spawnPos);
                takenSpawnPositionsList.Add(spawnPos);
                availableSpawnPositionsList.Remove(spawnPos);
            }
        }
    }

    public void CollectSkillScroll()
    {
        OnSkillScrollCollected?.Invoke();
    }

    public void SpawnResourceForKillingEnemy(Vector3 spawnPos)
    {
        ResourceBonusItemStruct resourceData = new ResourceBonusItemStruct();
        resourceData = _resourcesManager.GetResourceItemForKillingEnemyData();

        if(resourceData.canBeCollected)
        {
            if(resourceData.resourceType == ResourcesTypes.Gems)
            {
                int gemIndex = UnityEngine.Random.Range(0, 3);
                PoolItemsTypes gemType;
                if(gemIndex == 0)
                {
                    gemType = PoolItemsTypes.Gem_Green;
                }
                else if(gemIndex == 1)
                {
                    gemType = PoolItemsTypes.Gem_Orange;
                }
                else
                {
                    gemType = PoolItemsTypes.Gem_Purple;
                }

                PoolItem gem = _poolItemsManager.SpawnItemFromPool(gemType, spawnPos, Quaternion.identity, gemsHolder);
                if(gem.TryGetComponent(out PickableResource resource))
                {
                    resource.SetResourceData(resourceData);
                }
            }
            else
            {
                PoolItem coin = _poolItemsManager.SpawnItemFromPool(PoolItemsTypes.Coin, spawnPos, Quaternion.identity, coinsHolder);
                if (coin.TryGetComponent(out PickableResource resource))
                {
                    resource.SetResourceData(resourceData);
                }
            }
        }
    }

    private PoolItemsTypes GetPoolItemTypeToSpawn()
    {
        PoolItemsTypes itemToSpawn = PoolItemsTypes.TreasureChest;

        int itemCategoryIndex = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(PickableItemsCategories)).Length);

        if (itemCategoryIndex == (int)PickableItemsCategories.TreasureChest)
        {
            itemToSpawn = PoolItemsTypes.TreasureChest;
        }
        else if (itemCategoryIndex == (int)PickableItemsCategories.SkillScroll)
        {
            itemToSpawn = PoolItemsTypes.SkillScroll;
        }
        else if (itemCategoryIndex == (int)PickableItemsCategories.CoinsMagnet)
        {
            itemToSpawn = PoolItemsTypes.CoinsMagnet;
        }

        return itemToSpawn;
    }

    //private Vector3 GetPositionForItemToSpawn()
    //{
    //    Vector3 spawnPos = Vector3.zero;

    //    if(availableSpawnPositionsList.Count > 0)
    //    {
    //        for (int i = 0; i < availableSpawnPositionsList.Count; i++)
    //        {
               
    //        }
    //    }

    //    return spawnPos;
    //}
}
