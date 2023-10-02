using System;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

public class SystemTimeManager : MonoBehaviour, IDataPersistance
{
    private DataPersistanceManager _dataPersistanceManager;

    private readonly float pauseGameSpeed = 0.000000001f;
    private readonly float normalGameSpeed = 1f;

    private bool newGameReward = false;
    private bool newDay = false;
    [SerializeField]private int currentDay;
    [SerializeField]private int lastDay;
    [SerializeField]private int consecutiveDays = 0;
    private int daysToCompleteCycle = 7;

    private bool _isRewardClaimed;
    private MainUI _mainUI;
    private DailyRewardsItemManager _dailyRewardsItemManager;
    public bool NewDay { get => newDay; private set => newDay = value; }
    
    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager,MainUI mainUI,DailyRewardsItemManager dailyRewardsItemManager)
    {
        _dailyRewardsItemManager = dailyRewardsItemManager;
        _mainUI = mainUI;
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
            lastDay = data.lastDayPlaying;
            consecutiveDays = data.daysInARow;
        }
        CheckDayOfPlaying();
        
        _mainUI.ShowDailyRewardsUI();
    }

    public void SaveData(GameData data)
    {
        if (data.lastDayPlaying == DateConstants.newGameIndexForData)
        {
            newGameReward = true;
        }
        data.lastDayPlaying = System.DateTime.Now.DayOfYear;
        data.daysInARow = consecutiveDays;

    }

    private void CheckDayOfPlaying()
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
        
        CheckDailyReward();
    }
    public void CheckDailyReward()
    {
     
       

        // Get the current day (0 represents the first day, 1 represents the second day, and so on)
         currentDay = GetCurrentDay();
         if(consecutiveDays>6)
         {
             // Player skipped one or more days, reset the consecutiveDays counter to 1 and show the reward window with the first reward
             consecutiveDays = 0;
             _dailyRewardsItemManager.SelectCurrentDay(consecutiveDays);
         }
        else if (currentDay == lastDay) // Compare the current day with the last login day to check if the player logged in today
        {
            // Player logged in today, show the reward window with the reward for the current consecutive day
            _dailyRewardsItemManager.SelectCurrentDay(consecutiveDays);
        }
        else if (IsConsecutiveDays(lastDay, currentDay))
        {
            // Player logged in on the consecutive day, update the consecutiveDays counter and show the reward window
            consecutiveDays++;
            _dailyRewardsItemManager.SelectCurrentDay(consecutiveDays);
        }
          else
         {
             consecutiveDays = 0;
             _dailyRewardsItemManager.SelectCurrentDay(consecutiveDays);
         }
        

        // Save the current day for the next time the function is called
    }

    // Helper function to get the current day (0 represents the first day, 1 represents the second day, and so on)
    private int GetCurrentDay()
    {
        return DateTime.Now.DayOfYear;
    }

    // Helper function to check if two days are consecutive
    private bool IsConsecutiveDays(int day1, int day2)
    {
        // Check if the second day is exactly one day after the first day
        return day2 == day1 + 1;
    }
    private void GrantReward(int day)
    {
        _isRewardClaimed = true;

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
