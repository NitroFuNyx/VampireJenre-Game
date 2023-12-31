using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [Header("Holders")]
    [Space]
    [SerializeField] private Transform spawnedObjectsHolder;
    [SerializeField] private List<Transform> spawnPositionsHoldersList = new List<Transform>();
    [Header("Spawn Positions")]
    [Space]
    [SerializeField] private List<Transform> spawnPositionsList = new List<Transform>();
    [Header("VFX")]
    [Space]
    [SerializeField] private List<ParticleSystem> vfxSpawnList = new List<ParticleSystem>();

    private PoolItemsManager _poolItemsManager;
    private PoolItem spawnedBoss;

    private List<Transform> takenSpawnPositionsList = new List<Transform>();

    private float resetSpawnPositionsDelay = 1f;

    private void Awake()
    {
        FillSpawnPositionsList();
    }

    #region Zenject
    [Inject]
    private void Construct(PoolItemsManager poolItemsManager)
    {
        _poolItemsManager = poolItemsManager;
    }
    #endregion Zenject

    public void SpawnEnemyWave(PoolItemsTypes enemyType, int enemiesAmount)
    {
        if(enemiesAmount > spawnPositionsList.Count)
        {
            enemiesAmount = spawnPositionsList.Count;
        }

        ActivateSpawnVFX();

        for(int i = 0; i < enemiesAmount; i++)
        {
            SpawnEnemy(enemyType);
        }

        StartCoroutine(ResetSpawnPositionsCoroutine());
    }

    public void SpawnEnemy(PoolItemsTypes enemyType)
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        if(spawnPoint != null)
        {
            PoolItem poolItem = _poolItemsManager.SpawnItemFromPool(enemyType, spawnPoint.position, spawnPoint.rotation, spawnedObjectsHolder);
            
            if (poolItem != null)
            {
                poolItem.SetObjectAwakeState();
            }
            else
            {
                Debug.LogWarning($"There is no {enemyType} models left in the pool to spawn at {gameObject}", gameObject);
            }
        }
    }
    public void SpawnBoss(PoolItemsTypes enemyType)
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        if(spawnPoint != null)
        {
            PoolItem poolItem = _poolItemsManager.SpawnItemFromPool(enemyType, Vector3.zero, spawnPoint.rotation, spawnedObjectsHolder);
            spawnedBoss = poolItem;
            if (poolItem != null)
            {
                if (poolItem.TryGetComponent(out EnemySkillsManager skillsManager))
                {
                    skillsManager.DynamicEnvironment = poolItem.DynamicEnvironment;
                }
                poolItem.SetObjectAwakeState();
            }
            else
            {
                Debug.LogWarning($"There is no {enemyType} models left in the pool to spawn at {gameObject}", gameObject);
            }
        }
    }

    public void ReturnAllEnemiesToPool()
    {
        List<PoolItem> poolItemsList = new List<PoolItem>();
        
        for(int i = 0; i < spawnedObjectsHolder.childCount; i++)
        {
            if(spawnedObjectsHolder.GetChild(i).TryGetComponent(out PoolItem poolItem))
            {
                poolItemsList.Add(poolItem);
            }
        }

        for(int i = 0; i < poolItemsList.Count; i++)
        {
            poolItemsList[i].PoolItemsManager.ReturnItemToPool(poolItemsList[i]);
        }

        if(spawnedBoss != null)
        {
            spawnedBoss.PoolItemsManager.ReturnItemToPool(spawnedBoss);
            spawnedBoss = null;
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        //Transform spawnPoint = spawnPositionsList[0];
        Transform spawnPoint = null;
        
        if(spawnPositionsList.Count > 0)
        {
            int randomPointIndex = Random.Range(0, spawnPositionsList.Count);

            spawnPoint = spawnPositionsList[randomPointIndex];

            takenSpawnPositionsList.Add(spawnPoint);
            spawnPositionsList.Remove(spawnPoint);
        }

        return spawnPoint;
    }

    private void ActivateSpawnVFX()
    {
        if(vfxSpawnList.Count > 0)
        {
            for (int i = 0; i < vfxSpawnList.Count; i++)
            {
                vfxSpawnList[i].Play();
            }
        }
    }

    private void FillSpawnPositionsList()
    {
        for(int i = 0; i < spawnPositionsHoldersList.Count; i++)
        {
            for(int j = 0; j < spawnPositionsHoldersList[i].childCount; j++)
            {
                spawnPositionsList.Add(spawnPositionsHoldersList[i].GetChild(j));
            }
        }
    }

    private IEnumerator ResetSpawnPositionsCoroutine()
    {
        yield return new WaitForSeconds(resetSpawnPositionsDelay);

        for(int i = 0; i < takenSpawnPositionsList.Count; i++)
        {
            if(!spawnPositionsList.Contains(takenSpawnPositionsList[i]))
            {
                spawnPositionsList.Add(takenSpawnPositionsList[i]);
            }
        }

        takenSpawnPositionsList.Clear();
    }
}
