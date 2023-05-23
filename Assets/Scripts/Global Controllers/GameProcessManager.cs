using System.Collections;
using UnityEngine;
using System;
using Zenject;

public class GameProcessManager : MonoBehaviour
{
    [Header("Player")]
    [Space]
   // [SerializeField] private GameObject skillObject;
    [SerializeField] private PlayerComponentsManager player;
    [Header("Map Progreess Data")]
    [Space]
    [SerializeField] private float currentMapProgress = 0;
    [SerializeField] private float upgradeProgressValue = 100;

    private SpawnEnemiesManager _spawnEnemiesManager;
    private MainUI _mainUI;

    private int mapProgressDelta = 1;

    #region Events Declaration
    public event Action OnGameStarted;
    public event Action OnPlayerLost;
    public event Action<float, float> OnMapProgressChanged;
    #endregion Events Declaration

    private void Start()
    {
        Input.multiTouchEnabled = false;
    }

    #region Zenject
    [Inject]
    private void Construct(SpawnEnemiesManager spawnEnemiesManager, MainUI mainUI)
    {
        _spawnEnemiesManager = spawnEnemiesManager;
        _mainUI = mainUI;
    }
    #endregion Zenject

    public void StartGame()
    {
        OnGameStarted?.Invoke();
        player.StartGame();
        StartCoroutine(StartGameCoroutine());
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
       // skillObject.SetActive(false);
        OnPlayerLost?.Invoke();
        ResetMapProgress();
    }

    private void ResetMapProgress()
    {
        currentMapProgress = 0f;
        OnMapProgressChanged?.Invoke(currentMapProgress, upgradeProgressValue);
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
       // skillObject.SetActive(true);
        _spawnEnemiesManager.SpawnEnemies();
    }
}
