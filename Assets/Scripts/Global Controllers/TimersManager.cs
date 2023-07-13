using System.Collections;
using UnityEngine;
using System;
using Zenject;

public class TimersManager : MonoBehaviour
{
    private const int SubdivivisionsInTimeUnitAmount = 60;

    private GameProcessManager _gameProcessManager;

    private float currentStopwatchValue = 0f;

    private bool stopwatchActive = false;

    #region Events Declaration
    public event Action<string> OnStopwatchValueChanged;
    public event Action<string> OnStopwatchTimeStop;
    #endregion Events Declaration

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(GameProcessManager gameProcessManager)
    {
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    public void StartStopwatch()
    {
        currentStopwatchValue = 0f;
        stopwatchActive = true;
        StartCoroutine(StartStopwatchCoroutine());
    }   

    public string GetFormatedTimeString(float time)
    {
        return $"{GetHoursAndMinutesAmount((int)time)}:{GetSecondsAmount((int)time)}";
    }

    private string GetHoursAndMinutesAmount(int currentTimeValue)
    {
        string amountString = "";

        int amount = currentTimeValue / SubdivivisionsInTimeUnitAmount;
        int amountOfHours = 0;
        int amountOfMinutes = 0;

        if (amount / SubdivivisionsInTimeUnitAmount > 0)
        {
            amountOfHours = amount / SubdivivisionsInTimeUnitAmount;
            amountOfMinutes = amount % SubdivivisionsInTimeUnitAmount;

            string hours = $"{amountOfHours}";
            string minutes = $"{amountOfMinutes}";

            if (amountOfHours < 10)
            {
                hours = $"0{amountOfHours}";
            }
            if (amountOfMinutes < 10)
            {
                minutes = $"0{amountOfMinutes}";
            }
            amountString = $"{hours}:{minutes}";
        }
        else
        {
            amountString = $"{amount}";
            if (amount < 10)
            {
                amountString = $"0{amount}";
            }
        }

        return amountString;
    }

    private string GetSecondsAmount(int currentTimeValue)
    {
        string amountString;

        int amount = currentTimeValue % SubdivivisionsInTimeUnitAmount;

        amountString = $"{amount}";
        if (amount < 10)
        {
            amountString = $"0{amount}";
        }

        return amountString;
    }

    private void SubscribeOnEvents()
    {
        _gameProcessManager.OnGameStarted += GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost += GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon += GameProcessManager_PlayerWon_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _gameProcessManager.OnGameStarted -= GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost -= GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon -= GameProcessManager_PlayerWon_ExecuteReaction;
    }

    private void StopStopwatch()
    {
        stopwatchActive = false;
    }

    private void GameProcessManager_GameStarted_ExecuteReaction()
    {
        StartStopwatch();
    }

    private void GameProcessManager_PlayerLost_ExecuteReaction()
    {
        StopStopwatch();
    }

    private void GameProcessManager_PlayerWon_ExecuteReaction()
    {
        StopStopwatch();
    }

    private IEnumerator StartStopwatchCoroutine()
    {
        while(stopwatchActive)
        {
            currentStopwatchValue += Time.deltaTime;
            OnStopwatchValueChanged?.Invoke(GetFormatedTimeString(currentStopwatchValue));
            yield return new WaitForEndOfFrame();
        }
        OnStopwatchTimeStop?.Invoke(GetFormatedTimeString(currentStopwatchValue));
        Debug.LogWarning($"Time is {currentStopwatchValue}");
    }
}
