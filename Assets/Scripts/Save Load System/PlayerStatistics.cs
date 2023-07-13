using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[Serializable]
public class PlayerStatistics :MonoBehaviour,  IDataPersistance
{
    private PlayerStatistic _playerStatistic;
    private GameProcessManager _gameProcessManager;
    private TimersManager _timersManager;
    private DataPersistanceManager _dataPersistanceManager;
    private SpawnEnemiesManager _spawnEnemiesManager;


    #region Zenject

    [Inject]
    private void InjectDependencies(GameProcessManager gameProcessManager, TimersManager timersManager,DataPersistanceManager dataPersistanceManager, SpawnEnemiesManager spawnEnemiesManager)
    {
        _spawnEnemiesManager = spawnEnemiesManager;
        _dataPersistanceManager = dataPersistanceManager;
        _timersManager = timersManager;
        _gameProcessManager = gameProcessManager;
    }

    #endregion

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    private void Start()
    {
        _playerStatistic = new PlayerStatistic();
    }

    #region EventSubscription

    private void OnEnable()
    {
        Debug.LogWarning("Subscribed");
        _gameProcessManager.OnPlayerLost += IncreaseMatchesPlayed;
        _gameProcessManager.OnPlayerWon += IncreaseMatchesPlayed;
        _timersManager.OnStopwatchTimeStop += AddTimePlayed;
        _timersManager.OnStopwatchTimeStop += CalculateLongestTimePlayed;
    }

    private void OnDisable()
    {
        _gameProcessManager.OnPlayerLost -= IncreaseMatchesPlayed;
        _gameProcessManager.OnPlayerWon -= IncreaseMatchesPlayed;
        _timersManager.OnStopwatchTimeStop -= AddTimePlayed;
        _timersManager.OnStopwatchTimeStop -= CalculateLongestTimePlayed;
    }

    #endregion

    public void LoadData(GameData data)
    {
        _playerStatistic = data.playerStatistics;
    }

    public void SaveData(GameData data)
    {
        data.playerStatistics = _playerStatistic;
    }

    public void IncreaseMonstersKilled(int Amount)
    {
        _playerStatistic.killedMonstersAmount += Amount;
    }

    public int CalculateAverageMonstersKilled()
    {
        return (int) (_playerStatistic.killedMonstersAmount / _playerStatistic.matchesPlayed);
    }

    public TimeHolder CalculateAverageTimeSurviving()
    {
        TimeHolder averageTime = new TimeHolder();

        int totalMinutesPlayed = _playerStatistic.allTimePlayed.Hours * 60 + _playerStatistic.allTimePlayed.Minutes;

        int averageMinutesPlayed = totalMinutesPlayed / _playerStatistic.matchesPlayed;

        averageTime.Hours = averageMinutesPlayed / 60;
        averageTime.Minutes = averageMinutesPlayed % 60;
        return averageTime;
        Debug.LogWarning("Subscribed");

    }

    public void AddTimePlayed(string time)
    {
        TimeHolder givenTime = new TimeHolder(time);

        int newTotalMinutesPlayed = givenTime.Hours * 60 + givenTime.Minutes;

        int totalMinutesPlayed =_playerStatistic. allTimePlayed.Hours * 60 + _playerStatistic.allTimePlayed.Minutes;

        int updatedTotalMinutesPlayed = totalMinutesPlayed + newTotalMinutesPlayed;

        int updatedHours = updatedTotalMinutesPlayed / 60;
        int updatedMinutes = updatedTotalMinutesPlayed % 60;

        TimeHolder updatedAllTimePlayed = new TimeHolder {Hours = updatedHours, Minutes = updatedMinutes};

        _playerStatistic.allTimePlayed = updatedAllTimePlayed;
        _dataPersistanceManager.SaveGame();

    }

    public void IncreaseMatchesPlayed()
    {
        _playerStatistic.matchesPlayed++;
        Debug.LogWarning("Matches added");
        CalculateAverageTimeSurviving();
        CalculateAverageMonstersKilled();
    }

    public void CalculateLongestTimePlayed(string timePlayed)
    {
        TimeHolder time = new TimeHolder(timePlayed);
        int totalMinutesPlayed = time.Hours * 60 + time.Minutes;

        int longestTotalMinutesPlayed = _playerStatistic.longestTimePlayed.Hours * 60 + _playerStatistic.longestTimePlayed.Minutes;

        if (totalMinutesPlayed > longestTotalMinutesPlayed)
        {
            _playerStatistic.longestTimePlayed = time;
        }
        _dataPersistanceManager.SaveGame();

    }
}
[Serializable]
public class PlayerStatistic
{
    public TimeHolder averageTimeSurviving;
    public TimeHolder allTimePlayed;
    public int matchesPlayed;
    public TimeHolder longestTimePlayed;
    public int killedMonstersAmount;
    public int killedMonstersAverage;
    public Dictionary<ActiveSkills, int> PickedSkillsDictionary;
    public ActiveSkills mostPickableSkill;
    public PlayerStatistic()
    {
        averageTimeSurviving = new TimeHolder();
        allTimePlayed = new TimeHolder();
        matchesPlayed = 0;
        longestTimePlayed = new TimeHolder();
        killedMonstersAmount = 0;
        killedMonstersAverage = 0;
        PickedSkillsDictionary = new Dictionary<ActiveSkills, int>();
    }
}
[Serializable]
public class TimeHolder
{
    public int Hours;
    public int Minutes;


    public TimeHolder()
    {
        Hours = 0;
        Minutes = 0;
    }
    public TimeHolder(string timer)
    {
        ParseTime(timer, out Minutes, out Hours);
    }

    private void ParseTime(string time, out int resultMinutes, out int resultHours)
    {
        string[] timeParts = time.Split(':');

        resultHours = int.Parse(timeParts[0]);
        resultMinutes = int.Parse(timeParts[1]);
    }
}