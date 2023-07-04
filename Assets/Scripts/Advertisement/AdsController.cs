using System;
using GoogleMobileAds.Api.Mediation.AppLovin;
using UnityEngine;
using TMPro;

public class AdsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        AppLovin.SetHasUserConsent(true);
    }

    #region Events declaration

    public event Action LoadInterstitialAd;
    public event Action<Action> LoadRewardedAd;
    public event Action OnRewardAdViewed;
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
        LoadRewardedAd?.Invoke(GiveAReward);
        text.text = $"Load Ad";
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
        OnRewardAdViewed?.Invoke();
        text.text += $" Grant Bonus";
    }

    public void CloseInterstitial()
    {
        Debug.Log($"Close Interstial Event");
        OnInterstialAdClosed?.Invoke();
    }
}
