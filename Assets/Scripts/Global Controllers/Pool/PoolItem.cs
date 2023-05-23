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

    public PoolItemsTypes PoolItemType { get => poolItemType;}
    public PoolItemsManager PoolItemsManager { get => _poolItemsManager; private set => _poolItemsManager = value; }

    public ResourcesManager ResourcesManager { get => _resourcesManager; private set => _resourcesManager = value; }
    public GameProcessManager GameProcessManager { get => _gameProcessManager; private set => _gameProcessManager = value; }
    public PlayerExperienceManager PlayerExperienceManager { get => _playerExperienceManager; private set => _playerExperienceManager = value; }

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

    public void CashComponents(PoolItemsManager poolItemsManager, ResourcesManager resourcesManager, GameProcessManager gameProcessManager, PlayerExperienceManager playerExperienceManager)
    {
        _poolItemsManager = poolItemsManager;

        _resourcesManager = resourcesManager;
        _gameProcessManager = gameProcessManager;
        _playerExperienceManager = playerExperienceManager;
    }
}