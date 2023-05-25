using System.Collections;
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

    private SpawnEnemiesManager _spawnEnemiesManager;
    private MainUI _mainUI;
    private SystemTimeManager _systemTimeManager;
    private SkillsManager _skillsManager;

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
    }

    private void OnDestroy()
    {
        _skillsManager.OnSkillToUpgradeDefined -= SkillToUpgradeDefined_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(SpawnEnemiesManager spawnEnemiesManager, MainUI mainUI, SystemTimeManager systemTimeManager, SkillsManager skillsManager)
    {
        _spawnEnemiesManager = spawnEnemiesManager;
        _mainUI = mainUI;
        _systemTimeManager = systemTimeManager;
        _skillsManager = skillsManager;
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
        ResetMapProgress();
    }

    private void ResetMapProgress()
    {
        currentMapProgress = 0f;
        OnMapProgressChanged?.Invoke(currentMapProgress, upgradeProgressValue);
    }

    private void SkillToUpgradeDefined_ExecuteReaction(int _, int __)
    {
        _systemTimeManager.ResumeGame();
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _spawnEnemiesManager.SpawnEnemies();
    }
}
