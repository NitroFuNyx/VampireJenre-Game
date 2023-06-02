using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
