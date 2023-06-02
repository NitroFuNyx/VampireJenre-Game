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

    private PlayerExperienceManager _playerExperienceManager;

    public EnemyDataStruct CurrentEnemiesData { get => currentEnemiesData; }

    public event Action OnEnemyCharacteristicsUpgraded;

    private void Awake()
    {
        currentEnemiesData = enemyStartDataSO.EnemyStartData;
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerExperienceManager playerExperienceManager)
    {
        _playerExperienceManager = playerExperienceManager;
    }
    #endregion Zenject

    private void PlayerExperienceManager_OnPlayerGotNewLevel_ExecuteReaction()
    {
        EnemyDataStruct newEnemyData = currentEnemiesData;
        newEnemyData.hp += 2f;
        newEnemyData.damage += 2f;
        newEnemyData.speed += 0.05f;

        currentEnemiesData = newEnemyData;

        OnEnemyCharacteristicsUpgraded?.Invoke();
    }
}
