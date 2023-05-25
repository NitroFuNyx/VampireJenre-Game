using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;

public class SkillsManager : MonoBehaviour
{
    [Header("Free Skills Data")]
    [Space]
    [SerializeField] private List<ActiveSkills> activeSkillsList_Free = new List<ActiveSkills>();
    [SerializeField] private List<PassiveSkills> passiveSkillsList_Free = new List<PassiveSkills>();
    [Header("Taken Skills Data")]
    [Space]
    [SerializeField] private List<ActiveSkills> activeSkillsList_Taken = new List<ActiveSkills>();
    [SerializeField] private List<PassiveSkills> passiveSkillsList_Taken = new List<PassiveSkills>();
    [Header("Skills Display Data")]
    [Space]
    [SerializeField] private SkillsDisplayDataSO skillsDisplayDataSO;

    private GameProcessManager _gameProcessManager;
    private GameLevelUI _gameLevelUI;
    private TakenSkillsDisplayPanel _takenSkillsDisplayPanel;

    private int skillsOptionsPerLevel = 3;

    #region Events Declaration
    public event Action<int, int> OnSkillToUpgradeDefined;
    #endregion Events Declaration

    private void Awake()
    {
        ResetSkillsLists();
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
    private void Construct(GameProcessManager gameProcessManager, GameLevelUI gameLevelUI, TakenSkillsDisplayPanel takenSkillsDisplayPanel)
    {
        _gameProcessManager = gameProcessManager;
        _gameLevelUI = gameLevelUI;
        _takenSkillsDisplayPanel = takenSkillsDisplayPanel;
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
            if(activeSkillsList_Free.Contains(activeSkill))
            {
                activeSkillsList_Taken.Add(activeSkill);
            }

            skillSprite = GetActiveSkillDisplayData(activeSkill).skillSprite;
        }
        else
        {
            PassiveSkills passiveSkill = (PassiveSkills)skillSubCategoryIndex;
            if (passiveSkillsList_Free.Contains(passiveSkill))
            {
                passiveSkillsList_Taken.Add(passiveSkill);
            }

            skillSprite = GetPassiveSkillDisplayData(passiveSkill).skillSprite;
        }

        _takenSkillsDisplayPanel.SkillTaken_ExecuteReaction(skillTypeIndex, skillSprite);
    }

    private void SubscribeOnEvents()
    {
        _gameProcessManager.OnGameStarted += GameProcessManager_OnGameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost += GameProcessManager_OnPlayerLost_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _gameProcessManager.OnGameStarted -= GameProcessManager_OnGameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost -= GameProcessManager_OnPlayerLost_ExecuteReaction;
    }

    private void ChooseFirstSkill()
    {
        List<UpgradeSkillData> upgradeSkillsDataList = new List<UpgradeSkillData>();

        for(int i = 0; i < skillsOptionsPerLevel; i++)
        {
            int randomSkillIndex = UnityEngine.Random.Range(0, activeSkillsList_Free.Count);
            ActiveSkills skill = activeSkillsList_Free[randomSkillIndex];
            activeSkillsList_Free.Remove(skill);
            activeSkillsList_Taken.Add(skill);

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
        for(int i = 0; i < activeSkillsList_Taken.Count; i++)
        {
            activeSkillsList_Free.Add(activeSkillsList_Taken[i]);
        }
        activeSkillsList_Taken.Clear();
    }

    private void ChooseSkillToUpgrade()
    {
        List<UpgradeSkillData> upgradeSkillsDataList = new List<UpgradeSkillData>();
    }

    private void ResetSkillsLists()
    {
        activeSkillsList_Free.Clear();
        passiveSkillsList_Free.Clear();

        activeSkillsList_Taken.Clear();
        passiveSkillsList_Taken.Clear();

        for (int i = 0; i < System.Enum.GetValues(typeof(ActiveSkills)).Length; i++)
        {
            ActiveSkills activeSkill = (ActiveSkills)i;
            activeSkillsList_Free.Add(activeSkill);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(PassiveSkills)).Length; i++)
        {
            PassiveSkills passiveSkill = (PassiveSkills)i;
            passiveSkillsList_Free.Add(passiveSkill);
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
