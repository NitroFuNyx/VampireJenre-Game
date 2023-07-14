using UnityEngine;
using System.Collections;
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
    [Header("Ad Buttons")]
    [Space]
    [SerializeField] private List<GameObject> adButtonsList = new List<GameObject>();
    [Header("Hp Bar")]
    [Space]
    [SerializeField] private Transform hpBar;
    [SerializeField] private Transform hpBarPos_WithAdBanner;
    [SerializeField] private Transform hpBarPos_WithoutAdBanner;
    [Header("Uncommon Mode Elements")]
    [Space]
    [SerializeField] private CanvasGroup gemsDisplayPanel;
    [Header("Map And Player Level Bars")]
    [Space]
    [SerializeField] private CanvasGroup standartModeBar;
    [SerializeField] private CanvasGroup deathmatchModeBar;

    private TimersManager _timersManager;
    private SystemTimeManager _systemTimeManager;
    private GameProcessManager _gameProcessManager;
    private AdsManager _adsManager;
    private AdsController _adsController;

    private VictoryPanel victoryPanel;
    private LoosePanel loosePanel;
    private DeathmatchFinishPanel deathmatchFinishPanel;

    private Dictionary<GameLevelPanels, GameLevelSubPanel> subpanelsDictionary = new Dictionary<GameLevelPanels, GameLevelSubPanel>();

    private float hpBarHeightWithAdBanner = 265f;
    private float hpBarHeightWithoutAd = 120f;

    private void Start()
    {
        SubscribeOnEvents();
        FillSubpanelsDictionary();
        HideAllSubpanels();
        CashComponents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(TimersManager timersManager, SystemTimeManager systemTimeManager, GameProcessManager gameProcessManager,
                           AdsManager adsManager, AdsController adsController)
    {
        _timersManager = timersManager;
        _systemTimeManager = systemTimeManager;
        _gameProcessManager = gameProcessManager;
        _adsManager = adsManager;
        _adsController = adsController;
    }
    #endregion Zenject

    private void SubscribeOnEvents()
    {
        _timersManager.OnStopwatchValueChanged += TimersManager_OnStopwatchValueChanged_ExecuteReaction;
        _gameProcessManager.OnPlayerLost += GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon += GameProcessManager_PlayerWon_ExecuteReaction;
        _gameProcessManager.OnPlayerRecoveryOptionUsed += GameProcessManager_PlayerRecoveryOptionUsed_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _timersManager.OnStopwatchValueChanged -= TimersManager_OnStopwatchValueChanged_ExecuteReaction;
        _gameProcessManager.OnPlayerLost -= GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon -= GameProcessManager_PlayerWon_ExecuteReaction;
        _gameProcessManager.OnPlayerRecoveryOptionUsed -= GameProcessManager_PlayerRecoveryOptionUsed_ExecuteReaction;
    }

    private void CashComponents()
    {
        victoryPanel = subpanelsDictionary[GameLevelPanels.VictoryPanel].GetComponent<VictoryPanel>();
        loosePanel = subpanelsDictionary[GameLevelPanels.LoosePanel].GetComponent<LoosePanel>();
        deathmatchFinishPanel = subpanelsDictionary[GameLevelPanels.DeathmatchFinishPanel].GetComponent<DeathmatchFinishPanel>();
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

        for (int i = 0; i < adButtonsList.Count; i++)
        {
            adButtonsList[i].SetActive(!_adsManager.BlockAdsOptionPurchased);
        }

        if(!_adsManager.BlockAdsOptionPurchased)
        {
            _adsController.LoadBanner();
            hpBar.position = new Vector3(hpBar.position.x, hpBarPos_WithAdBanner.position.y, hpBar.position.z);
        }
        else
        {
            hpBar.position = new Vector3(hpBar.position.x, hpBarPos_WithoutAdBanner.position.y, hpBar.position.z);
        }
    }

    public override void PanelDeactivated_ExecuteReaction()
    {
        _adsController.DestroyBanner();
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

    public void ShowLoosePanelUI()
    {
        HideAllSubpanels();
        loosePanel.UpdatePlayerResults();
        subpanelsDictionary[GameLevelPanels.LoosePanel].ShowPanel();
    }

    public void ShowDeathmatchFinishPanel()
    {
        HideAllSubpanels();
        deathmatchFinishPanel.UpdatePlayerResults();
        subpanelsDictionary[GameLevelPanels.DeathmatchFinishPanel].ShowPanel();
    }

    public void SetBattleModeUI(GameModes gameMode)
    {
        if(gameMode == GameModes.Standart)
        {
            gemsDisplayPanel.alpha = 1f;
            standartModeBar.alpha = 1f;
            deathmatchModeBar.alpha = 0f;
        }
        else
        {
            gemsDisplayPanel.alpha = 0f;
            standartModeBar.alpha = 0f;
            deathmatchModeBar.alpha = 1f;
        }
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

    private void GameProcessManager_PlayerRecoveryOptionUsed_ExecuteReaction()
    {
        HideAllSubpanels();
    }

    private void GameProcessManager_PlayerLost_ExecuteReaction(GameModes gameMode)
    {
        if(gameMode == GameModes.Standart)
        {
            ShowLoosePanelUI();
        }
        else if(gameMode == GameModes.Deathmatch)
        {
            ShowDeathmatchFinishPanel();
        }
    }

    private void GameProcessManager_PlayerWon_ExecuteReaction()
    {
        HideAllSubpanels();
        victoryPanel.UpdatePlayerResults();
        subpanelsDictionary[GameLevelPanels.VictoryPanel].ShowPanel();
    }
}
