using UnityEngine;
using Save_Load_System;
using TMPro;
using Zenject;

public class DeathmatchFinishPanel : GameLevelSubPanel
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI thisRound_DefeatedEnemyCounterText;
    [SerializeField] private TextMeshProUGUI thisRound_TimeText;
    [SerializeField] private TextMeshProUGUI best_DefeatedEnemyCounterText;
    [SerializeField] private TextMeshProUGUI best_TimeText;

    private SpawnEnemiesManager _spawnEnemiesManager;
    private TimersManager _timersManager;
    private PlayerStatisticsManager _playerStatisticsManager; 

    #region Zenject
    [Inject]
    private void Construct(SpawnEnemiesManager spawnEnemiesManager, TimersManager timersManager, PlayerStatisticsManager playerStatisticsManager)
    {
        _spawnEnemiesManager = spawnEnemiesManager;
        _timersManager = timersManager;
        _playerStatisticsManager = playerStatisticsManager;
    }
    #endregion Zenject

    public void UpdatePlayerResults()
    {
        thisRound_DefeatedEnemyCounterText.text = $"{_spawnEnemiesManager.DefeatedEnemiesCounter}";
        thisRound_TimeText.text = $"{_timersManager.GetFormatedCurrentTimeString()}";

        best_DefeatedEnemyCounterText.text = $"{_playerStatisticsManager.PlayerStatsData.killedMonstersAmount}";
        best_TimeText.text = $"{_playerStatisticsManager.PlayerStatsData.longestTimePlayed}";
           
    }
}
