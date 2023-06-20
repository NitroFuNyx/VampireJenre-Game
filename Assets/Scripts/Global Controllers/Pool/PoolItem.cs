using UnityEngine;
using System;

public class PoolItem : MonoBehaviour
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private PoolItemsTypes poolItemType;

    private PoolItemsManager _poolItemsManager;

    private ResourcesManager _resourcesManager;
    private GameProcessManager _gameProcessManager;
    private PlayerExperienceManager _playerExperienceManager;
    private PlayerCharacteristicsManager playerCharacteristicsManager;
    private Transform dynamicEnvironment;

    private PickableItemsManager _pickableItemsManager;
    private SpawnEnemiesManager _spawnEnemiesManager;
    private EnemiesCharacteristicsManager _enemiesCharacteristicsManager;

    private AdsManager _adsManager;
    private AdsController _adsController;

    private VFXManager _vfxManager;

    public PoolItemsTypes PoolItemType { get => poolItemType;}
    public PoolItemsManager PoolItemsManager { get => _poolItemsManager; private set => _poolItemsManager = value; }

    public ResourcesManager ResourcesManager { get => _resourcesManager; private set => _resourcesManager = value; }
    public GameProcessManager GameProcessManager { get => _gameProcessManager; private set => _gameProcessManager = value; }
    public PlayerExperienceManager PlayerExperienceManager { get => _playerExperienceManager; private set => _playerExperienceManager = value; }

    public PlayerCharacteristicsManager CharacteristicsManager => playerCharacteristicsManager;

    public Transform DynamicEnvironment => dynamicEnvironment;

    public PickableItemsManager PickableItemsManager { get => _pickableItemsManager; private set => _pickableItemsManager = value; }
    public SpawnEnemiesManager SpawnEnemiesManager { get => _spawnEnemiesManager; private set => _spawnEnemiesManager = value; }
    public EnemiesCharacteristicsManager _EnemiesCharacteristicsManager { get => _enemiesCharacteristicsManager; private set => _enemiesCharacteristicsManager = value; }
    public VFXManager VfxManager { get => _vfxManager; }

    #region Events Declaration
    public event Action OnItemResetRequired;
    public event Action OnObjectAwakeStateSet;
    #endregion Events Declaration

    public void ResetPoolItem()
    {
        OnItemResetRequired?.Invoke();
    }

    public void SetObjectAwakeState()
    {
        OnObjectAwakeStateSet?.Invoke();
    }

    public void CashComponents(PoolItemsManager poolItemsManager, ResourcesManager resourcesManager, GameProcessManager gameProcessManager,
                               PlayerExperienceManager playerExperienceManager,PlayerCharacteristicsManager playerCharacteristicsManager,
                               PickableItemsManager pickableItemsManager, Transform dynamicEnvironment, SpawnEnemiesManager spawnEnemiesManager,
                               EnemiesCharacteristicsManager enemiesCharacteristicsManager, AdsManager adsManager, AdsController adsController,
                               VFXManager vfxManager)
    {
        this.dynamicEnvironment = dynamicEnvironment;
        this.playerCharacteristicsManager = playerCharacteristicsManager;
        _poolItemsManager = poolItemsManager;
        _resourcesManager = resourcesManager;
        _gameProcessManager = gameProcessManager;
        _playerExperienceManager = playerExperienceManager;
        _pickableItemsManager = pickableItemsManager;
        _spawnEnemiesManager = spawnEnemiesManager;
        _enemiesCharacteristicsManager = enemiesCharacteristicsManager;
        _adsManager = adsManager;
        _adsController = adsController;
        _vfxManager = vfxManager;
    }
}