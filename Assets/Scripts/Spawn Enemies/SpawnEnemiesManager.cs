using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnEnemiesManager : MonoBehaviour
{
    [Header("Spawners Cementary")]
    [Space]
    [SerializeField] private EnemySpawner spawner_BeyondMap;
    [SerializeField] private EnemySpawner spawner_OnMap;
    [SerializeField] private EnemySpawner spawner_AtGates;
    [Header("Spawners Castle")]
    [Space]
    [SerializeField] private EnemySpawner spawner_InCastle;
    [Header("Spawn Data")]
    [Space]
    [SerializeField] private int spawnAmountInOneWave;
    [SerializeField] private float spawnDelay = 8f;
    [Space]
    [SerializeField] private int firstTresholdLevel = 3;
    [SerializeField] private int firstTresholdMaxEnemiesAmount = 10;
    [Space]
    [SerializeField] private int secondTresholdLevel = 10;
    [SerializeField] private int secondTresholdMaxEnemiesAmount = 20;
    [Space]
    [SerializeField] private int thirdTresholdLevel = 20;
    [SerializeField] private int thirdTresholdEnemiesInWaveMultiplyer = 2;
    [Header("Enemies On Map")]
    [Space]
    [SerializeField] private List<EnemyComponentsManager> enemiesOnMapList = new List<EnemyComponentsManager>();

    private GameProcessManager _gameProcessManager;
    private PlayerExperienceManager _playerExperienceManager;
    private ChaptersProgressManager _chaptersProgressManager;
    private MapsManager _mapsManager;

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
    private void Construct(GameProcessManager gameProcessManager, PlayerExperienceManager playerExperienceManager, 
                           ChaptersProgressManager chaptersProgressManager, MapsManager mapsManager)
    {
        _gameProcessManager = gameProcessManager;
        _playerExperienceManager = playerExperienceManager;
        _chaptersProgressManager = chaptersProgressManager;
        _mapsManager = mapsManager;
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
        if(_mapsManager.CurrentLevelMap == LevelMaps.Cementary)
        {
            if (_chaptersProgressManager.FinishedChaptersCounter == 0)
            {
                spawner_AtGates.SpawnBoss(PoolItemsTypes.Zombie_Boss);
            }
            else if (_chaptersProgressManager.FinishedChaptersCounter == 1)
            {
                spawner_AtGates.SpawnBoss(PoolItemsTypes.Orc_Boss);
            }
            else
            {
                spawner_AtGates.SpawnBoss(PoolItemsTypes.Demon_Boss);
            }
        }
        else if(_mapsManager.CurrentLevelMap == LevelMaps.Castle)
        {
            spawner_InCastle.SpawnBoss(PoolItemsTypes.Zombie_Boss);
        }

        canSpawnEnemies = false;

        StopAllCoroutines();
    }

    public void SpawnEnemiesWave()
    {
        if (_playerExperienceManager.CurrentLevel <= firstTresholdLevel && enemiesOnMapList.Count < firstTresholdMaxEnemiesAmount)
        {
            if(_mapsManager.CurrentLevelMap == LevelMaps.Cementary)
            {
                spawner_BeyondMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, spawnAmountInOneWave);
                spawner_OnMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, spawnAmountInOneWave);
                spawner_AtGates.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, spawnAmountInOneWave);
            }
            else if (_mapsManager.CurrentLevelMap == LevelMaps.Castle)
            {
                spawner_InCastle.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, spawnAmountInOneWave);
                spawner_InCastle.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, spawnAmountInOneWave);
                spawner_InCastle.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, spawnAmountInOneWave);
            }
        }
        else if (_playerExperienceManager.CurrentLevel > firstTresholdLevel && _playerExperienceManager.CurrentLevel < secondTresholdLevel && 
                enemiesOnMapList.Count < secondTresholdMaxEnemiesAmount)
        {
            if (_mapsManager.CurrentLevelMap == LevelMaps.Cementary)
            {
                spawner_BeyondMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, spawnAmountInOneWave);
                spawner_OnMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, spawnAmountInOneWave);
                spawner_AtGates.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, spawnAmountInOneWave);
            }
            else if (_mapsManager.CurrentLevelMap == LevelMaps.Castle)
            {
                spawner_InCastle.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, spawnAmountInOneWave);
                spawner_InCastle.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, spawnAmountInOneWave);
                spawner_InCastle.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, spawnAmountInOneWave);
            }
        }
        else if(_playerExperienceManager.CurrentLevel >= secondTresholdLevel && _playerExperienceManager.CurrentLevel < thirdTresholdLevel)
        {
            if (_mapsManager.CurrentLevelMap == LevelMaps.Cementary)
            {
                spawner_BeyondMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, spawnAmountInOneWave);
                spawner_OnMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, spawnAmountInOneWave);
                spawner_AtGates.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, spawnAmountInOneWave);
            }
            else if (_mapsManager.CurrentLevelMap == LevelMaps.Castle)
            {
                spawner_InCastle.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, spawnAmountInOneWave);
                spawner_InCastle.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, spawnAmountInOneWave);
                spawner_InCastle.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, spawnAmountInOneWave);
            }
        }
        else if (_playerExperienceManager.CurrentLevel >= thirdTresholdLevel)
        {
            if (_mapsManager.CurrentLevelMap == LevelMaps.Cementary)
            {
                spawner_BeyondMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, spawnAmountInOneWave * thirdTresholdEnemiesInWaveMultiplyer);
                spawner_OnMap.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, spawnAmountInOneWave * thirdTresholdEnemiesInWaveMultiplyer);
                spawner_AtGates.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, spawnAmountInOneWave * thirdTresholdEnemiesInWaveMultiplyer);
            }
            else if (_mapsManager.CurrentLevelMap == LevelMaps.Castle)
            {
                spawner_InCastle.SpawnEnemyWave(PoolItemsTypes.Enemy_Ghost, spawnAmountInOneWave * thirdTresholdEnemiesInWaveMultiplyer);
                spawner_InCastle.SpawnEnemyWave(PoolItemsTypes.Enemy_Skeleton, spawnAmountInOneWave * thirdTresholdEnemiesInWaveMultiplyer);
                spawner_InCastle.SpawnEnemyWave(PoolItemsTypes.Enemy_Zombie, spawnAmountInOneWave * thirdTresholdEnemiesInWaveMultiplyer);
            }
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
        _gameProcessManager.OnLevelDataReset += StopEnemySpawn;

        _playerExperienceManager.OnPlayerGotNewLevel += PlayerExperienceManager_OnPlayerGotNewLevel_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _gameProcessManager.OnPlayerLost -= GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon -= GameProcessManager_PlayerWon_ExecuteReaction;
        _gameProcessManager.OnLevelDataReset -= StopEnemySpawn;

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

    private void GameProcessManager_PlayerLost_ExecuteReaction(GameModes _)
    {
        StopEnemySpawn();

        spawner_BeyondMap.ReturnAllEnemiesToPool();
        spawner_OnMap.ReturnAllEnemiesToPool();
        spawner_AtGates.ReturnAllEnemiesToPool();
        spawner_InCastle.ReturnAllEnemiesToPool();
    }

    private void GameProcessManager_PlayerWon_ExecuteReaction()
    {
        StopEnemySpawn();
    }

    private void PlayerExperienceManager_OnPlayerGotNewLevel_ExecuteReaction()
    {
        if(_playerExperienceManager.CurrentLevel < 10)
        {
            spawnAmountInOneWave = 2;
        }
        else
        {
            spawnAmountInOneWave = 6;
        }
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
        spawner_InCastle.ReturnAllEnemiesToPool();
    }
}
