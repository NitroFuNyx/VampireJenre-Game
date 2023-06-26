using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Api.Mediation.AppLovin;
using UnityEngine;
using Zenject;

public class AdsInterstitialSetup : MonoBehaviour
{
    
    private InterstitialAd interstitialAd;
    private AdsController adsController;
    
#if UNITY_ANDROID
    //private string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
    private string _adUnitId = "ca-app-pub-4398823708131797/4227292415";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-4398823708131797/8772988185";
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
            LoadInterstitialAd();

        });
        GoogleMobileAds.Mediation.AppLovin.Api.AppLovin.Initialize();

        
        
    }
    // These ad units are configured to always serve test ads.
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    #region Subscribe events

    private void SubscribeEvents()
    {
        adsController.LoadInterstitialAd += ShowAd;
    }

    private void UnSubscribeEvents()
    {
        adsController.LoadInterstitialAd -= ShowAd;

    }

    #endregion
    #region Zenject
    [Inject]
    private void InjectDependencies(AdsController adsController)
    {
        this.adsController = adsController;
    }
    #endregion

   

    /// <summary>
    /// Loads the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
                RegisterEventHandlers(interstitialAd);

                
            });
    }
    /// <summary>
    /// Shows the interstitial ad.
    /// </summary>
    public void ShowAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            LoadInterstitialAd();
        }
    }

    /// <summary>
    /// Destroys the ad.
    /// </summary>
    public void DestroyAd()
    {
        if (interstitialAd != null)
        {
            Debug.Log("Destroying banner ad.");
            interstitialAd.Destroy();
            interstitialAd = null;
            LoadInterstitialAd();
        }
    }
    

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            adsController.CloseInterstitial();
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }
    
}
