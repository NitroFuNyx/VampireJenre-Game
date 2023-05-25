using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;

public class SkillsManager : MonoBehaviour
{
    [Header("General Skills Data")]
    [Space]
    [SerializeField] private List<ActiveSkills> allActiveSkillsList = new List<ActiveSkills>();
    [SerializeField] private List<PassiveSkills> allPassiveSkillsList = new List<PassiveSkills>();
    [Header("Taken Skills Data")]
    [Space]
    [SerializeField] private List<ActiveSkillInGameDataStruct> activeSkillsTakenList = new List<ActiveSkillInGameDataStruct>();
    [SerializeField] private List<PassiveSkillInGameDataStruct> passiveSkillsTakenList = new List<PassiveSkillInGameDataStruct>();
    [Header("Skills Display Data")]
    [Space]
    [SerializeField] private SkillsDisplayDataSO skillsDisplayDataSO;

    private GameProcessManager _gameProcessManager;
    private GameLevelUI _gameLevelUI;
    private TakenSkillsDisplayPanel _takenSkillsDisplayPanel;
    private PlayerExperienceManager _playerExperienceManager;

    private int skillsOptionsPerLevel = 3;

    #region Events Declaration
    public event Action<int, int> OnSkillToUpgradeDefined;
    #endregion Events Declaration

    private void Awake()
    {
        FillAvailableSkillsLists();
    }

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(GameProcessManager gameProcessManager, GameLevelUI gameLevelUI, TakenSkillsDisplayPanel takenSkillsDisplayPanel, PlayerExperienceManager playerExperienceManager)
    {
        _gameProcessManager = gameProcessManager;
        _gameLevelUI = gameLevelUI;
        _takenSkillsDisplayPanel = takenSkillsDisplayPanel;
        _playerExperienceManager = playerExperienceManager;
    }
    #endregion Zenject

    public void DefineSkillToUpgrade(int skillTypeIndex, int skillSubCategoryIndex)
    {
        OnSkillToUpgradeDefined?.Invoke(skillTypeIndex, skillSubCategoryIndex);
        _gameLevelUI.HideUpgradePanel();

        Sprite skillSprite = null;

        if((SkillBasicTypes)skillTypeIndex == SkillBasicTypes.Active)
        {
            ActiveSkills activeSkill = (ActiveSkills)skillSubCategoryIndex;

            int skillInUseDataIndex = -1;

            for (int i = 0; i < activeSkillsTakenList.Count; i++)
            {
                if (activeSkillsTakenList[i].skillType == activeSkill)
                {
                    skillInUseDataIndex = i;
                }
            }

            if(skillInUseDataIndex == -1) // there is no current skill in use
            {
                ActiveSkillInGameDataStruct inGameSkillData = new ActiveSkillInGameDataStruct();
                inGameSkillData.skillType = activeSkill;
                inGameSkillData.skillLevel = 1;
                activeSkillsTakenList.Add(inGameSkillData);
            }
            else
            {
                ActiveSkillInGameDataStruct tempSkillData = new ActiveSkillInGameDataStruct();
                tempSkillData.skillType = activeSkillsTakenList[skillInUseDataIndex].skillType;
                tempSkillData.skillLevel = activeSkillsTakenList[skillInUseDataIndex].skillLevel + 1;
                activeSkillsTakenList[skillInUseDataIndex] = tempSkillData;
            }

            skillSprite = GetActiveSkillDisplayData(activeSkill).skillSprite;
        }
        else
        {
            PassiveSkills passiveSkill = (PassiveSkills)skillSubCategoryIndex;

            int skillInUseDataIndex = -1;

            for (int i = 0; i < passiveSkillsTakenList.Count; i++)
            {
                if (passiveSkillsTakenList[i].skillType == passiveSkill)
                {
                    skillInUseDataIndex = i;
                }
            }

            if (skillInUseDataIndex == -1) // there is no current skill in use
            {
                PassiveSkillInGameDataStruct inGameSkillData = new PassiveSkillInGameDataStruct();
                inGameSkillData.skillType = passiveSkill;
                inGameSkillData.skillLevel = 1;
                passiveSkillsTakenList.Add(inGameSkillData);
            }
            else
            {
                PassiveSkillInGameDataStruct tempSkillData = new PassiveSkillInGameDataStruct();
                tempSkillData.skillType = passiveSkillsTakenList[skillInUseDataIndex].skillType;
                tempSkillData.skillLevel = passiveSkillsTakenList[skillInUseDataIndex].skillLevel + 1;
                passiveSkillsTakenList[skillInUseDataIndex] = tempSkillData;
            }

            skillSprite = GetPassiveSkillDisplayData(passiveSkill).skillSprite;
        }

        _takenSkillsDisplayPanel.SkillTaken_ExecuteReaction(skillTypeIndex, skillSprite);
    }

