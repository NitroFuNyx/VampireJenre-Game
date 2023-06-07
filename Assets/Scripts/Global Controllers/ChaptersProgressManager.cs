using UnityEngine;
using System;
using Zenject;

public class ChaptersProgressManager : MonoBehaviour, IDataPersistance
{
    private DataPersistanceManager _dataPersistanceManager;
    private GameProcessManager _gameProcessManager;

    private int finishedChaptersCounter = 0;

    public int FinishedChaptersCounter { get => finishedChaptersCounter; }

    #region Events Declaration
    public event Action<int> OnChaptersProhressUpdated;
    #endregion Events Declaration

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    private void Start()
    {
        _gameProcessManager.OnPlayerWon += GameProcessManager_PlayerWon_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _gameProcessManager.OnPlayerWon -= GameProcessManager_PlayerWon_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager, GameProcessManager gameProcessManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    #region Save/Load Methods
    public void LoadData(GameData data)
    {
        this.finishedChaptersCounter = data.finishedChaptersCounter;
    }

    public void SaveData(GameData data)
    {
        data.finishedChaptersCounter = this.finishedChaptersCounter;
    }
    #endregion Save/Load Methods

    public void UpdateChaptersProgress()
    {
        finishedChaptersCounter++;
        if(finishedChaptersCounter > 2)
        {
            finishedChaptersCounter = 2;
        }

        OnChaptersProhressUpdated?.Invoke(finishedChaptersCounter);

        _dataPersistanceManager.SaveGame();
    }

    private void GameProcessManager_PlayerWon_ExecuteReaction()
    {
        UpdateChaptersProgress();
    }
}
