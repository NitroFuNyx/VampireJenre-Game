using UnityEngine;
using System.Collections;
using Zenject;

public class RewardsManager : MonoBehaviour, IDataPersistance
{
    [Header("Scriptable Objects")]
    [Space]
    [SerializeField] private DailyRewardsSO dailyRewardsSO;

    private RewardWheelSpinner _rewardWheelSpinner;
    private ResourcesManager _resourcesManager;
    private DataPersistanceManager _dataPersistanceManager;
    private SystemTimeManager _systemTimeManager;

    [SerializeField] private bool freeRewardSpinUsed = false;
    [SerializeField] private bool rewardForAdSpinUsed = false;

    public DailyRewardsSO DailyRewardsSO { get => dailyRewardsSO; }
    public bool FreeRewardSpinUsed { get => freeRewardSpinUsed;}
    public bool RewardForAdSpinUsed { get => rewardForAdSpinUsed;}

    public event System.Action OnSpinButtonUpdateRequired;

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

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
    private void Construct(RewardWheelSpinner rewardWheelSpinner, ResourcesManager resourcesManager,
                           DataPersistanceManager dataPersistanceManager, SystemTimeManager systemTimeManager)
    {
        _rewardWheelSpinner = rewardWheelSpinner;
        _resourcesManager = resourcesManager;
        _dataPersistanceManager = dataPersistanceManager;
        _systemTimeManager = systemTimeManager;
    }
    #endregion Zenject

    #region Save/Load Methods
    public void LoadData(GameData data)
    {
        freeRewardSpinUsed = data.freeRewardSpinUsed;
        rewardForAdSpinUsed = data.rewardForAdSpinUsed;

        StartCoroutine(SetRewardsSpinDataCoroutine());
    }

    public void SaveData(GameData data)
    {
        data.freeRewardSpinUsed = freeRewardSpinUsed;
        data.rewardForAdSpinUsed = rewardForAdSpinUsed;
    }
    #endregion Save/Load Methods

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

    public void UseFreeRewardSpin()
    {
        freeRewardSpinUsed = true;
        _dataPersistanceManager.SaveGame();
    }

    public void UseRewardSpinForAd()
    {
        rewardForAdSpinUsed = true;
        _dataPersistanceManager.SaveGame();
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

    private IEnumerator SetRewardsSpinDataCoroutine()
    {
        yield return null;

        if(_systemTimeManager.NewDay)
        {
            freeRewardSpinUsed = false;
            rewardForAdSpinUsed = false;
            _dataPersistanceManager.SaveGame();
        }
        else
        {
            if(freeRewardSpinUsed || rewardForAdSpinUsed)
            {
                OnSpinButtonUpdateRequired?.Invoke();
            }
        }
    }
}
