using System;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Api.Mediation.AppLovin;
using UnityEngine;
using Zenject;

public class AdsBannerSetup : MonoBehaviour
{
    
private BannerView _bannerView;
private AdsController adsController;

// Use this for initialization
    void Start()
    {
        
        RequestConfiguration requestConfiguration = new RequestConfiguration
        {
            TestDeviceIds = new List<string>
            {
                AdRequest.TestDeviceSimulator,
                // Add your test device IDs (replace with your own device IDs).
                #if UNITY_IPHONE
                "96e23e80653bb28980d3f40beb58915c"
                #elif UNITY_ANDROID
                "f8d9637e-a47f-400d-95b9-942a438933a6"
                #endif
            }
        };
        MobileAds.SetRequestConfiguration(requestConfiguration);

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
        });
        AppLovin.Initialize();
    }
    
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
        adsController.LoadBannerAd += ShowAd;
        adsController.DeleteBannerAd += DestroyAd;
    }

    private void UnSubscribeEvents()
    {
        adsController.LoadBannerAd -= ShowAd;
        adsController.DeleteBannerAd -= DestroyAd;

    }

    #endregion
    #region Zenject
    [Inject]
    private void InjectDependencies(AdsController adsController)
    {
        this.adsController = adsController;
    }
    #endregion

    public void OnGUI()
    {
        GUI.skin.label.fontSize = 60;
        Rect textOutputRect = new Rect(
          0.15f * Screen.width,
          0.25f * Screen.height,
          0.7f * Screen.width,
          0.3f * Screen.height);
    }

    private void ShowAd()
    {
        RequestBanner();

    }
    private void RequestBanner()
    {
        // These ad units are configured to always serve test ads.
        #if UNITY_EDITOR
            string adUnitId = "unused";
        #elif UNITY_ANDROID
            //string adUnitId = "ca-app-pub-3212738706492790/6113697308";
            string adUnitId = "ca-app-pub-4398823708131797/7651478205";//my
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-4398823708131797/6234492742";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Clean up banner ad before creating a new one.
        if (_bannerView != null)
        {
            _bannerView.Destroy();
        }

        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        _bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        // Register for ad events.
        _bannerView.OnBannerAdLoaded += OnBannerAdLoaded;
        _bannerView.OnBannerAdLoadFailed += OnBannerAdLoadFailed;

        AdRequest adRequest = new AdRequest();

        // Load a banner ad.
        _bannerView.LoadAd(adRequest);
    }

    /// <summary>
    /// Destroys the ad.
    /// </summary>
    private void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    #region Banner callback handlers

    private void OnBannerAdLoaded()
    {
       
    }

    private void OnBannerAdLoadFailed(LoadAdError error)
    {
        Debug.LogError("Banner view failed to load an ad with error : "
                + error);
    }

    #endregion



}
