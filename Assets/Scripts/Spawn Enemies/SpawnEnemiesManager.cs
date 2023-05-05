using System.Collections;
using UnityEngine;

public class SpawnEnemiesManager : MonoBehaviour
{
    [Header("Spawners")]
    [Space]
    [SerializeField] private EnemySpawner spawner_BeyondMap;
    [SerializeField] private EnemySpawner spawner_OnMap;
    [SerializeField] private EnemySpawner spawner_AtGates;

    public void SpawnEnemies()
    {
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
        spawner_BeyondMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, 30);
        spawner_OnMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, 30);
        spawner_AtGates.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, 30);
    }

    private IEnumerator SpawnEnemiesWavesCoroutine()
    {
        while(true)
        {
            SpawnEnemiesWave();
            yield return new WaitForSeconds(8f);
        }
    }    
}
