using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesManager : MonoBehaviour
{
    [Header("Spawners")]
    [Space]
    [SerializeField] private EnemySpawner spawner_BeyondMap;
    [SerializeField] private EnemySpawner spawner_OnMap;
    [SerializeField] private EnemySpawner spawner_AtGates;

    [ContextMenu("Spawn Beyond Map")]
    public void SpawnEnemiesBeyondMap()
    {
        spawner_BeyondMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, 2);
    }

    [ContextMenu("Spawn On Map")]
    public void SpawnEnemiesOnMap()
    {
        spawner_OnMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, 100);
    }

    [ContextMenu("Spawn At Gates")]
    public void SpawnEnemiesAtGates()
    {
        spawner_AtGates.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, 100);
    }
}
