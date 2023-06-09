using Zenject;

public class BlockAdsButton : ButtonInteractionHandler
{
    private AdsManager _adsManager;

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        _adsManager.PurchaseAdsBlocker();
    }

    #region Zenject
    [Inject]
    private void Construct(AdsManager adsManager)
    {
        _adsManager = adsManager;
    }
    #endregion Zenject
}
