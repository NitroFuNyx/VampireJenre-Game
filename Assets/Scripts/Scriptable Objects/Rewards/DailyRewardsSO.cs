using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyRewardsSO", menuName = "ScriptableObjects/DailyRewardsSO", order = 1)]
public class DailyRewardsSO : ScriptableObject
{
    [Header("Rewards")]
    [Space]
    [SerializeField] private List<RewardDataStruct> rewardsLists = new List<RewardDataStruct>();
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite coinsSprite;
    [SerializeField] private Sprite gemsSprite;

    public List<RewardDataStruct> RewardsLists { get => rewardsLists; }
    public Sprite CoinsSprite { get => coinsSprite; }
    public Sprite GemsSprite { get => gemsSprite; }
}
