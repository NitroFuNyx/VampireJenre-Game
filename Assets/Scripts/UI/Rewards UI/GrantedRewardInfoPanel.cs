using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class GrantedRewardInfoPanel : PanelActivationManager
{
    [Header("Images")]
    [Space]
    [SerializeField] private Image rewardImage;
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI rewardAmountText;

    private RewardsManager _rewardsManager;
    private RewardWheelSpinner _rewardWheelSpinner;

    private void Start()
    {
        HidePanel();

        _rewardWheelSpinner.OnRewardDefined += RewardDefined_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _rewardWheelSpinner.OnRewardDefined -= RewardDefined_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(RewardsManager rewardsManager, RewardWheelSpinner rewardWheelSpinner)
    {
        _rewardWheelSpinner = rewardWheelSpinner;
        _rewardsManager = rewardsManager;
    }
    #endregion Zenject

    private void RewardDefined_ExecuteReaction(RewardObject rewardObject)
    {
        RewardDataStruct currentRewardData = _rewardsManager.GetRewardData(rewardObject);

        rewardAmountText.text = $"+ {currentRewardData.ResourceAmount}";

        if (currentRewardData.resourceType == ResourcesTypes.Coins)
        {
            rewardImage.sprite = _rewardsManager.DailyRewardsSO.CoinsSprite;
        }
        else
        {
            rewardImage.sprite = _rewardsManager.DailyRewardsSO.GemsSprite;
        }

        ShowPanel();
    }
}
