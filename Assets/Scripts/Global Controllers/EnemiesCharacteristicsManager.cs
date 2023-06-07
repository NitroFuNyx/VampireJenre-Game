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

    private PlayerExperienceManager _playerExperienceManager;
    private ChaptersProgressManager _chaptersProgressManager;

    public EnemyDataStruct CurrentEnemiesData { get => currentEnemiesData; }

    public event Action OnEnemyCharacteristicsUpgraded;

    private void Awake()
    {
        currentEnemiesData = enemyStartDataSO.EnemyStartData;
    }

    private void Start()
    {
        _playerExperienceManager.OnPlayerGotNewLevel += PlayerExperienceManager_OnPlayerGotNewLevel_ExecuteReaction;

        _chaptersProgressManager.OnChaptersProhressUpdated += ChaptersProgressManager_OnChaptersProgressUpdated_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _playerExperienceManager.OnPlayerGotNewLevel -= PlayerExperienceManager_OnPlayerGotNewLevel_ExecuteReaction;

        _chaptersProgressManager.OnChaptersProhressUpdated -= ChaptersProgressManager_OnChaptersProgressUpdated_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerExperienceManager playerExperienceManager, ChaptersProgressManager chaptersProgressManager)
    {
        _playerExperienceManager = playerExperienceManager;
        _chaptersProgressManager = chaptersProgressManager;
    }
    #endregion Zenject

    private void PlayerExperienceManager_OnPlayerGotNewLevel_ExecuteReaction()
    {
        EnemyDataStruct newEnemyData = currentEnemiesData;
        newEnemyData.hp += hpUpgradeValue;
        newEnemyData.damage += damageUpgradeValue;
        newEnemyData.speed += baseSpeedUpgradeValue;

        currentEnemiesData = newEnemyData;

        OnEnemyCharacteristicsUpgraded?.Invoke();
    }

    private void ChaptersProgressManager_OnChaptersProgressUpdated_ExecuteReaction(int _)
    {
        EnemyDataStruct newEnemyData = currentEnemiesData;

        newEnemyData.hp += GetPercentValueOfCharacteristic(newEnemyData.hp);
        newEnemyData.damage += GetPercentValueOfCharacteristic(newEnemyData.damage);
        newEnemyData.speed += GetPercentValueOfCharacteristic(newEnemyData.speed);

        currentEnemiesData = newEnemyData;

        OnEnemyCharacteristicsUpgraded?.Invoke();
    }

    private float GetPercentValueOfCharacteristic(float characteristicValue)
    {
        float percentValue = (characteristicValue * allCharacteristicsUpgradePercent) / CommonValues.maxPercentAmount;

        return percentValue;
    }
}
