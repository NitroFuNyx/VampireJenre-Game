using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyRewardsSO", menuName = "ScriptableObjects/DailyRewardsSO", order = 1)]
public class DailyRewardsSO : ScriptableObject
{
    [Header("Rewards")]
    [Space]
    [SerializeField] private List<RewardDataStruct> rewardsLists = new List<RewardDataStruct>();

    public List<RewardDataStruct> RewardsLists { get => rewardsLists; }
}
