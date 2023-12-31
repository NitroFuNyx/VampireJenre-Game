using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Api.Mediation.AppLovin;
using UnityEngine;
using Zenject;

public class AdsRewardedSetup : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private AdsController adsController;
#if UNITY_ANDROID
    //private string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
    private string _adUnitId = "ca-app-pub-4398823708131797/5025314860";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-4398823708131797/8904904026";
#else
  private string _adUnitId = "unused";
#endif
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        
        MobileAds.Initialize(initStatus =>
        {
            var map = initStatus.getAdapterStatusMap();
            foreach (var (className,status) in map) {
                switch (status.InitializationState) {
                    case AdapterState.NotReady:
                        Debug.Log(
                            $"Ads - AdsManager - Adapter: {className} Description: {status.Description} status : not ready.");
                        break;
                    case AdapterState.Ready:
                        Debug.Log(
                            $"Ads - AdsManager - Adapter: {className} Description: {status.Description} status : ready");
                        
                        break;
                }
            }
            LoadRewardedAd();

        });
       GoogleMobileAds.Mediation.AppLovin.Api.AppLovin.Initialize();
        
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    // These ad units are configured to always serve test ads.
    private void SubscribeEvents()
    {
        adsController.LoadRewardedAd += ShowRewardedAd;
    }

    private void UnSubscribeEvents()
    {
        adsController.LoadRewardedAd -= ShowRewardedAd;

    }
    #region Zenject
    [Inject]
    private void InjectDependencies(AdsController adsController)
    {
        this.adsController = adsController;
    }
    #endregion
    

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    private void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
            //adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }
    
                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                RegisterEventHandlers(rewardedAd);

            });
    }
    private void ShowRewardedAd(Action onPlayerRewarded)
    {
        //Debug.LogWarning($"IN METHOD OF SHOWING REWARDED next {rewardedAd} {rewardedAd.CanShowAd()}");
       
        Debug.LogWarning("Showing IN METHOD reward" );
        
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                Debug.LogWarning("Showing reward");

                onPlayerRewarded?.Invoke();
            });
        }
        else
        {
            Debug.LogError("Rewarded ad is not ready yet.");
            LoadRewardedAd();
            adsController.NotifyNoAds();

        }
    }
    
    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadRewardedAd();
        };
    }
   
}
