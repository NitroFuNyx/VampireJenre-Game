using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PoolItemsManager : MonoBehaviour
{
    [Header("Pool Data")]
    [Space]
    [SerializeField] private int enemiesPoolSize = 50;
    [SerializeField] private int skillMissilePoolSize = 75;
    [SerializeField] private int skillChainLightningPoolSize = 75;
    [SerializeField] private int skillMeteorPoolSize = 25;
    [SerializeField] private int skillLightningBoltrPoolSize = 25;
    [SerializeField] private int skillFireballPoolSize = 25;
    [SerializeField] private int skillSingleShotPoolSize = 50;
    [SerializeField] private int skillPowerWavePoolSize = 50;

    [Header("Active Pools")]
    [Space]
    [SerializeField] private List<List<PoolItem>> activePoolsList = new List<List<PoolItem>>();
    [Header("Prefabs")]
    [Space]
    [SerializeField] private PoolItem enemySkeletonPrefab;
    [SerializeField] private PoolItem enemyGhostPrefab;
    [SerializeField] private PoolItem enemyZombiePrefab;
    [SerializeField] private PoolItem skillMissilePrefab;
    [SerializeField] private PoolItem skillMeteorPrefab;
    [SerializeField] private PoolItem skillLightningBoltPrefab;
    [SerializeField] private PoolItem skillFireballPrefab;
    [SerializeField] private PoolItem skillChainLightningPrefab;
    [SerializeField] private PoolItem skillSingleShotPrefab;
    [SerializeField] private PoolItem skillPowerWaveShotPrefab;

    private Dictionary<PoolItemsTypes, List<PoolItem>> itemsListsDictionary = new Dictionary<PoolItemsTypes, List<PoolItem>>();
    private Dictionary<PoolItemsTypes, Transform> itemsHoldersDictionary = new Dictionary<PoolItemsTypes, Transform>();

    private Vector3 poolItemsSpawnPos = new Vector3(100f, 100f, 100f);

    private void Start()
    {
        CreatePool(enemySkeletonPrefab, "Enemy Skeleton", enemiesPoolSize);
        CreatePool(enemyGhostPrefab, "Enemy Ghost", enemiesPoolSize);
        CreatePool(enemyZombiePrefab, "Enemy Zombie", enemiesPoolSize);
        CreatePool(skillSingleShotPrefab , "Skill Single Shot", skillSingleShotPoolSize);
        CreatePool(skillMissilePrefab, "Skill Missile ", skillMissilePoolSize);
        CreatePool(skillMeteorPrefab , "Skill Meteor ", skillMeteorPoolSize);
        CreatePool(skillLightningBoltPrefab , "Skill Lightning Bolt ", skillLightningBoltrPoolSize);
        CreatePool(skillFireballPrefab , "Skill Fireball Bolt ", skillFireballPoolSize);
        CreatePool(skillChainLightningPrefab , "Skill Chain lightning ", skillChainLightningPoolSize);
        CreatePool(skillPowerWaveShotPrefab , "Skill Power Wave ", skillPowerWavePoolSize);
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
        _poolItem.ResetPoolItem();
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
            poolItem.CashComponents(this);
            //poolItem.gameObject.SetActive(false);
            itemsList.Add(poolItem);
            poolItem.name = $"{itemName} {i}";
        }

        StartCoroutine(TurnNewPoolItemsOffCoroutine(itemsList));
    }

    private IEnumerator TurnNewPoolItemsOffCoroutine(List<PoolItem> itemsList)
    {
        yield return null;

        for(int i = 0; i < itemsList.Count; i++)
        {
            itemsList[i].gameObject.SetActive(false);
        }
    }
}