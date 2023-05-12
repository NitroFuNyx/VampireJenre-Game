using UnityEngine;

public class RewardsManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [Space]
    [SerializeField] private DailyRewardsSO dailyRewardsSO;
    [Header("Sprite")]
    [Space]
    [SerializeField] private Sprite coinsSprite;
    [SerializeField] private Sprite gemsSprite;

    public DailyRewardsSO DailyRewardsSO { get => dailyRewardsSO; }
    public Sprite CoinsSprite { get => coinsSprite; }
    public Sprite GemsSprite { get => gemsSprite; }
}
