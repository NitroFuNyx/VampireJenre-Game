using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Zenject;

public class GameLevelUI : MainCanvasPanel
{
    [Header("Time")]
    [Space]
    [SerializeField] private TextMeshProUGUI stopwatchValueText;
    [Header("Subpanels")]
    [Space]
    [SerializeField] private List<GameLevelSubPanel> subpanelsList = new List<GameLevelSubPanel>();
    [Header("Skill Display Panels")]
    [Space]
    [SerializeField] private List<SkillUpgradeDisplayPanel> skillUpgradeDisplayPanelsList = new List<SkillUpgradeDisplayPanel>();
    [SerializeField] private SkillUpgradeDisplayPanel scrollSkillDisplayPanel;

    private TimersManager _timersManager;
    private SystemTimeManager _systemTimeManager;
    private GameProcessManager _gameProcessManager;

    private Dictionary<GameLevelPanels, GameLevelSubPanel> subpanelsDictionary = new Dictionary<GameLevelPanels, GameLevelSubPanel>();

    private void Start()
    {
        SubscribeOnEvents();
        FillSubpanelsDictionary();
        HideAllSubpanels();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(TimersManager timersManager, SystemTimeManager systemTimeManager, GameProcessManager gameProcessManager)
    {
        _timersManager = timersManager;
        _systemTimeManager = systemTimeManager;
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    private void SubscribeOnEvents()
    {
        _timersManager.OnStopwatchValueChanged += TimersManager_OnStopwatchValueChanged_ExecuteReaction;
        _gameProcessManager.OnPlayerLost += GameProcessManager_PlayerLost_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _timersManager.OnStopwatchValueChanged -= TimersManager_OnStopwatchValueChanged_ExecuteReaction;
        _gameProcessManager.OnPlayerLost -= GameProcessManager_PlayerLost_ExecuteReaction;
    }

    private void FillSubpanelsDictionary()
    {
        for(int i = 0; i < subpanelsList.Count; i++)
        {
            subpanelsDictionary.Add(subpanelsList[i].PanelType, subpanelsList[i]);
        }
    }

    public override void PanelActivated_ExecuteReaction()
    {
        HideAllSubpanels();
    }

    public override void PanelDeactivated_ExecuteReaction()
    {

    }

    public void ShowPausePanel()
    {
        subpanelsDictionary[GameLevelPanels.PausePanel].ShowPanel();
        _systemTimeManager.PauseGame();
    }

    public void ResumeGame()
    {
        HideAllSubpanels();
    }

    public void ShowUpgradePanel(List<UpgradeSkillData> skillsOptionsDataList)
    {
        for(int i = 0; i < skillsOptionsDataList.Count; i++)
        {
            skillUpgradeDisplayPanelsList[i].UpdateUI(skillsOptionsDataList[i]);
        }

        subpanelsDictionary[GameLevelPanels.LevelUpgradePanel].ShowPanel();
    }

    public void ShowSkillScrollInfoPanel(List<UpgradeSkillData> skillsOptionsDataList)
    {
        scrollSkillDisplayPanel.UpdateUI(skillsOptionsDataList[0]);

        subpanelsDictionary[GameLevelPanels.SkillScrollInfoPanel].ShowPanel();
    }

    public void HideUpgradePanel()
    {
        subpanelsDictionary[GameLevelPanels.LevelUpgradePanel].HidePanel();
    }

    public void HideSkillScrollInfoPanel()
    {
        subpanelsDictionary[GameLevelPanels.SkillScrollInfoPanel].HidePanel();
    }

    private void HideAllSubpanels()
    {
        for(int i = 0; i < subpanelsList.Count; i++)
        {
            subpanelsList[i].HidePanel();
        }
    }

    private void TimersManager_OnStopwatchValueChanged_ExecuteReaction(string timeString)
    {
        stopwatchValueText.text = timeString;
    }

    private void GameProcessManager_PlayerLost_ExecuteReaction()
    {
        subpanelsDictionary[GameLevelPanels.LoosePanel].ShowPanel();
    }
}
