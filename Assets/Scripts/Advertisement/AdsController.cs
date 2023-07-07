using System;
using GoogleMobileAds.Api.Mediation.AppLovin;
using UnityEngine;

public class AdsController : MonoBehaviour
{
    private void Awake()
    {
        AppLovin.SetHasUserConsent(true);
    }

    #region Events declaration

    public event Action LoadInterstitialAd;
    public event Action<Action> LoadRewardedAd;
    public event Action OnRewardAdViewed;
    public event Action OnAdvertAbsence;
    public event Action LoadBannerAd;
    public event Action DeleteBannerAd;

    public event Action OnInterstialAdClosed;

    #endregion

    public void LoadInterstitial()
    {
        LoadInterstitialAd?.Invoke();
    }
    public void LoadRewarded()
    {
        Debug.Log("ASK FOR REWARD AD");
        LoadRewardedAd?.Invoke(GiveAReward);
    }
    public void LoadBanner()
    {
        LoadBannerAd?.Invoke();
    }
    public void DestroyBanner()
    {
        DeleteBannerAd?.Invoke();
    }

    public void GiveAReward()
    {
        Debug.LogWarning("REWARD APPROVED");
        OnRewardAdViewed?.Invoke();
    }

    public void CloseInterstitial()
    {
        Debug.Log($"Close Interstial Event");
        OnInterstialAdClosed?.Invoke();
    }

    public void NotifyNoAds()
    {
        OnAdvertAbsence?.Invoke();
    }
}
