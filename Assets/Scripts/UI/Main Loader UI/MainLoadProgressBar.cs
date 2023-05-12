using Zenject;

public class MainLoadProgressBar : ProgressBar
{
    private MainLoaderUI _mainLoaderUI;

    #region Zenject
    [Inject]
    private void Construct(MainLoaderUI mainLoaderUI)
    {
        _mainLoaderUI = mainLoaderUI;
    }
    #endregion Zenject

    protected override void SubscribeOnEvents()
    {
        _mainLoaderUI.OnLoadProgressChanged += ProgressBarValueChanged_ExecuteReaction;
    }

    protected override void UnsubscribeFromEvents()
    {
        _mainLoaderUI.OnLoadProgressChanged -= ProgressBarValueChanged_ExecuteReaction;
    }

    protected override void SetProgressBarStartValue()
    {
        progressBarImage.fillAmount = 0f;
    }
}
