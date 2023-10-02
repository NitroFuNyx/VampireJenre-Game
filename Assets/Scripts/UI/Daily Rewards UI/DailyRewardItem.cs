using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardItem : MonoBehaviour
{
    [SerializeField] private Image rewardStatus;
    [SerializeField] private Image currentDayFrame;
    [SerializeField] private DailyRewards reward;
    [SerializeField] private int rewardAmount;

    public int RewardAmount
    {
        get => rewardAmount;
        set => rewardAmount = value;
    }

    public void ChangeStatus(bool status)
    {
        if (status)
        {
            var color = rewardStatus.color;
            color.a = 1;
            rewardStatus.color = color;
        }
        else
        {
            var color = rewardStatus.color;
            color.a = 0;
            rewardStatus.color = color;
        }
    }

    public void SetCurrentDayFrame()
    {
        var color = currentDayFrame.color;
        color.a = 1;
        currentDayFrame.color = color;
    }

    public void GetAReward()
    {
        Debug.Log($"Rewarded with {rewardAmount} of {reward.ToString()}");
    }
    

}
