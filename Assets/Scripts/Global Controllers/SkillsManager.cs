using System.Collections;
using System.Collections.Generic;
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

    private int skillsOptionsPerLevel = 3;

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
    private void Construct(GameProcessManager gameProcessManager, GameLevelUI gameLevelUI)
    {
        _gameProcessManager = gameProcessManager;
        _gameLevelUI = gameLevelUI;
    }
    #endregion Zenject

    private void SubscribeOnEvents()
    {
        _gameProcessManager.OnGameStarted += GameProcessManager_OnGameStarted_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _gameProcessManager.OnGameStarted -= GameProcessManager_OnGameStarted_ExecuteReaction;
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
        }

        _gameLevelUI.ShowUpgradePanel(upgradeSkillsDataList);
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
