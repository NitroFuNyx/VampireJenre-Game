using UnityEngine;
using Zenject;

public class RewardsManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [Space]
    [SerializeField] private DailyRewardsSO dailyRewardsSO;
    [Header("Sprite")]
    [Space]
    [SerializeField] private Sprite coinsSprite;
    [SerializeField] private Sprite gemsSprite;

    private RewardWheelSpinner _rewardWheelSpinner;
    private ResourcesManager _resourcesManager;

    public DailyRewardsSO DailyRewardsSO { get => dailyRewardsSO; }
    public Sprite CoinsSprite { get => coinsSprite; }
    public Sprite GemsSprite { get => gemsSprite; }

    private void Start()
    {
        _rewardWheelSpinner.OnRewardDefined += RewardDefined_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _rewardWheelSpinner.OnRewardDefined -= RewardDefined_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(RewardWheelSpinner rewardWheelSpinner, ResourcesManager resourcesManager)
    {
        _rewardWheelSpinner = rewardWheelSpinner;
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    private void RewardDefined_ExecuteReaction(RewardObject rewardObject)
    {
        RewardDataStruct currentRewardData = GetRewardData(rewardObject);

        if(currentRewardData.resourceType == ResourcesTypes.Coins)
        {
            _resourcesManager.IncreaseCoinsAmount(currentRewardData.ResourceAmount);
        }
    }
    
    private RewardDataStruct GetRewardData(RewardObject rewardObject)
    {
        RewardDataStruct targetRewardData = new RewardDataStruct();

        for (int i = 0; i < dailyRewardsSO.RewardsLists.Count; i++)
        {
            if (rewardObject.RewardIndex == dailyRewardsSO.RewardsLists[i].rewardIndex)
            {
                targetRewardData = dailyRewardsSO.RewardsLists[i];
                break;
            }
        }

        return targetRewardData;
    }
}