    private void SubscribeOnEvents()
    {
        _gameProcessManager.OnGameStarted += GameProcessManager_OnGameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost += GameProcessManager_OnPlayerLost_ExecuteReaction;

        _playerExperienceManager.OnPlayerGotNewLevel += PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _gameProcessManager.OnGameStarted -= GameProcessManager_OnGameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost -= GameProcessManager_OnPlayerLost_ExecuteReaction;

        _playerExperienceManager.OnPlayerGotNewLevel -= PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction;
    }

    private void ChooseFirstSkill()
    {
        List<UpgradeSkillData> upgradeSkillsDataList = new List<UpgradeSkillData>();

        List<ActiveSkills> activeSkillsAvailableList = new List<ActiveSkills>();
        List<ActiveSkills> activeSkillsOprionsForUpgradeList = new List<ActiveSkills>();

        for(int i = 0; i < allActiveSkillsList.Count; i++)
        {
            activeSkillsAvailableList.Add(allActiveSkillsList[i]);
        }

        for(int i = 0; i < skillsOptionsPerLevel; i++)
        {
            int randomSkillIndex = UnityEngine.Random.Range(0, activeSkillsAvailableList.Count);
            ActiveSkills skill = activeSkillsAvailableList[randomSkillIndex];
            activeSkillsAvailableList.Remove(skill);
            activeSkillsOprionsForUpgradeList.Add(skill);

            UpgradeSkillData upgradeSkillData = new UpgradeSkillData();
            upgradeSkillData.SkillType = SkillBasicTypes.Active;
            upgradeSkillData.ActiveSkill = skill;
            upgradeSkillData.SkillLevelString = $"1";

            ActiveSkillsDisplayDataStruct skillDisplayData = GetActiveSkillDisplayData(skill);

            upgradeSkillData.SkillNameString = $"{skillDisplayData.skillName}";
            upgradeSkillData.SkillSprite = skillDisplayData.skillSprite;
            upgradeSkillsDataList.Add(upgradeSkillData);
        }

        _gameLevelUI.ShowUpgradePanel(upgradeSkillsDataList);
    }

    private void ChooseRandomSkillToUpgrade()
    {
        List<UpgradeSkillData> upgradeSkillsDataList = new List<UpgradeSkillData>();
    }

    private void FillAvailableSkillsLists()
    {
        allActiveSkillsList.Clear();
        allPassiveSkillsList.Clear();

        for (int i = 0; i < System.Enum.GetValues(typeof(ActiveSkills)).Length; i++)
        {
            ActiveSkills activeSkill = (ActiveSkills)i;
            allActiveSkillsList.Add(activeSkill);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(PassiveSkills)).Length; i++)
        {
            PassiveSkills passiveSkill = (PassiveSkills)i;
            allPassiveSkillsList.Add(passiveSkill);
        }
    }

    private void FillAvailableForUpgradeSkillsLists()
    {
        for(int i = 0; i < allActiveSkillsList.Count; i++)
        {

        }
    }

    private void GameProcessManager_OnGameStarted_ExecuteReaction()
    {
        ChooseFirstSkill();
    }

    private void GameProcessManager_OnPlayerLost_ExecuteReaction()
    {
        _takenSkillsDisplayPanel.ResetSkillsData();
    }

    private void PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction()
    {
        ChooseRandomSkillToUpgrade();
    }

    private ActiveSkillsDisplayDataStruct GetActiveSkillDisplayData(ActiveSkills skill)
    {
        ActiveSkillsDisplayDataStruct skillData = new ActiveSkillsDisplayDataStruct();

        for(int i = 0; i < skillsDisplayDataSO.ActiveSkillsDisplayDataList.Count; i++)
        {
            if(skillsDisplayDataSO.ActiveSkillsDisplayDataList[i].skill == skill)
            {
                skillData = skillsDisplayDataSO.ActiveSkillsDisplayDataList[i];
                break;
            }
        }

        return skillData;
    }

    private PassiveSkillsDisplayDataStruct GetPassiveSkillDisplayData(PassiveSkills skill)
    {
        PassiveSkillsDisplayDataStruct skillData = new PassiveSkillsDisplayDataStruct();

        for (int i = 0; i < skillsDisplayDataSO.PassiveSkillsDisplayDataList.Count; i++)
        {
            if (skillsDisplayDataSO.PassiveSkillsDisplayDataList[i].skill == skill)
            {
                skillData = skillsDisplayDataSO.PassiveSkillsDisplayDataList[i];
                break;
            }
        }

        return skillData;
    }
}
