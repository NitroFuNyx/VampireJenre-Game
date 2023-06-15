using UnityEngine;
using TMPro;
using Zenject;

public class LoosePanel : GameLevelSubPanel
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI defeatedEnemyCounterText;
    [SerializeField] private TextMeshProUGUI coinsAmountForLevelText;

    private ResourcesManager _resourcesManager;
    private SpawnEnemiesManager _spawnEnemiesManager;

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager, SpawnEnemiesManager spawnEnemiesManager)
    {
        _resourcesManager = resourcesManager;
        _spawnEnemiesManager = spawnEnemiesManager;
    }
    #endregion Zenject

    public void UpdatePlayerResults()
    {
        defeatedEnemyCounterText.text = $"{_spawnEnemiesManager.DefeatedEnemiesCounter}";
        coinsAmountForLevelText.text = $"{_resourcesManager.CurrentLevelCoinsAmount}";
    }

    public void GetUpButtonPressed_ExecuteReaction()
    {

    }
}
