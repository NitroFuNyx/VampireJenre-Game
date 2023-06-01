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
    private PlayerCharacteristicsManager _playerCharacteristicsManager;
    private PickableItemsManager _pickableItemsManager;

    private List<ActiveSkills> activeSkillsUpgradedToMax = new List<ActiveSkills>();
    private List<PassiveSkills> passiveSkillsUpgradedToMax = new List<PassiveSkills>();

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
    private void Construct(GameProcessManager gameProcessManager, GameLevelUI gameLevelUI, TakenSkillsDisplayPanel takenSkillsDisplayPanel, 
                           PlayerExperienceManager playerExperienceManager, PlayerCharacteristicsManager playerCharacteristicsManager, PickableItemsManager pickableItemsManager)
    {
        _gameProcessManager = gameProcessManager;
        _gameLevelUI = gameLevelUI;
        _takenSkillsDisplayPanel = takenSkillsDisplayPanel;
        _playerExperienceManager = playerExperienceManager;
        _playerCharacteristicsManager = playerCharacteristicsManager;
        _pickableItemsManager = pickableItemsManager;
    }
    #endregion Zenject

    public void DefineSkillToUpgrade(int skillCategoryIndex, int skillIndex)
    {
        OnSkillToUpgradeDefined?.Invoke(skillCategoryIndex, skillIndex);
        _gameLevelUI.HideUpgradePanel();
        _gameLevelUI.HideSkillScrollInfoPanel();
        Debug.Log($"{(SkillBasicTypes)skillCategoryIndex} {skillIndex}");

        Sprite skillSprite = null;

        if((SkillBasicTypes)skillCategoryIndex == SkillBasicTypes.Active)
        {
            ActiveSkills activeSkill = (ActiveSkills)skillIndex;

            int skillInUseDataIndex = -1;

            for (int i = 0; i < activeSkillsTakenList.Count; i++)
            {
                if (activeSkillsTakenList[i].skillType == activeSkill)
                {
                    skillInUseDataIndex = i;
                }
            }

            if (skillInUseDataIndex == -1) // there is no current skill in use
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

                if (activeSkillsTakenList[skillInUseDataIndex].skillLevel == SkillsInGameValues.maxSkillLevel)
                {
                    activeSkillsUpgradedToMax.Add(activeSkillsTakenList[skillInUseDataIndex].skillType);
                }
            }

            skillSprite = GetActiveSkillDisplayData(activeSkill).skillSprite;
        }
        else
        {
            PassiveSkills passiveSkill = (PassiveSkills)skillIndex;

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

                if (passiveSkillsTakenList[skillInUseDataIndex].skillLevel == SkillsInGameValues.maxSkillLevel)
                {
                    passiveSkillsUpgradedToMax.Add(passiveSkillsTakenList[skillInUseDataIndex].skillType);
                }
            }

            skillSprite = GetPassiveSkillDisplayData(passiveSkill).skillSprite;
        }

        int upgradedSkillNewLevel;
        if(skillCategoryIndex == (int)SkillBasicTypes.Active)
        {
            upgradedSkillNewLevel = activeSkillsTakenList[activeSkillsTakenList.Count - 1].skillLevel;
            Debug.Log($"Skill {activeSkillsTakenList[activeSkillsTakenList.Count - 1].skillType} new level {activeSkillsTakenList[activeSkillsTakenList.Count - 1].skillLevel}");
        }
        else
        {
            upgradedSkillNewLevel = passiveSkillsTakenList[passiveSkillsTakenList.Count - 1].skillLevel;
            Debug.Log($"Skill {passiveSkillsTakenList[passiveSkillsTakenList.Count - 1].skillType} new level {passiveSkillsTakenList[passiveSkillsTakenList.Count - 1].skillLevel}");
        }
        _playerCharacteristicsManager.UpgradePlayerSkill(skillCategoryIndex, skillIndex, upgradedSkillNewLevel);
        _takenSkillsDisplayPanel.SkillTaken_ExecuteReaction(skillCategoryIndex, skillIndex, skillSprite);
    }

    private void SubscribeOnEvents()
    {
        _gameProcessManager.OnGameStarted += GameProcessManager_OnGameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost += GameProcessManager_OnPlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon += GameProcessManager_OnPlayerWon_ExecuteReaction;

        _playerExperienceManager.OnPlayerGotNewLevel += PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction;

        _pickableItemsManager.OnSkillScrollCollected += PickableItemsManager_SkillScrollCollected_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _gameProcessManager.OnGameStarted -= GameProcessManager_OnGameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost -= GameProcessManager_OnPlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon -= GameProcessManager_OnPlayerWon_ExecuteReaction;

        _playerExperienceManager.OnPlayerGotNewLevel -= PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction;

        _pickableItemsManager.OnSkillScrollCollected -= PickableItemsManager_SkillScrollCollected_ExecuteReaction;
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

        for(int i = 0; i < SkillsInGameValues.skillsOptionsForUpgradePerLevelAmount; i++)
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

    public List<UpgradeSkillData> GetRandomSkillsToUpgradeList(int optionsAmount)
    {
        List<UpgradeSkillData> upgradeSkillsDataList = new List<UpgradeSkillData>();

        List<ActiveSkills> activeSkillsAvailableList = new List<ActiveSkills>();
        List<ActiveSkills> activeSkillsOprionsForUpgradeList = new List<ActiveSkills>();

        List<PassiveSkills> passiveSkillsAvailableList = new List<PassiveSkills>();
        List<PassiveSkills> passiveSkillsOprionsForUpgradeList = new List<PassiveSkills>();

        activeSkillsAvailableList = GetCurrentlyAvailableForUpgradeActiveSkills();
        passiveSkillsAvailableList = GetCurrentlyAvailableForUpgradePassiveSkills();

        for (int i = 0; i < optionsAmount; i++)
        {
            int skillCategoryIndex = -1;

            if (activeSkillsAvailableList.Count > 0 && passiveSkillsAvailableList.Count > 0)
            {
                skillCategoryIndex = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(SkillBasicTypes)).Length);
            }
            else if (activeSkillsAvailableList.Count > 0 && passiveSkillsAvailableList.Count == 0)
            {
                // all passive skills upgraded
                skillCategoryIndex = (int)SkillBasicTypes.Active;
            }
            else if (activeSkillsAvailableList.Count == 0 && passiveSkillsAvailableList.Count > 0)
            {
                // all active skills upgraded
                skillCategoryIndex = (int)SkillBasicTypes.Passive;
            }
            else
            {
                // all skills upgraded
            }

            int randomSkillIndex;
            UpgradeSkillData upgradeSkillData = new UpgradeSkillData();

            if (skillCategoryIndex == (int)SkillBasicTypes.Active)
            {
                randomSkillIndex = UnityEngine.Random.Range(0, activeSkillsAvailableList.Count);

                ActiveSkills skill = activeSkillsAvailableList[randomSkillIndex];
                activeSkillsAvailableList.Remove(skill);
                activeSkillsOprionsForUpgradeList.Add(skill);

                upgradeSkillData.SkillType = SkillBasicTypes.Active;
                upgradeSkillData.ActiveSkill = skill;
                upgradeSkillData.SkillLevelString = $"1";

                for (int j = 0; j < activeSkillsTakenList.Count; j++)
                {
                    if (activeSkillsTakenList[j].skillType == skill)
                    {
                        upgradeSkillData.SkillLevelString = $"{activeSkillsTakenList[j].skillLevel + 1}";
                        break;
                    }
                }

                ActiveSkillsDisplayDataStruct skillDisplayData = GetActiveSkillDisplayData(skill);

                upgradeSkillData.SkillNameString = $"{skillDisplayData.skillName}";
                upgradeSkillData.SkillSprite = skillDisplayData.skillSprite;
            }
            else if (skillCategoryIndex == (int)SkillBasicTypes.Passive)
            {
                randomSkillIndex = UnityEngine.Random.Range(0, passiveSkillsAvailableList.Count);

                PassiveSkills skill = passiveSkillsAvailableList[randomSkillIndex];
                passiveSkillsAvailableList.Remove(skill);
                passiveSkillsOprionsForUpgradeList.Add(skill);

                upgradeSkillData.SkillType = SkillBasicTypes.Passive;
                upgradeSkillData.PassiveSkill = skill;
                upgradeSkillData.SkillLevelString = $"1";

                for (int j = 0; j < passiveSkillsTakenList.Count; j++)
                {
                    if (passiveSkillsTakenList[j].skillType == skill)
                    {
                        upgradeSkillData.SkillLevelString = $"{passiveSkillsTakenList[j].skillLevel + 1}";
                        break;
                    }
                }

                PassiveSkillsDisplayDataStruct skillDisplayData = GetPassiveSkillDisplayData(skill);

                upgradeSkillData.SkillNameString = $"{skillDisplayData.skillName}";
                upgradeSkillData.SkillSprite = skillDisplayData.skillSprite;
            }

            upgradeSkillsDataList.Add(upgradeSkillData);
        }

        return upgradeSkillsDataList;
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

    private void GameProcessManager_OnGameStarted_ExecuteReaction()
    {
        ChooseFirstSkill();
    }

    private void GameProcessManager_OnPlayerLost_ExecuteReaction()
    {
        _takenSkillsDisplayPanel.ResetSkillsData();
    }

    private void GameProcessManager_OnPlayerWon_ExecuteReaction()
    {
        _takenSkillsDisplayPanel.ResetSkillsData();
    }

    private void PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction()
    {
        _gameLevelUI.ShowUpgradePanel(GetRandomSkillsToUpgradeList(SkillsInGameValues.skillsOptionsForUpgradePerLevelAmount));
    }

    private void PickableItemsManager_SkillScrollCollected_ExecuteReaction()
    {
        _gameLevelUI.ShowSkillScrollInfoPanel(GetRandomSkillsToUpgradeList(1));
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

    private List<ActiveSkills> GetCurrentlyAvailableForUpgradeActiveSkills()
    {
        List<ActiveSkills> availableSkillsList = new List<ActiveSkills>();

        for (int i = 0; i < allActiveSkillsList.Count; i++)
        {
            availableSkillsList.Add(allActiveSkillsList[i]);
        }

        for (int i = 0; i < activeSkillsTakenList.Count; i++)
        {
            if (activeSkillsTakenList[i].skillLevel == SkillsInGameValues.maxSkillLevel && availableSkillsList.Contains(activeSkillsTakenList[i].skillType))
            {
                availableSkillsList.Remove(activeSkillsTakenList[i].skillType);
            }
        }

        if (activeSkillsTakenList.Count == SkillsInGameValues.maxSkillsInCategoryAmount)
        {
            List<ActiveSkills> skillsToRemoveFromAvailableList = new List<ActiveSkills>();
            List<ActiveSkills> skillsTaken = new List<ActiveSkills>();

            for (int i = 0; i < activeSkillsTakenList.Count; i++)
            {
                skillsTaken.Add(activeSkillsTakenList[i].skillType);
            }

            for (int i = 0; i < availableSkillsList.Count; i++)
            {
                if (!skillsTaken.Contains(availableSkillsList[i]))
                {
                    skillsToRemoveFromAvailableList.Add(availableSkillsList[i]);
                }
            }

            for (int i = 0; i < skillsToRemoveFromAvailableList.Count; i++)
            {
                availableSkillsList.Remove(skillsToRemoveFromAvailableList[i]);
            }
        }

        return availableSkillsList;
    }

    private List<PassiveSkills> GetCurrentlyAvailableForUpgradePassiveSkills()
    {
        List<PassiveSkills> availableSkillsList = new List<PassiveSkills>();

        for (int i = 0; i < allPassiveSkillsList.Count; i++)
        {
            availableSkillsList.Add(allPassiveSkillsList[i]);
        }

        for (int i = 0; i < passiveSkillsTakenList.Count; i++)
        {
            if (passiveSkillsTakenList[i].skillLevel == SkillsInGameValues.maxSkillLevel && availableSkillsList.Contains(passiveSkillsTakenList[i].skillType))
            {
                availableSkillsList.Remove(passiveSkillsTakenList[i].skillType);
            }
        }

        if (passiveSkillsTakenList.Count == SkillsInGameValues.maxSkillsInCategoryAmount)
        {
            List<PassiveSkills> skillsToRemoveFromAvailableList = new List<PassiveSkills>();
            List<PassiveSkills> skillsTaken = new List<PassiveSkills>();

            for (int i = 0; i < passiveSkillsTakenList.Count; i++)
            {
                skillsTaken.Add(passiveSkillsTakenList[i].skillType);
            }

            for (int i = 0; i < availableSkillsList.Count; i++)
            {
                if (!skillsTaken.Contains(availableSkillsList[i]))
                {
                    skillsToRemoveFromAvailableList.Add(availableSkillsList[i]);
                }
            }

            for (int i = 0; i < skillsToRemoveFromAvailableList.Count; i++)
            {
                availableSkillsList.Remove(skillsToRemoveFromAvailableList[i]);
            }
        }

        return availableSkillsList;
    }
}
