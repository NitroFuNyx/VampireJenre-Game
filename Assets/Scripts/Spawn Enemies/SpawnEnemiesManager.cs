using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnEnemiesManager : MonoBehaviour
{
    [Header("Spawners")]
    [Space]
    [SerializeField] private EnemySpawner spawner_BeyondMap;
    [SerializeField] private EnemySpawner spawner_OnMap;
    [SerializeField] private EnemySpawner spawner_AtGates;
    [Header("Spawn Data")]
    [Space]
    [SerializeField] private int spawnAmountInOneWave;
    [SerializeField] private int spawnAmountDelta;
    [SerializeField] private float spawnDelay = 8f;
    [SerializeField] private int maxEnemiesAmount = 20;
    [Header("Enemies On Map")]
    [Space]
    [SerializeField] private List<EnemyComponentsManager> enemiesOnMapList = new List<EnemyComponentsManager>();

    private GameProcessManager _gameProcessManager;
    private PlayerExperienceManager _playerExperienceManager;

    private bool canSpawnEnemies = true;

    private int defeatedEnemiesCounter = 0;

    public int DefeatedEnemiesCounter { get => defeatedEnemiesCounter; private set => defeatedEnemiesCounter = value; }

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
    private void Construct(GameProcessManager gameProcessManager, PlayerExperienceManager playerExperienceManager)
    {
        _gameProcessManager = gameProcessManager;
        _playerExperienceManager = playerExperienceManager;
    }
    #endregion Zenject

    public void SpawnEnemies()
    {
        canSpawnEnemies = true;
        StartCoroutine(SpawnEnemiesWavesCoroutine());
    }
    
    [ContextMenu("Spawn Boss")]
    public void SpawnBossAtCenter()
    {
        spawner_AtGates.SpawnBoss(PoolItemsTypes.Zombie_Boss);
    }

    public void SpawnEnemiesWave()
    {
        if(_playerExperienceManager.CurrentLevel < 10 && enemiesOnMapList.Count < maxEnemiesAmount)
        {
            spawner_BeyondMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, spawnAmountInOneWave);
            spawner_OnMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, spawnAmountInOneWave);
            spawner_AtGates.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, spawnAmountInOneWave);
        }
    }

    public void AddEnemyToOnMapList(EnemyComponentsManager enemy)
    {
        enemiesOnMapList.Add(enemy);
    }

    public void RemoveEnemyFronOnMapList(EnemyComponentsManager enemy)
    {
        enemiesOnMapList.Remove(enemy);
        defeatedEnemiesCounter++;
    }

    private void SubscribeOnEvents()
    {
        _gameProcessManager.OnPlayerLost += GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon += GameProcessManager_PlayerWon_ExecuteReaction;

        _playerExperienceManager.OnPlayerGotNewLevel += PlayerExperienceManager_OnPlayerGotNewLevel_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _gameProcessManager.OnPlayerLost -= GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon -= GameProcessManager_PlayerWon_ExecuteReaction;

        _playerExperienceManager.OnPlayerGotNewLevel -= PlayerExperienceManager_OnPlayerGotNewLevel_ExecuteReaction;
    }

    [ContextMenu("Stop")]
    private void StopEnemySpawn()
    {
        canSpawnEnemies = false;

        StopAllCoroutines();

        enemiesOnMapList.Clear();
        defeatedEnemiesCounter = 0;
        Debug.Log($"Stop Enemies Spawn");

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

    private void PlayerExperienceManager_OnPlayerGotNewLevel_ExecuteReaction()
    {
        if(_playerExperienceManager.CurrentLevel < 10)
        {
            spawnAmountInOneWave = 3;
        }
        else
        {
            spawnAmountInOneWave = 6;
        }
        Debug.Log($"Spawning {spawnAmountInOneWave} Enemies At One Category");
    }

    private IEnumerator SpawnEnemiesWavesCoroutine()
    {
        while(canSpawnEnemies)
        {
            SpawnEnemiesWave();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private IEnumerator StopEnemySpawnCoroutine()
    {
        Debug.Log($"Hide Enemies");
        yield return null;
        spawner_BeyondMap.ReturnAllEnemiesToPool();
        spawner_OnMap.ReturnAllEnemiesToPool();
        spawner_AtGates.ReturnAllEnemiesToPool();
    }
}
