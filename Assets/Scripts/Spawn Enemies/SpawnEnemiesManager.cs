using System.Collections;
using UnityEngine;
using Zenject;

public class SpawnEnemiesManager : MonoBehaviour
{
    [Header("Spawners")]
    [Space]
    [SerializeField] private EnemySpawner spawner_BeyondMap;
    [SerializeField] private EnemySpawner spawner_OnMap;
    [SerializeField] private EnemySpawner spawner_AtGates;

    private GameProcessManager _gameProcessManager;

    private bool canSpawnEnemies = true;

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(GameProcessManager gameProcessManager)
    {
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

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
    
    [ContextMenu("Spawn Boss")]
    public void SpawnBossAtCenter()
    {
        spawner_AtGates.SpawnBoss(PoolItemsTypes.Zombie_Boss);
    }

    public void SpawnEnemiesWave()
    {
        spawner_BeyondMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, 16);
        spawner_OnMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, 16);
        spawner_AtGates.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, 16);
    }

    private void SubscribeOnEvents()
    {
        _gameProcessManager.OnPlayerLost += GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon += GameProcessManager_PlayerWon_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _gameProcessManager.OnPlayerLost -= GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon -= GameProcessManager_PlayerWon_ExecuteReaction;
    }

    [ContextMenu("Stop")]
    private void StopEnemySpawn()
    {
        canSpawnEnemies = false;

        StopAllCoroutines();

        StartCoroutine(StopEnemySpawnCoroutine());
    }

    private void GameProcessManager_PlayerLost_ExecuteReaction()
    {
        StopEnemySpawn();
    }

    private void GameProcessManager_PlayerWon_ExecuteReaction()
    {
        StopEnemySpawn();
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
