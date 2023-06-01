using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PickableItemsManager : MonoBehaviour
{
    [Header("Spawn Positions")]
    [Space]
    [SerializeField] private List<PlayerVisionBorderDetector> availableSpawnPositionsList = new List<PlayerVisionBorderDetector>();
    [SerializeField] private List<PlayerVisionBorderDetector> takenSpawnPositionsList = new List<PlayerVisionBorderDetector>();
    [Header("Spawn Items Main Data")]
    [Space] 
    [SerializeField] private int itemsSpawnAmount = 3;
    [Header("Holders")]
    [Space]
    [SerializeField] private Transform gemsHolder;
    [SerializeField] private Transform coinsHolder;
    [Header("Resources On Map")]
    [Space]
    [SerializeField] private List<PoolItem> allPickableItemsOnMap = new List<PoolItem>();
    [SerializeField] private List<PickableResource> resourcesOnMapList = new List<PickableResource>();

    private PoolItemsManager _poolItemsManager;
    private ResourcesManager _resourcesManager;
    private PlayerCollisionsManager _player;
    private GameProcessManager _gameProcessManager;

    #region Events Declaration
    public event System.Action OnSkillScrollCollected;
    #endregion Events Declaration

    private void Start()
    {
        _gameProcessManager.OnGameStarted += GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost += ResetItems;
        _gameProcessManager.OnPlayerWon += ResetItems;
    }

    private void OnDestroy()
    {
        _gameProcessManager.OnGameStarted -= GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost -= ResetItems;
        _gameProcessManager.OnPlayerWon -= ResetItems;
    }

    #region Zenject
    [Inject]
    private void Construct(PoolItemsManager poolItemsManager, ResourcesManager resourcesManager, PlayerCollisionsManager playerCollisionsManager,
                           GameProcessManager gameProcessManager)
    {
        _poolItemsManager = poolItemsManager;
        _resourcesManager = resourcesManager;
        _player = playerCollisionsManager;
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    [ContextMenu("Spawn Items")]
    public void SpawnItems()
    {
        for(int i = 0; i < itemsSpawnAmount; i++)
        {
            List<PlayerVisionBorderDetector> spawnPosList = GetAvailablePositionsListForItemToSpawn();

            if (spawnPosList.Count > 0)
            {
                PlayerVisionBorderDetector spawnPos = spawnPosList[0];

                PoolItem item = _poolItemsManager.SpawnItemFromPool(GetPoolItemTypeToSpawn(), spawnPos.transform.position, Quaternion.identity, spawnPos.transform);
                takenSpawnPositionsList.Add(spawnPos);
                availableSpawnPositionsList.Remove(spawnPos);
                allPickableItemsOnMap.Add(item);
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
                allPickableItemsOnMap.Add(gem);
                if (gem.TryGetComponent(out PickableResource resource))
                {
                    resource.SetResourceData(resourceData);
                    resourcesOnMapList.Add(resource);
                }
            }
            else
            {
                PoolItem coin = _poolItemsManager.SpawnItemFromPool(PoolItemsTypes.Coin, spawnPos, Quaternion.identity, coinsHolder);
                allPickableItemsOnMap.Add(coin);
                if (coin.TryGetComponent(out PickableResource resource))
                {
                    resource.SetResourceData(resourceData);
                    resourcesOnMapList.Add(resource);
                }
            }
        }
    }

    public void CollectAllPickableResources()
    {
        for(int i = 0; i < resourcesOnMapList.Count; i++)
        {
            resourcesOnMapList[i].MoveToPlayer(_player.transform);
        }

        resourcesOnMapList.Clear();
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

    private List<PlayerVisionBorderDetector> GetAvailablePositionsListForItemToSpawn()
    {
        List<PlayerVisionBorderDetector> spawnPosList = new List<PlayerVisionBorderDetector>();

        if (availableSpawnPositionsList.Count > 0)
        {
            for (int i = 0; i < availableSpawnPositionsList.Count; i++)
            {
                if(!availableSpawnPositionsList[i].CheckIfVisibleForPlayer())
                {
                    spawnPosList.Add(availableSpawnPositionsList[i]);
                }
            }
        }

        return spawnPosList;
    }

    private void ResetItems()
    {
        for(int i = 0; i < allPickableItemsOnMap.Count; i++)
        {
            allPickableItemsOnMap[i].PoolItemsManager.ReturnItemToPool(allPickableItemsOnMap[i]);
        }

        allPickableItemsOnMap.Clear();

        for(int i = 0; i < takenSpawnPositionsList.Count; i++)
        {
            availableSpawnPositionsList.Add(takenSpawnPositionsList[i]);
        }

        takenSpawnPositionsList.Clear();
    }

    private void GameProcessManager_GameStarted_ExecuteReaction()
    {
        SpawnItems();
    }
}
