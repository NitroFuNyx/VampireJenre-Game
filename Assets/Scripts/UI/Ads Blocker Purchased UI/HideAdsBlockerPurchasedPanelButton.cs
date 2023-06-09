using Zenject;

public class HideAdsBlockerPurchasedPanelButton : ButtonInteractionHandler
{
    private AdsBlockerPurchasedPanel _adsBlockerPurchasedPanel;

    #region Zenject
    [Inject]
    private void Construct(AdsBlockerPurchasedPanel adsBlockerPurchasedPanel)
    {
        _adsBlockerPurchasedPanel = adsBlockerPurchasedPanel;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _adsBlockerPurchasedPanel.HidePanel();
    }
}
