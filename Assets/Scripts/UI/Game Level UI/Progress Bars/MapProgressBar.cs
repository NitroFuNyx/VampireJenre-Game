using Zenject;

public class MapProgressBar : ProgressBar
{
    private GameProcessManager _gameProcessManager;

    #region Zenject
    [Inject]
    private void Construct(GameProcessManager gameProcessManager)
    {
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    protected override void SubscribeOnEvents()
    {
        _gameProcessManager.OnMapProgressChanged += ProgressBarValueChanged_ExecuteReaction;
    }

    protected override void UnsubscribeFromEvents()
    {
        _gameProcessManager.OnMapProgressChanged -= ProgressBarValueChanged_ExecuteReaction;
    }

    protected override void SetProgressBarStartValue()
    {
        progressBarImage.fillAmount = 0f;
    }
}
