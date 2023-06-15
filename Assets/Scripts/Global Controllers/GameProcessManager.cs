using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class GameProcessManager : MonoBehaviour
{
    [Header("Player")]
    [Space]
    [SerializeField] private PlayerComponentsManager player;
    [Header("Map Progreess Data")]
    [Space]
    [SerializeField] private float currentMapProgress = 0;
    [SerializeField] private float upgradeProgressValue = 100;
    [Header("Skills")]
    [Space]
    [SerializeField] private List<GameObject> skillsObjectsList = new List<GameObject>();

    private SpawnEnemiesManager _spawnEnemiesManager;
    private MainUI _mainUI;
    private SystemTimeManager _systemTimeManager;
    private SkillsManager _skillsManager;
    private PlayerExperienceManager _playerExperienceManager;
    private PickableItemsManager _pickableItemsManager;
    private GameLevelUI _gameLevelUI;
    private TreasureChestInfoPanel _treasureChestInfoPanel;

    private bool battleStarted = false;
    private bool playerRecoveryOptionUsed = false;

    private int mapProgressDelta = 1;

    public bool BattleStarted { get => battleStarted; private set => battleStarted = value; }
    public bool PlayerRecoveryOptionUsed { get => playerRecoveryOptionUsed; private set => playerRecoveryOptionUsed = value; }

    #region Events Declaration
    public event Action OnGameStarted;
    public event Action OnPlayerLost;
    public event Action OnPlayerRecoveryOptionUsed;
    public event Action OnPlayerWon;
    public event Action OnLevelDataReset;
    public event Action<float, float> OnMapProgressChanged;
    #endregion Events Declaration

    private void Start()
    {
        Input.multiTouchEnabled = false;

        _skillsManager.OnSkillToUpgradeDefined += SkillToUpgradeDefined_ExecuteReaction;

        _playerExperienceManager.OnPlayerGotNewLevel += PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction;

        _pickableItemsManager.OnSkillScrollCollected += PickableItemsManager_SkillScrollCollected_ExecuteReaction;
        _pickableItemsManager.OnTreasureChestCollected += PickableItemsManager_TreasureChestCollected_ExecuteReaction;

        _treasureChestInfoPanel.OnTreasureChestItemsCollected += TreasureChestInfoPanel_OnTreasureChestItemsCollected_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _skillsManager.OnSkillToUpgradeDefined -= SkillToUpgradeDefined_ExecuteReaction;

        _playerExperienceManager.OnPlayerGotNewLevel -= PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction;

        _pickableItemsManager.OnSkillScrollCollected -= PickableItemsManager_SkillScrollCollected_ExecuteReaction;
        _pickableItemsManager.OnTreasureChestCollected -= PickableItemsManager_TreasureChestCollected_ExecuteReaction;

        _treasureChestInfoPanel.OnTreasureChestItemsCollected -= TreasureChestInfoPanel_OnTreasureChestItemsCollected_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(SpawnEnemiesManager spawnEnemiesManager, MainUI mainUI, SystemTimeManager systemTimeManager, SkillsManager skillsManager,
                           PlayerExperienceManager playerExperienceManager, PickableItemsManager pickableItemsManager, GameLevelUI gameLevelUI,
                           TreasureChestInfoPanel treasureChestInfoPanel)
    {
        _spawnEnemiesManager = spawnEnemiesManager;
        _mainUI = mainUI;
        _systemTimeManager = systemTimeManager;
        _skillsManager = skillsManager;
        _playerExperienceManager = playerExperienceManager;
        _pickableItemsManager = pickableItemsManager;
        _gameLevelUI = gameLevelUI;
        _treasureChestInfoPanel = treasureChestInfoPanel;
    }
    #endregion Zenject

    public void StartGame()
    {
        for (int i = 0; i < skillsObjectsList.Count; i++)
        {
            skillsObjectsList[i].SetActive(false);
        }

        battleStarted = true;
        OnGameStarted?.Invoke();
        player.StartGame();
        _systemTimeManager.PauseGame();
    }

    public void IncreaseCurrentProgressValue()
    {
        currentMapProgress += mapProgressDelta;
        OnMapProgressChanged?.Invoke(currentMapProgress, upgradeProgressValue);

        if(currentMapProgress >= upgradeProgressValue)
        {
            _spawnEnemiesManager.SpawnBossAtCenter();
        }
    }

    [ContextMenu("Win")]
    public void GameWin()
    {
        OnPlayerWon?.Invoke();
        //ResetMapData();
    }

    public void ResetLevelDataWithSaving()
    {
        OnLevelDataReset?.Invoke();
        ResetMapData();
        playerRecoveryOptionUsed = false;
    }

    [ContextMenu("Loose")]
    public void GameLost_ExecuteReaction()
    {
        StartCoroutine(PauseGameWithDelayCoroutine());

        if(playerRecoveryOptionUsed)
        {
            Debug.Log($"Player Lost");
            OnPlayerLost?.Invoke();
        }
        else
        {
            Debug.Log($"Player Out Of Hp");
            _gameLevelUI.ShowLoosePanelUI();
        }
        //ResetMapData();
    }

    public void UsePlayerRecoveryOption()
    {
        playerRecoveryOptionUsed = true;
        OnPlayerRecoveryOptionUsed?.Invoke();
        _systemTimeManager.ResumeGame();
    }

    private void ResetMapProgress()
    {
        currentMapProgress = 0f;
        OnMapProgressChanged?.Invoke(currentMapProgress, upgradeProgressValue);
        battleStarted = false;
    }

    private void SkillToUpgradeDefined_ExecuteReaction(int skillCategory, int skillIndex)
    {
        _systemTimeManager.ResumeGame();
        if((SkillBasicTypes)skillCategory == SkillBasicTypes.Active)
        {
            skillsObjectsList[skillIndex].gameObject.SetActive(true);
        }
        StartCoroutine(StartGameCoroutine());
    }

    private void PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction()
    {
        StartCoroutine(PauseGameWithDelayCoroutine());
    }

    private void PickableItemsManager_SkillScrollCollected_ExecuteReaction()
    {
        StartCoroutine(PauseGameWithDelayCoroutine());
    }

    private void PickableItemsManager_TreasureChestCollected_ExecuteReaction(TreasureChestItems _, TreasureChestItems __)
    {
        StartCoroutine(PauseGameWithDelayCoroutine());
    }

    private void TreasureChestInfoPanel_OnTreasureChestItemsCollected_ExecuteReaction()
    {
        _systemTimeManager.ResumeGame();
    }

    private void ResetMapData()
    {
        for (int i = 0; i < skillsObjectsList.Count; i++)
        {
            skillsObjectsList[i].SetActive(false);
        }
        ResetMapProgress();
        _systemTimeManager.ResumeGame();
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _spawnEnemiesManager.SpawnEnemies();
    }

    private IEnumerator PauseGameWithDelayCoroutine()
    {
        yield return null;
        _systemTimeManager.PauseGame();
    }
}
