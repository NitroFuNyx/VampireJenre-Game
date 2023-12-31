using System.Collections;
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
    [SerializeField] private float spawnItemsDelay = 20f;
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

    private bool canSpawnItems = false;

    #region Events Declaration
    public event System.Action OnSkillScrollCollected;
    public event System.Action<TreasureChestItems, TreasureChestItems> OnTreasureChestCollected;
    #endregion Events Declaration

    private void Start()
    {
        _gameProcessManager.OnGameStarted += GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost += GameProcessManager_OnPlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon += ResetItems;
        _gameProcessManager.OnLevelDataReset += ResetItems;
    }

    private void OnDestroy()
    {
        _gameProcessManager.OnGameStarted -= GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost -= GameProcessManager_OnPlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon -= ResetItems;
        _gameProcessManager.OnLevelDataReset -= ResetItems;
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
        if(GetAvailablePositionsListForItemToSpawn().Count > 0)
        {
            for (int i = 0; i < itemsSpawnAmount; i++)
            {
                List<PlayerVisionBorderDetector> spawnPosList = GetAvailablePositionsListForItemToSpawn();

                if (spawnPosList.Count > 0)
                {
                    int spawnPosIndex = Random.Range(0, spawnPosList.Count);
                    PlayerVisionBorderDetector spawnPos = spawnPosList[spawnPosIndex];

                    PoolItem item = _poolItemsManager.SpawnItemFromPool(GetPoolItemTypeToSpawn(), spawnPos.transform.position, Quaternion.identity, spawnPos.transform);
                    takenSpawnPositionsList.Add(spawnPos);
                    availableSpawnPositionsList.Remove(spawnPos);
                    allPickableItemsOnMap.Add(item);
                }
            }
        }
    }

    public void CollectSkillScroll()
    {
        OnSkillScrollCollected?.Invoke();
    }

    public void CollectTreasureChest()
    {
        TreasureChestItems firstTreasure = GetRandomTreasureChestItem();
        TreasureChestItems secondTreasure = GetRandomTreasureChestItem();
        
        OnTreasureChestCollected?.Invoke(firstTreasure, secondTreasure);
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
                if (gem != null && gem.TryGetComponent(out PickableResource resource))
                {
                    resource.SetResourceData(resourceData);
                    resourcesOnMapList.Add(resource);
                }
            }
            else
            {
                PoolItem coin = _poolItemsManager.SpawnItemFromPool(PoolItemsTypes.Coin, spawnPos, Quaternion.identity, coinsHolder);
                allPickableItemsOnMap.Add(coin);
                if (coin != null && coin.TryGetComponent(out PickableResource resource))
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
        canSpawnItems = false;
        StopAllCoroutines();

        for(int i = 0; i < allPickableItemsOnMap.Count; i++)
        {
            if(allPickableItemsOnMap[i] != null && allPickableItemsOnMap[i].gameObject.activeInHierarchy)
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
        //SpawnItems();
        canSpawnItems = true;
        StartCoroutine(SpawnItemsCoroutine());
    }

    private void GameProcessManager_OnPlayerLost_ExecuteReaction(GameModes _)
    {
        ResetItems();
    }

    private TreasureChestItems GetRandomTreasureChestItem()
    {
        int itemIndex = Random.Range(0, System.Enum.GetValues(typeof(TreasureChestItems)).Length);
        TreasureChestItems item = (TreasureChestItems)itemIndex;

        if(_gameProcessManager.CurrentGameMode == GameModes.Deathmatch)
        {
            if(item == TreasureChestItems.Gems)
            {
                item = TreasureChestItems.Coins;
            }
        }

        return item;
    }

    private IEnumerator SpawnItemsCoroutine()
    {
        while(canSpawnItems)
        {
            SpawnItems();
            yield return new WaitForSeconds(spawnItemsDelay);
        }
    }
}
