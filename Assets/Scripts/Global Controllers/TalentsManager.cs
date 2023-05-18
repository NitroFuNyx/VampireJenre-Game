using UnityEngine;
using System;
using Zenject;

public class TalentsManager : MonoBehaviour
{
    [Header("Cost Data")]
    [Space]
    [SerializeField] private int talentCostAmount = 100;
    [Header("Talents Data")]
    [Space]
    [SerializeField] private TalentsWheelDataSO talentsWheelDataSO;

    private ResourcesManager _resourcesManager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;
    private TalentWheel _talentWheel;

    private Action OnBuyingProcessFinishedCallback;

    private void Start()
    {
        _talentWheel.OnTalentToUpgradeDefined += TalentToUpgradeDefined_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _talentWheel.OnTalentToUpgradeDefined -= TalentToUpgradeDefined_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager, PlayerCharacteristicsManager playerCharacteristicsManager, TalentWheel talentWheel)
    {
        _resourcesManager = resourcesManager;
        _talentWheel = talentWheel;
        _playerCharacteristicsManager = playerCharacteristicsManager;
    }
    #endregion Zenject

    public void BuyTalent(Action OnBuyingProcessLaunced, Action OnBuyingProcessFinished, Action OnBuyingProcessCanceled)
    {
        if(_resourcesManager.CoinsAmount >= talentCostAmount)
        {
            OnBuyingProcessLaunced?.Invoke();
            OnBuyingProcessFinishedCallback = OnBuyingProcessFinished;
            _resourcesManager.DecreaseCoinsAmount(talentCostAmount);
            _talentWheel.StartWheel();
        }
        else
        {
            OnBuyingProcessCanceled?.Invoke();
        }
    }

    private void TalentToUpgradeDefined_ExecuteReaction(TalentItem talentItem)
    {
        _playerCharacteristicsManager.UpgradePlayerDataWithSaving(GetTalentData(talentItem));
        OnBuyingProcessFinishedCallback?.Invoke();
    }

    private TalentDataStruct GetTalentData(TalentItem talentItem)
    {
        TalentDataStruct targetTalentData = new TalentDataStruct();

        for(int i = 0; i < talentsWheelDataSO.TalentsLists.Count; i++)
        {
            if(talentItem.TalentIndex == talentsWheelDataSO.TalentsLists[i].talentIndex)
            {
                targetTalentData = talentsWheelDataSO.TalentsLists[i];
                break;
            }
        }

        return targetTalentData;
    }
}
