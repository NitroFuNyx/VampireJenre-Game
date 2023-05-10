using Zenject;

public class RewardsButton : ButtonInteractionHandler
{
    private MainUI _mainUI;
    private MenuButtonsUI _menuButtonsUI;

    #region Zenject
    [Inject]
    private void Construct(MainUI mainUI, MenuButtonsUI menuButtonsUI)
    {
        _mainUI = mainUI;
        _menuButtonsUI = menuButtonsUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _menuButtonsUI.ChangeScreenBlockingState(true);
        ShowAnimation_ButtonPressed();
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_mainUI.ShowRewardsUI));
    }
}
