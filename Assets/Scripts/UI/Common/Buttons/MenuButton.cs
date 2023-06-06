using Zenject;

public class MenuButton : ButtonInteractionHandler
{
    private MainUI _mainUI;
    private GameProcessManager _gameProcessManager;

    #region Zenject
    [Inject]
    private void Construct(MainUI mainUI, GameProcessManager gameProcessManager)
    {
        _mainUI = mainUI;
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _mainUI.ShowMainScreen();
        _gameProcessManager.ResetLevelDataWithSaving();
    }
}
