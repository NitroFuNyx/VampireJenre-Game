using UnityEngine;
using System;
using Zenject;

public class PlayerExperienceManager : MonoBehaviour
{
    [Header("Experience Data")]
    [Space]
    [SerializeField] private float currentXp = 0;
    [SerializeField] private float upgradeXpValue = 100;

    private GameProcessManager _gameProcessManager;

    #region Events Declaration
    public event Action<float, float> OnPlayerXpAmountChanged;
    #endregion Events Declaration

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

    public void IncreaseXpValue(float deltaXp)
    {
        currentXp += deltaXp;

        if(currentXp >= upgradeXpValue)
        {
            float newLevelStartXp = currentXp - upgradeXpValue;
            currentXp = newLevelStartXp;
            upgradeXpValue = 100; // reset upgradeXpValue
        }

        OnPlayerXpAmountChanged?.Invoke(currentXp, upgradeXpValue);
    }

    private void SubscribeOnEvents()
    {
        _gameProcessManager.OnPlayerLost += GameProcessManager_PlayerLost_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _gameProcessManager.OnPlayerLost -= GameProcessManager_PlayerLost_ExecuteReaction;
    }

    private void ResetPlayerProgress()
    {
        currentXp = 0f;
        OnPlayerXpAmountChanged?.Invoke(currentXp, upgradeXpValue);
    }

    private void GameProcessManager_PlayerLost_ExecuteReaction()
    {
        ResetPlayerProgress();
    }
}
