using UnityEngine;
using System;
using Zenject;

public class PlayerExperienceManager : MonoBehaviour
{
    [Header("Experience Data")]
    [Space]
    [SerializeField] private float currentXp = 0;
    [SerializeField] private float upgradeXpValue = 5;

    private GameProcessManager _gameProcessManager;

    private int currentLevel = 1;

    public int CurrentLevel { get => currentLevel; private set => currentLevel = value; }

    #region Events Declaration
    public event Action<float, float> OnPlayerXpAmountChanged;
    public event Action OnPlayerGotNewLevel;
    public event Action<int> OnPlayerLevelDataUpdated;
    #endregion Events Declaration

    private void Start()
    {
        SubscribeOnEvents();
        OnPlayerLevelDataUpdated?.Invoke(currentLevel);
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

    public void IncreaseXpValue(float deltaXp)
    {
        currentXp += deltaXp;

        if(currentXp >= upgradeXpValue)
        {
            //float newLevelStartXp = currentXp - upgradeXpValue;
            //currentXp = newLevelStartXp;
            currentXp = 0f;
            upgradeXpValue = 10; // reset upgradeXpValue
            currentLevel++;
            OnPlayerGotNewLevel?.Invoke();
            OnPlayerLevelDataUpdated?.Invoke(currentLevel);
        }

        OnPlayerXpAmountChanged?.Invoke(currentXp, upgradeXpValue);
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

    private void ResetPlayerProgress()
    {
        currentXp = 0f;
        currentLevel = 1;
        OnPlayerXpAmountChanged?.Invoke(currentXp, upgradeXpValue);
        OnPlayerLevelDataUpdated?.Invoke(currentLevel);
    }

    private void GameProcessManager_PlayerLost_ExecuteReaction()
    {
        ResetPlayerProgress();
    }

    private void GameProcessManager_PlayerWon_ExecuteReaction()
    {
        ResetPlayerProgress();
    }
}
