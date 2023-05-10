using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class GameLevelUI : MainCanvasPanel
{
    [Header("Progress Bars")]
    [Space]
    [SerializeField] private Image mapProgressBar;
    [SerializeField] private Image playerExperinceProgressBar;
    [Header("Resources")]
    [Space]
    [SerializeField] private TextMeshProUGUI coinsAmountText;
    [Header("Time")]
    [Space]
    [SerializeField] private TextMeshProUGUI stopwatchValueText;

    private ResourcesManager _resourcesManager;
    private GameProcessManager _gameProcessManager;
    private PlayerExperienceManager _playerExperienceManager;
    private TimersManager _timersManager;

    private void Start()
    {
        SubscribeOnEvents();

        mapProgressBar.fillAmount = 0f;
        playerExperinceProgressBar.fillAmount = 0f;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager, GameProcessManager gameProcessManager, PlayerExperienceManager playerExperienceManager,
                           TimersManager timersManager)
    {
        _resourcesManager = resourcesManager;
        _gameProcessManager = gameProcessManager;
        _playerExperienceManager = playerExperienceManager;
        _timersManager = timersManager;
    }
    #endregion Zenject

    private void SubscribeOnEvents()
    {
        _resourcesManager.OnCoinsAmountChanged += ResourcesManager_CoinsAmountChanged_ExecuteReaction;
        _resourcesManager.OnGemsAmountChanged += ResourcesManager_GemsAmountChanged_ExecuteReaction;

        _gameProcessManager.OnMapProgressChanged += GameProcessManager_MapProgressChanged_ExecuteReaction;

        _playerExperienceManager.OnPlayerXpAmountChanged += PlayerExperienceManager_PlayerXpAmountChanged_ExecuteReaction;

        _timersManager.OnStopwatchValueChanged += TimersManager_OnStopwatchValueChanged_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _resourcesManager.OnCoinsAmountChanged -= ResourcesManager_CoinsAmountChanged_ExecuteReaction;
        _resourcesManager.OnGemsAmountChanged -= ResourcesManager_GemsAmountChanged_ExecuteReaction;

        _gameProcessManager.OnMapProgressChanged -= GameProcessManager_MapProgressChanged_ExecuteReaction;

        _playerExperienceManager.OnPlayerXpAmountChanged -= PlayerExperienceManager_PlayerXpAmountChanged_ExecuteReaction;

        _timersManager.OnStopwatchValueChanged -= TimersManager_OnStopwatchValueChanged_ExecuteReaction;
    }

    public override void PanelActivated_ExecuteReaction()
    {

    }

    public override void PanelDeactivated_ExecuteReaction()
    {

    }

    private void ResourcesManager_CoinsAmountChanged_ExecuteReaction(int amount)
    {
        coinsAmountText.text = $"{amount}";
    }

    private void ResourcesManager_GemsAmountChanged_ExecuteReaction(int amount)
    {

    }

    private void GameProcessManager_MapProgressChanged_ExecuteReaction(float currentAmount, float maxAmount)
    {
        mapProgressBar.fillAmount = currentAmount.Remap(0, maxAmount, 0f, 1f);
    }

    private void PlayerExperienceManager_PlayerXpAmountChanged_ExecuteReaction(float currentAmount, float maxAmount)
    {
        playerExperinceProgressBar.fillAmount = currentAmount.Remap(0, maxAmount, 0f, 1f);
    }

    private void TimersManager_OnStopwatchValueChanged_ExecuteReaction(string timeString)
    {
        stopwatchValueText.text = timeString;
    }
}
