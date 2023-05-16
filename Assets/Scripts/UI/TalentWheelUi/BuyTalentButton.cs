using Zenject;

public class BuyTalentButton : ButtonInteractionHandler
{
    private TalentsManager _talentsManager;

    private bool buttonActivated = false;

    #region Zenject
    [Inject]
    private void Construct(TalentsManager talentsManager)
    {
        _talentsManager = talentsManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if(!buttonActivated)
        {
            ShowAnimation_ButtonPressed();
            _talentsManager.BuyTalent(ResetButton, BuyingProcessCanceled_ExecuteReaction);
        }
    }

    public void ResetButton()
    {
        buttonActivated = false;
    }

    public void BuyingProcessCanceled_ExecuteReaction()
    {
        buttonActivated = false;
    }
}
