using System;
using UnityEngine;

public class AdsController : MonoBehaviour
{
    public event Action LoadInterstitialAd;
    public event Action<Action> LoadRewardedAd;
    public event Action OnRewardAdViewed;
    public event Action LoadBannerAd;
    public event Action DeleteBannerAd;

    public void LoadInterstitial()
    {
        LoadInterstitialAd?.Invoke();
    }
    public void LoadRewarded()
    {
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
        OnRewardAdViewed?.Invoke();
    }

    public void CloseInterstitial()
    {
        
    }
}
