using UnityEngine;
using System;
using Zenject;

public class EnemiesCharacteristicsManager : MonoBehaviour
{
    [Header("Enemies Current Data")]
    [Space]
    [SerializeField] private EnemyDataStruct currentEnemiesData;
    [Header("Enemies Start Data")]
    [Space]
    [SerializeField] private EnemyStartDataSO enemyStartDataSO;
    [Header("Upgrade Data Per Player Level")]
    [Space]
    [SerializeField] float hpUpgradeValue = 2f;
    [SerializeField] float damageUpgradeValue = 2f;
    [SerializeField] float baseSpeedUpgradeValue = 0.05f;
    [Header("Upgrade Data Per Chapter")]
    [Space]
    [SerializeField] private float allCharacteristicsUpgradePercent = 5f;
    [Header("Finish Upgrade Data")]
    [Space]
    [SerializeField] private int massiveLevelUpgradeTreshold = 20;
    [SerializeField] private int massiveLevelUpgradeMultiplyer = 2;

    private PlayerExperienceManager _playerExperienceManager;
    private ChaptersProgressManager _chaptersProgressManager;
    private GameProcessManager _gameProcessManager;

    public EnemyDataStruct CurrentEnemiesData { get => currentEnemiesData; }

    public event Action<bool> OnEnemyCharacteristicsUpgraded;

    private void Start()
    {
        _playerExperienceManager.OnPlayerGotNewLevel += PlayerExperienceManager_OnPlayerGotNewLevel_ExecuteReaction;
        _gameProcessManager.OnGameStarted += GameProcessManager_OnGameStarted_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _playerExperienceManager.OnPlayerGotNewLevel -= PlayerExperienceManager_OnPlayerGotNewLevel_ExecuteReaction;
        _gameProcessManager.OnGameStarted -= GameProcessManager_OnGameStarted_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerExperienceManager playerExperienceManager, ChaptersProgressManager chaptersProgressManager,
                           GameProcessManager gameProcessManager)
    {
        _playerExperienceManager = playerExperienceManager;
        _chaptersProgressManager = chaptersProgressManager;
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    private void PlayerExperienceManager_OnPlayerGotNewLevel_ExecuteReaction()
    {
        EnemyDataStruct newEnemyData = currentEnemiesData;

        if(_playerExperienceManager.CurrentLevel < massiveLevelUpgradeTreshold)
        {
            newEnemyData.hp += hpUpgradeValue;
            newEnemyData.damage += damageUpgradeValue;
            newEnemyData.speed += baseSpeedUpgradeValue;
        }
        else
        {
            newEnemyData.hp *= massiveLevelUpgradeMultiplyer;
            newEnemyData.damage *= massiveLevelUpgradeMultiplyer;
            newEnemyData.speed *= massiveLevelUpgradeMultiplyer;
        }

        currentEnemiesData = newEnemyData;

        OnEnemyCharacteristicsUpgraded?.Invoke(false);
    }

    private void UpgradeEnemiesDataInNewChapter()
    {
        EnemyDataStruct newEnemyData = currentEnemiesData;

        newEnemyData.hp += GetPercentValueOfCharacteristic(newEnemyData.hp);
        newEnemyData.damage += GetPercentValueOfCharacteristic(newEnemyData.damage);
        newEnemyData.speed += GetPercentValueOfCharacteristic(newEnemyData.speed);

        currentEnemiesData = newEnemyData;

        OnEnemyCharacteristicsUpgraded?.Invoke(true);
    }

    private void GameProcessManager_OnGameStarted_ExecuteReaction()
    {
        currentEnemiesData = enemyStartDataSO.EnemyStartData;

        if(_chaptersProgressManager.FinishedChaptersCounter > 0)
        {
            UpgradeEnemiesDataInNewChapter();
        }
        else
        {
            OnEnemyCharacteristicsUpgraded?.Invoke(true);
        }
    }

    private float GetPercentValueOfCharacteristic(float characteristicValue)
    {
        float percentValue = (characteristicValue * (allCharacteristicsUpgradePercent * _chaptersProgressManager.FinishedChaptersCounter)) / CommonValues.maxPercentAmount;

        return percentValue;
    }
}
