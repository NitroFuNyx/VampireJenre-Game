using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

public class IAPManager : MonoBehaviour
{
    private const string removeAllAdsId = "crazyheads.removeallads";

    private AdsManager _adsManager;

    #region Zenject
    [Inject]
    private void Construct(AdsManager adsManager)
    {
        _adsManager = adsManager;
    }
    #endregion Zenject

    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id == removeAllAdsId)
        {
            Debug.Log($"No Ads Option Purchased");
            _adsManager.PurchaseAdsBlocker();
        }
    }
}
