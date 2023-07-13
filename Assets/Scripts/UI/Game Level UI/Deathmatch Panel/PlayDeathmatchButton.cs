using Zenject;

public class PlayDeathmatchButton : ButtonInteractionHandler
{
    private DeathmatchAccessManager _deathmatchAccessManager;

    #region Zenject
    [Inject]
    private void Construct(DeathmatchAccessManager deathmatchAccessManager)
    {
        _deathmatchAccessManager = deathmatchAccessManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _deathmatchAccessManager.PlayDeathmatchButtonPressed_ExecuteReaction(ShowAnimation_ButtonPressed, ShowFailedBuyingResult);
    }

    private void ShowFailedBuyingResult()
    {

    }
}
