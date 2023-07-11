using Zenject;

public class PlayDeathmatchButton : ButtonInteractionHandler
{
    private MainUI _mainUI;

    #region Zenject
    [Inject]
    private void Construct(MainUI mainUI)
    {
        _mainUI = mainUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        _mainUI.DeathmatchButtonPressed_ExecuteReaction();
    }
}
