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

    private SpawnEnemiesManager _spawnEnemiesManager;
    private MainUI _mainUI;
    private SystemTimeManager _systemTimeManager;
    private SkillsManager _skillsManager;
    private PlayerExperienceManager _playerExperienceManager;
    private PickableItemsManager _pickableItemsManager;
    private GameLevelUI _gameLevelUI;
    private TreasureChestInfoPanel _treasureChestInfoPanel;
    private AdsController _adsController;
    private AudioManager _audioManager;

    private bool battleStarted = false;
    private bool playerRecoveryOptionUsed = false;
    private bool firstSkillDefined = false;

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

        _adsController.OnInterstialAdClosed += AdsController_InterstialAdClosed_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _skillsManager.OnSkillToUpgradeDefined -= SkillToUpgradeDefined_ExecuteReaction;


        _playerExperienceManager.OnPlayerGotNewLevel -= PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction;

        _pickableItemsManager.OnSkillScrollCollected -= PickableItemsManager_SkillScrollCollected_ExecuteReaction;
        _pickableItemsManager.OnTreasureChestCollected -= PickableItemsManager_TreasureChestCollected_ExecuteReaction;

        _treasureChestInfoPanel.OnTreasureChestItemsCollected -= TreasureChestInfoPanel_OnTreasureChestItemsCollected_ExecuteReaction;

        _adsController.OnInterstialAdClosed -= AdsController_InterstialAdClosed_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(SpawnEnemiesManager spawnEnemiesManager, MainUI mainUI, SystemTimeManager systemTimeManager, SkillsManager skillsManager,
                           PlayerExperienceManager playerExperienceManager, PickableItemsManager pickableItemsManager, GameLevelUI gameLevelUI,
                           TreasureChestInfoPanel treasureChestInfoPanel, AdsController adsController, AudioManager audioManager)
    {
        _spawnEnemiesManager = spawnEnemiesManager;
        _mainUI = mainUI;
        _systemTimeManager = systemTimeManager;
        _skillsManager = skillsManager;
        _playerExperienceManager = playerExperienceManager;
        _pickableItemsManager = pickableItemsManager;
        _gameLevelUI = gameLevelUI;
        _treasureChestInfoPanel = treasureChestInfoPanel;
        _adsController = adsController;
        _audioManager = audioManager;
    }
    #endregion Zenject

    public void StartGame()
    {
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
            _audioManager.PlayMusic_Boss();
        }
    }

    [ContextMenu("Win")]
    public void GameWin()
    {
        _audioManager.StopMusicAudio();
        _audioManager.PlaySFXSound_Victory();
        OnPlayerWon?.Invoke();
        //ResetMapData();
    }

    public void ResetLevelDataWithSaving()
    {
        OnLevelDataReset?.Invoke();
        _audioManager.PlayMusic_MainScreen_Loader();
        ResetMapData();
        playerRecoveryOptionUsed = false;
    }

    [ContextMenu("Loose")]
    public void GameLost_ExecuteReaction()
    {
        StartCoroutine(PauseGameWithDelayCoroutine());
        _audioManager.StopMusicAudio();
        _audioManager.PlaySFXSound_Defeat();

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

    public void GameLostSecondTime_ExecuteReaction()
    {
        _audioManager.StopMusicAudio();
        _audioManager.PlaySFXSound_Defeat();
        OnPlayerLost?.Invoke();
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

    private void SkillToUpgradeDefined_ExecuteReaction(int _, int __)//skill manager
     {
         _systemTimeManager.ResumeGame();
       //   if((SkillBasicTypes)skillCategory == SkillBasicTypes.Active)
       //   {
       //      skillsObjectsList[skillIndex].gameObject.SetActive(true);
       // }
        if(!firstSkillDefined)
        {
            firstSkillDefined = true;
            _audioManager.PlayMusic_Battle();
            Debug.Log($"First Skill Defined");
            StartCoroutine(StartGameCoroutine());
        }
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

    private void AdsController_InterstialAdClosed_ExecuteReaction()
    {
        Debug.Log($"Interstial Closed");
        StartCoroutine(PauseGameWithDelayCoroutine());
    }

    private void ResetMapData()
    {
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
