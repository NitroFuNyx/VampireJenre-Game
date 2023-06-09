using Zenject;

public class AdsBlockerPurchasedPanel : MainCanvasPanel
{
    private AdsManager _adsManager;

    private void Start()
    {
        HidePanel();

        _adsManager.OnSuccessfullAdsBlockerPurchase += AdsManager_OnSuccessfullAdsBlockerPurchase_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _adsManager.OnSuccessfullAdsBlockerPurchase -= AdsManager_OnSuccessfullAdsBlockerPurchase_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(AdsManager adsManager)
    {
        _adsManager = adsManager;
    }
    #endregion Zenject

    private void AdsManager_OnSuccessfullAdsBlockerPurchase_ExecuteReaction()
    {
        ShowPanel();
    }

    public override void PanelActivated_ExecuteReaction()
    {
        
    }

    public override void PanelDeactivated_ExecuteReaction()
    {
        
    }
}
