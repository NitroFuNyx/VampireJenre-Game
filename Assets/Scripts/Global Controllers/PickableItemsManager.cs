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

    private PoolItemsManager _poolItemsManager;

    #region Zenject
    [Inject]
    private void Construct(PoolItemsManager poolItemsManager)
    {
        _poolItemsManager = poolItemsManager;
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
