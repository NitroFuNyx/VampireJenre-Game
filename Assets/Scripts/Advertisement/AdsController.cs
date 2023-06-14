using System;
using UnityEngine;

public class AdsController : MonoBehaviour
{
    public event Action LoadInterstitialAd;
    public event Action LoadRewardedAd;
    public event Action LoadBannerAd;
    public event Action DeleteBannerAd;

    public void LoadInterstitial()
    {
        LoadInterstitialAd?.Invoke();
    }
    public void LoadRewarded()
    {
        LoadRewardedAd?.Invoke();
    }
    public void LoadBanner()
    {
        LoadBannerAd?.Invoke();
    }
    public void DestroyBanner()
    {
        DeleteBannerAd?.Invoke();
    }
}
