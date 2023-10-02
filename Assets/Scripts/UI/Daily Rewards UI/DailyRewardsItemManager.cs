using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardsItemManager : MonoBehaviour
{
    [SerializeField] private DailyRewardItem []rewardItems;
    [SerializeField] private int[] rewardAmount;
    [SerializeField] private Image rewardImage;
    private Dictionary<DailyRewards, Image> rewardsImages;

    private void Start()
    {
        if(rewardItems.Length != rewardAmount.Length)
            Debug.LogWarning("Different size of items and rewards!!!");
        
        for (int i = 0; i < rewardItems.Length; i++)
        {
            rewardItems[i].RewardAmount = rewardAmount[i];
        }
    }

    public void SelectCurrentDay(int day)
    {
        Debug.Log($"Days amount {day}");
        if(day>=7)
        {
            Debug.LogWarning("Wrong day data");
            return;
        }
        rewardItems[day].SetCurrentDayFrame();
        Debug.Log($"current day {day} ");
        for(int i=0 ;i<7;i++)
        {
           
            if(i<day)
            {
                
                rewardItems[i].ChangeStatus(true);
                Debug.Log($"Set passed day {i}");
            }
        }
    }

    private void SetTheRewardImage(DailyRewards reward)
    {
        rewardImage = rewardsImages[reward];
    }
}
