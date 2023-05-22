using UnityEngine;
using System;
using System.Collections.Generic;
using Zenject;

public class TalentsManager : MonoBehaviour, IDataPersistance
{
    [Header("Cost Data")]
    [Space]
    [SerializeField] private int talentCostAmount = 100;
    [Header("Talents Data")]
    [Space]
    [SerializeField] private TalentsWheelDataSO talentsWheelDataSO;
    [SerializeField] private List<TalentLevelStruct> talentsLevelsList = new List<TalentLevelStruct>();

    private ResourcesManager _resourcesManager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;
    private TalentWheel _talentWheel;
    private TalentBoughtInfoPanel _talentBoughtInfoPanel;
    private DataPersistanceManager _dataPersistanceManager;

    private Action OnBuyingProcessFinishedCallback;

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

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

    #region Save/Load Methods
    public void LoadData(GameData data)
    {
        for(int i = 0; i < data.skillsLevelsList.Count; i++)
        {
            talentsLevelsList.Add(data.skillsLevelsList[i]);
        }
    }

    public void SaveData(GameData data)
    {
        for (int i = 0; i < talentsLevelsList.Count; i++)
        {
            data.skillsLevelsList[i] = talentsLevelsList[i];
        }
    }
    #endregion Save/Load Methods

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

    public void InitializeTalentsLevelsData(GameData gameData)
    {
        for(int i = 0; i < talentsWheelDataSO.TalentsLists.Count; i++)
        {
            TalentLevelStruct talentLevelData = new TalentLevelStruct();
            talentLevelData.passiveSkillType = talentsWheelDataSO.TalentsLists[i].passiveSkillType;
            talentLevelData.level = 1;
            gameData.skillsLevelsList.Add(talentLevelData);
        }
    }

    private void TalentToUpgradeDefined_ExecuteReaction(TalentItem talentItem)
    {
        TalentDataStruct currentTalentData = GetTalentData(talentItem);

        for(int i = 0; i < talentsLevelsList.Count; i++)
        {
            if(talentsLevelsList[i].passiveSkillType == currentTalentData.passiveSkillType)
            {
                TalentLevelStruct newTalentLevelStruct = new TalentLevelStruct();
                newTalentLevelStruct.passiveSkillType = talentsLevelsList[i].passiveSkillType;
                newTalentLevelStruct.level = talentsLevelsList[i].level + 1;
                talentsLevelsList[i] = newTalentLevelStruct;
                Debug.Log($"Talent {talentsLevelsList[i].passiveSkillType} Preveous {talentsLevelsList[i].level - 1} New {talentsLevelsList[i].level}");
                _talentBoughtInfoPanel.ShowPanelWithTalentData(currentTalentData, talentsLevelsList[i].level);
                break;
            }
        }

        _playerCharacteristicsManager.UpgradePlayerDataWithSaving(currentTalentData);
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
