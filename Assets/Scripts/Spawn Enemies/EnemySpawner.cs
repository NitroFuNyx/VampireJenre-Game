using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [Header("Holders")]
    [Space]
    [SerializeField] private Transform spawnedObjectsHolder;
    [Header("Spawn Positions")]
    [Space]
    [SerializeField] private List<Transform> spawnPositionsList = new List<Transform>();

    private PoolItemsManager _poolItemsManager;

    [SerializeField] private List<Transform> takenSpawnPositionsList = new List<Transform>();

    private float resetSpawnPositionsDelay = 1f;

    #region Zenject
    [Inject]
    private void Construct(PoolItemsManager poolItemsManager)
    {
        _poolItemsManager = poolItemsManager;
    }
    #endregion Zenject

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, 1);
    }

    public void SpawnEnemyWave(PoolItemsTypes enemyType, int enemiesAmount)
    {
        if(enemiesAmount > spawnPositionsList.Count)
        {
            enemiesAmount = spawnPositionsList.Count;
        }

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
        poolItem.SetObjectAwakeState();
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
