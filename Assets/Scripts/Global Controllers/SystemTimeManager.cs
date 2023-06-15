using UnityEngine;
using Zenject;

public class SystemTimeManager : MonoBehaviour, IDataPersistance
{
    private DataPersistanceManager _dataPersistanceManager;

    private readonly float pauseGameSpeed = 0.000000001f;
    private readonly float normalGameSpeed = 1f;

    private bool newGameReward = false;
    private bool newDay = false;

    public bool NewDay { get => newDay; private set => newDay = value; }

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
    }
    #endregion Zenject

    public void LoadData(GameData data)
    {
        if (newGameReward)
        {
            Debug.Log($"Get Reward New Game");
            newDay = true;
        }
        else
        {
            CheckDayOfPlaying(data.lastDayPlaying);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.lastDayPlaying == DateConstants.newGameIndexForData)
        {
            newGameReward = true;
        }

        data.lastDayPlaying = System.DateTime.Now.DayOfYear;
    }

    private void CheckDayOfPlaying(int lastDay)
    {
        if (lastDay != System.DateTime.Now.DayOfYear)
        {
            Debug.Log($"New Day");
            newDay = true;
        }
        else
        {
            Debug.Log($"Same Day");
            newDay = false;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = pauseGameSpeed;
    }

    public void ResumeGame()
    {
        Time.timeScale = normalGameSpeed;
    }
}
