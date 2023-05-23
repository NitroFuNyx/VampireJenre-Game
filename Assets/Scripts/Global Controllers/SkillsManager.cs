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

    private GameProcessManager _gameProcessManager;

    private void Awake()
    {
        FillFreeSkillsLists();
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
    private void Construct(GameProcessManager gameProcessManager)
    {
        _gameProcessManager = gameProcessManager;
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
        for(int i = 0; i < activeSkillsList_Free.Count; i++)
        {

        }
    }

    private void FillFreeSkillsLists()
    {
        activeSkillsList_Free.Clear();
        passiveSkillsList_Free.Clear();
        Debug.Log($"{System.Enum.GetValues(typeof(ActiveSkills)).Length}");

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

    }
}
