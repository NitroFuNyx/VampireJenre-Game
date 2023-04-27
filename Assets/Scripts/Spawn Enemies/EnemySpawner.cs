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
        PoolItem poolItem = _poolItemsManager.SpawnItemFromPool(enemyType, spawnPoint.position, spawnPoint.rotation, spawnedObjectsHolder);

        if(poolItem != null)
        {
            poolItem.SetObjectAwakeState();
        }
        else
        {
            Debug.LogWarning($"There is no {enemyType} models left in the pool to spawn at {gameObject}", gameObject);
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        Transform spawnPoint = spawnPositionsList[0];

        int randomPointIndex = Random.Range(0, spawnPositionsList.Count);

        spawnPoint = spawnPositionsList[randomPointIndex];

        takenSpawnPositionsList.Add(spawnPoint);
        spawnPositionsList.Remove(spawnPoint);

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
