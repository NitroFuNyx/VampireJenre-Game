using Zenject;

public class PlayerHpProgressBar : ProgressBar
{
    private PlayerCollisionsManager _playerCollisionsManager;

    #region Zenject
    [Inject]
    private void Construct(PlayerCollisionsManager playerCollisionsManager)
    {
        _playerCollisionsManager = playerCollisionsManager;
    }
    #endregion Zenject

    protected override void SubscribeOnEvents()
    {
        _playerCollisionsManager.OnHpAmountChanged += ProgressBarValueChanged_ExecuteReaction;
    }

    protected override void UnsubscribeFromEvents()
    {
        _playerCollisionsManager.OnHpAmountChanged -= ProgressBarValueChanged_ExecuteReaction;
    }

    protected override void SetProgressBarStartValue()
    {
        progressBarImage.fillAmount = 1f;
    }
}
