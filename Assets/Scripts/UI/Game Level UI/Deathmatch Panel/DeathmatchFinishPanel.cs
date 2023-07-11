using UnityEngine;
using TMPro;
using Zenject;

public class DeathmatchFinishPanel : GameLevelSubPanel
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI ThisRound_DefeatedEnemyCounterText;
    [SerializeField] private TextMeshProUGUI ThisRound_TimeText;
    [SerializeField] private TextMeshProUGUI Best_DefeatedEnemyCounterText;
    [SerializeField] private TextMeshProUGUI Best_TimeText;

    private SpawnEnemiesManager _spawnEnemiesManager;
    private TimersManager _timersManager;

    #region Zenject
    [Inject]
    private void Construct(SpawnEnemiesManager spawnEnemiesManager, TimersManager timersManager)
    {
        _spawnEnemiesManager = spawnEnemiesManager;
        _timersManager = timersManager;
    }
    #endregion Zenject

    public void UpdatePlayerResults()
    {
        ThisRound_DefeatedEnemyCounterText.text = $"{_spawnEnemiesManager.DefeatedEnemiesCounter}";
        ThisRound_TimeText.text = $"{_timersManager.GetFormatedCurrentTimeString()}";
    }
}
