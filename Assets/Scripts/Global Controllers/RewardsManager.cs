using UnityEngine;
using Zenject;

public class RewardsManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [Space]
    [SerializeField] private DailyRewardsSO dailyRewardsSO;

    private RewardWheelSpinner _rewardWheelSpinner;
    private ResourcesManager _resourcesManager;
    private DataPersistanceManager _dataPersistanceManager;

    public DailyRewardsSO DailyRewardsSO { get => dailyRewardsSO; }

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
    private void Construct(RewardWheelSpinner rewardWheelSpinner, ResourcesManager resourcesManager, DataPersistanceManager dataPersistanceManager)
    {
        _rewardWheelSpinner = rewardWheelSpinner;
        _resourcesManager = resourcesManager;
        _dataPersistanceManager = dataPersistanceManager;
    }
    #endregion Zenject

    public RewardDataStruct GetRewardData(RewardObject rewardObject)
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

    private void RewardDefined_ExecuteReaction(RewardObject rewardObject)
    {
        RewardDataStruct currentRewardData = GetRewardData(rewardObject);

        if(currentRewardData.resourceType == ResourcesTypes.Coins)
        {
            _resourcesManager.IncreaseCoinsAmount(currentRewardData.ResourceAmount);
        }
        else
        {
            _resourcesManager.IncreaseGemsAmount(currentRewardData.ResourceAmount);
        }

        _dataPersistanceManager.SaveGame();
    }
}
