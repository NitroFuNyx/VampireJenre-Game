using System.Collections;
using UnityEngine;

public class SpawnEnemiesManager : MonoBehaviour
{
    [Header("Spawners")]
    [Space]
    [SerializeField] private EnemySpawner spawner_BeyondMap;
    [SerializeField] private EnemySpawner spawner_OnMap;
    [SerializeField] private EnemySpawner spawner_AtGates;

    private bool canSpawnEnemies = true;
    public void SpawnEnemies()
    {
        canSpawnEnemies = true;
        StartCoroutine(SpawnEnemiesWavesCoroutine());
    }

    [ContextMenu("Spawn Beyond Map")]
    public void SpawnEnemiesBeyondMap()
    {
        spawner_BeyondMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, 40);
    }

    [ContextMenu("Spawn On Map")]
    public void SpawnEnemiesOnMap()
    {
        spawner_OnMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, 1);
    }

    [ContextMenu("Spawn At Gates")]
    public void SpawnEnemiesAtGates()
    {
        spawner_AtGates.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, 100);
    }

    public void SpawnEnemiesWave()
    {
        spawner_BeyondMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, 16);
        spawner_OnMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, 16);
        spawner_AtGates.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, 16);
    }

    public void StopEnemySpawn()
    {
        canSpawnEnemies = false;

        StopAllCoroutines();

        StartCoroutine(StopEnemySpawnCoroutine());
    }

    private IEnumerator SpawnEnemiesWavesCoroutine()
    {
        while(canSpawnEnemies)
        {
            SpawnEnemiesWave();
            yield return new WaitForSeconds(8f);
        }
    }

    private IEnumerator StopEnemySpawnCoroutine()
    {
        yield return null;
        spawner_BeyondMap.ReturnAllEnemiesToPool();
        spawner_OnMap.ReturnAllEnemiesToPool();
        spawner_AtGates.ReturnAllEnemiesToPool();
    }
}
