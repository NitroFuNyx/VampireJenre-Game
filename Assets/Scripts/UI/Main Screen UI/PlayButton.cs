using Zenject;

public class PlayButton : ButtonInteractionHandler
{
    private MainUI _mainUI;

    public override void ButtonActivated()
    {
        _mainUI.PlayButtonPressed_ExecuteReaction();
    }

    #region Zenject
    [Inject]
    private void Construct(MainUI mainUI)
    {
        _mainUI = mainUI;
    }
    #endregion Zenject

    
}
