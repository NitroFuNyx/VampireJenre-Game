using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class RewardObject : MonoBehaviour
{
    [Header("Reward Data")]
    [Space]
    [SerializeField] private RewardIndexes rewardIndex;
    [Header("Internal References")]
    [Space]
    [SerializeField] private Image resourceImage;
    [SerializeField] private TextMeshProUGUI resourceAmountText;

    private RewardsManager _rewardsManager;

    public RewardIndexes RewardIndex { get => rewardIndex; }

    #region Zenject
    [Inject]
    private void Construct(RewardsManager rewardsManager)
    {
        _rewardsManager = rewardsManager;
    }
    #endregion Zenject

    private void Start()
    {
        SetRewardUIData();
    }

    private void SetRewardUIData()
    {
        for(int i = 0; i < _rewardsManager.DailyRewardsSO.RewardsLists.Count; i++)
        {
            if(rewardIndex == _rewardsManager.DailyRewardsSO.RewardsLists[i].rewardIndex)
            {
                SetResourceImage(_rewardsManager.DailyRewardsSO.RewardsLists[i]);
                resourceAmountText.text = $"{_rewardsManager.DailyRewardsSO.RewardsLists[i].ResourceAmount}";
            }
        }
    }

    private void SetResourceImage(RewardDataStruct rewardData)
    {
        if(rewardData.resourceType == ResourcesTypes.Coins)
        {
            resourceImage.sprite = _rewardsManager.DailyRewardsSO.CoinsSprite;
        }
        else
        {
            resourceImage.sprite = _rewardsManager.DailyRewardsSO.GemsSprite;
        }
    }
}
