using UnityEngine;
using TMPro;
using Zenject;

public class VictoryPanel : GameLevelSubPanel
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI coinsAmountForLevelText;
    [SerializeField] private TextMeshProUGUI gemsAmountForLevelText;

    private ResourcesManager _resourcesManager;

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager)
    {
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    public void UpdatePlayerResults()
    {
        coinsAmountForLevelText.text = $"{_resourcesManager.GetCoinsForLevelAmountWithSkillBonus()}";
        gemsAmountForLevelText.text = $"{_resourcesManager.CurrentLevelGemsAmount}";
    }

    public void MultiplyCoinsButtonPressed_ExecuteReaction()
    {
        // show ad
        _resourcesManager.MultiplyCurrentLevelCoinsAmount();
        coinsAmountForLevelText.text = $"{_resourcesManager.CurrentLevelCoinsAmount}";
        gemsAmountForLevelText.text = $"{_resourcesManager.CurrentLevelGemsAmount}";
    }
}
