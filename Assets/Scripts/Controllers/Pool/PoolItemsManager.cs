using System.Collections.Generic;
using UnityEngine;

public class PoolItemsManager : MonoBehaviour
{
    [Header("Pool Data")]
    [Space]
    [SerializeField] private int enemiesPoolSize = 50;
    [Header("Holders")]
    [Space]
    [SerializeField] private Transform spawnedItemsHolder;
    [Header("Active Pools")]
    [Space]
    [SerializeField] private List<List<PoolItem>> activePoolsList = new List<List<PoolItem>>();
    [Header("Prefabs")]
    [Space]
    [SerializeField] private PoolItem enemyPrefab;

    private Dictionary<PoolItemsTypes, List<PoolItem>> itemsListsDictionary = new Dictionary<PoolItemsTypes, List<PoolItem>>();
    private Dictionary<PoolItemsTypes, Transform> itemsHoldersDictionary = new Dictionary<PoolItemsTypes, Transform>();

    private Vector3 poolItemsSpawnPos = new Vector3(0f, 100f, 0f);

    private void Start()
    {
        CreatePool(enemyPrefab, "Enemy", enemiesPoolSize);
    }

    public PoolItem SpawnItemFromPool(PoolItemsTypes poolItemType, Vector3 _spawnPos, Quaternion _rotation, Transform _parent)
    {
        PoolItem poolItem = null;

        if (itemsListsDictionary.ContainsKey(poolItemType))
        {
            List<PoolItem> poolItemsList = itemsListsDictionary[poolItemType];

            for (int i = 0; i < poolItemsList.Count; i++)
            {
                if (!poolItemsList[i].gameObject.activeInHierarchy)
                {
                    poolItem = poolItemsList[i];
                    break;
                }
            }

            if (poolItem != null)
            {
                poolItem.transform.SetParent(_parent);
                poolItem.transform.position = _spawnPos;
                poolItem.transform.rotation = _rotation;
                poolItem.gameObject.SetActive(true);
                poolItemsList.Remove(poolItem);
            }
        }
        else
        {
            Debug.LogError($"No pool for such prefab: {poolItemType} in dictionary");
        }
        

        return poolItem;
    }

    public void ReturnItemToPool(PoolItem _poolItem)
    {
        List<PoolItem> poolItemsList = itemsListsDictionary[_poolItem.PoolItemType];

        _poolItem.gameObject.SetActive(false);
        _poolItem.transform.SetParent(itemsHoldersDictionary[_poolItem.PoolItemType]);
        _poolItem.transform.localPosition = Vector3.zero;
        poolItemsList.Add(_poolItem);
    }

    private void CreatePool(PoolItem poolItemPrefab, string itemName, int poolSize)
    {
        GameObject poolItemsParent = new GameObject();
        poolItemsParent.transform.SetParent(transform);
        poolItemsParent.name = $"{itemName} Items Parent";
        poolItemsParent.transform.position = poolItemsSpawnPos;

        List<PoolItem> itemsList = new List<PoolItem>();

        activePoolsList.Add(itemsList);

        itemsListsDictionary.Add(poolItemPrefab.PoolItemType, itemsList);
        itemsHoldersDictionary.Add(poolItemPrefab.PoolItemType, poolItemsParent.transform);

        for (int i = 0; i < poolSize; i++)
        {
            PoolItem poolItem = Instantiate(poolItemPrefab, Vector3.zero, Quaternion.identity, poolItemsParent.transform);
            poolItem.transform.localPosition = Vector3.zero;
            poolItem.gameObject.SetActive(false);
            itemsList.Add(poolItem);
            poolItem.name = $"{itemName} {i}";
        }
    }
}