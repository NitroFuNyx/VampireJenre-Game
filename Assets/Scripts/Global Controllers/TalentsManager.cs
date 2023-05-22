using UnityEngine;
using System;
using System.Collections.Generic;
using Zenject;

public class TalentsManager : MonoBehaviour
{
    [Header("Cost Data")]
    [Space]
    [SerializeField] private int talentCostAmount = 100;
    [Header("Talents Data")]
    [Space]
    [SerializeField] private TalentsWheelDataSO talentsWheelDataSO;
    [SerializeField] private List<TalentLevelStruct> talentsLevelsList_PersistentInGame = new List<TalentLevelStruct>();
    [SerializeField] private List<TalentLevelStruct> talentsLevelsList_TemporaryInMap = new List<TalentLevelStruct>();

    private ResourcesManager _resourcesManager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;
    private TalentWheel _talentWheel;
    private TalentBoughtInfoPanel _talentBoughtInfoPanel;
    private DataPersistanceManager _dataPersistanceManager;

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
    private void Construct(ResourcesManager resourcesManager, PlayerCharacteristicsManager playerCharacteristicsManager, TalentWheel talentWheel, 
                           TalentBoughtInfoPanel talentBoughtInfoPanel, DataPersistanceManager dataPersistanceManager)
    {
        _resourcesManager = resourcesManager;
        _talentWheel = talentWheel;
        _playerCharacteristicsManager = playerCharacteristicsManager;
        _talentBoughtInfoPanel = talentBoughtInfoPanel;
        _dataPersistanceManager = dataPersistanceManager;
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
        TalentDataStruct currentTalentData = GetTalentData(talentItem);

        _playerCharacteristicsManager.UpgradePlayerDataWithSaving(currentTalentData);
        OnBuyingProcessFinishedCallback?.Invoke();

        talentsWheelDataSO.TalentsLevelsList[(int)currentTalentData.talentIndex]++;
        _talentBoughtInfoPanel.ShowPanelWithTalentData();
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
