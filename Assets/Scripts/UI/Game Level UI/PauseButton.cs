using Zenject;

public class PauseButton : ButtonInteractionHandler
{
    private GameLevelUI _gameLevelUI;

    #region Zenject
    [Inject]
    private void Construct(GameLevelUI gameLevelUI)
    {
        _gameLevelUI = gameLevelUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_gameLevelUI.ShowPausePanel));
    }
}
