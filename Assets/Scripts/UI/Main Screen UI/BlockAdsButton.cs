using Zenject;

public class BlockAdsButton : ButtonInteractionHandler
{
    private AdsManager _adsManager;

    private void Start()
    {
        _adsManager.OnSuccessfullAdsBlockerPurchase += AdsManager_OnSuccessfullAdsBlockerPurchase_ExecuteReaction;
        _adsManager.OnAdsDataLoaded += AdsManager_OnAdsDataLoaded_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _adsManager.OnSuccessfullAdsBlockerPurchase -= AdsManager_OnSuccessfullAdsBlockerPurchase_ExecuteReaction;
        _adsManager.OnAdsDataLoaded -= AdsManager_OnAdsDataLoaded_ExecuteReaction;
    }

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        //_adsManager.PurchaseAdsBlocker();
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
        HideButton();
    }

    private void AdsManager_OnAdsDataLoaded_ExecuteReaction()
    {
        if(_adsManager.BlockAdsOptionPurchased)
        {
            HideButton();
        }
    }
}
