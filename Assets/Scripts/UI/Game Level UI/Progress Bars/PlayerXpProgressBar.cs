using Zenject;

public class PlayerXpProgressBar : ProgressBar
{
    private PlayerExperienceManager _playerExperienceManager;

    #region Zenject
    [Inject]
    private void Construct(PlayerExperienceManager playerExperienceManager)
    {
        _playerExperienceManager = playerExperienceManager;
    }
    #endregion Zenject

    protected override void SubscribeOnEvents()
    {
        _playerExperienceManager.OnPlayerXpAmountChanged += ProgressBarValueChanged_ExecuteReaction;
    }

    protected override void UnsubscribeFromEvents()
    {
        _playerExperienceManager.OnPlayerXpAmountChanged -= ProgressBarValueChanged_ExecuteReaction;
    }
    protected override void SetProgressBarStartValue()
    {
        progressBarImage.fillAmount = 0f;
    }
}
