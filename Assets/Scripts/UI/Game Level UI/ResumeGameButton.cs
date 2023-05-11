using Zenject;

public class ResumeGameButton : ButtonInteractionHandler
{
    private GameLevelUI _gameLevelUI;
    private SystemTimeManager _systemTimeManager;

    #region Zenject
    [Inject]
    private void Construct(GameLevelUI gameLevelUI, SystemTimeManager systemTimeManager)
    {
        _gameLevelUI = gameLevelUI;
        _systemTimeManager = systemTimeManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _systemTimeManager.ResumeGame();
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_gameLevelUI.ResumeGame));
    }
}
