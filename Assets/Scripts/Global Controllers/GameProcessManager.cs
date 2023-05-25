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

    private int mapProgressDelta = 1;

    #region Events Declaration
    public event Action OnGameStarted;
    public event Action OnPlayerLost;
    public event Action<float, float> OnMapProgressChanged;
    #endregion Events Declaration

    private void Start()
    {
        Input.multiTouchEnabled = false;

        _skillsManager.OnSkillToUpgradeDefined += SkillToUpgradeDefined_ExecuteReaction;

        _playerExperienceManager.OnPlayerGotNewLevel += PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _skillsManager.OnSkillToUpgradeDefined -= SkillToUpgradeDefined_ExecuteReaction;

        _playerExperienceManager.OnPlayerGotNewLevel -= PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(SpawnEnemiesManager spawnEnemiesManager, MainUI mainUI, SystemTimeManager systemTimeManager, SkillsManager skillsManager, PlayerExperienceManager playerExperienceManager)
    {
        _spawnEnemiesManager = spawnEnemiesManager;
        _mainUI = mainUI;
        _systemTimeManager = systemTimeManager;
        _skillsManager = skillsManager;
        _playerExperienceManager = playerExperienceManager;
    }
    #endregion Zenject

    public void StartGame()
    {
        OnGameStarted?.Invoke();
        _systemTimeManager.PauseGame();
        //player.StartGame();
        //StartCoroutine(StartGameCoroutine());
    }

    public void IncreaseCurrentProgressValue()
    {
        currentMapProgress += mapProgressDelta;
        OnMapProgressChanged?.Invoke(currentMapProgress, upgradeProgressValue);

        if(currentMapProgress >= upgradeProgressValue)
        {
            // spawn boss
        }
    }

    public void GameLost_ExecuteReaction()
    {
        OnPlayerLost?.Invoke();
        for(int i = 0; i < skillsObjectsList.Count; i++)
        {
            skillsObjectsList[i].SetActive(false);
        }
        ResetMapProgress();
    }

    private void ResetMapProgress()
    {
        currentMapProgress = 0f;
        OnMapProgressChanged?.Invoke(currentMapProgress, upgradeProgressValue);
    }

    private void SkillToUpgradeDefined_ExecuteReaction(int skillCategory, int skillIndex)
    {
        _systemTimeManager.ResumeGame();
        player.StartGame();
        if(skillIndex == (int)ActiveSkills.Fireballs)
        {
            skillIndex = (int)ActiveSkills.Meteor;
        }
        skillsObjectsList[skillIndex].gameObject.SetActive(true);
        StartCoroutine(StartGameCoroutine());
    }

    private void PlayerExperienceManager_PlayerGotNewLevel_ExecuteReaction()
    {
        //StartCoroutine(PauseGameWithDelayCoroutine());
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
