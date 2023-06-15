using UnityEngine;
using Zenject;

public class MultiplyCoinsButton : ButtonInteractionHandler
{
    [Header("Panels")]
    [Space]
    [SerializeField] private VictoryPanel victoryPanel;

    private GameProcessManager _gameProcessManager;

    private bool buttonPressed = false;

    private void Start()
    {
        _gameProcessManager.OnLevelDataReset += GameProcessManager_OnLevelDataReset_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _gameProcessManager.OnLevelDataReset -= GameProcessManager_OnLevelDataReset_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(GameProcessManager gameProcessManager)
    {
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if(!buttonPressed)
        {
            buttonPressed = true;
            ShowAnimation_ButtonPressed();
            StartCoroutine(ActivateDelayedButtonMethodCoroutine(SetButtonDisabled));
            victoryPanel.MultiplyCoinsButtonPressed_ExecuteReaction();
        }
    }

    private void GameProcessManager_OnLevelDataReset_ExecuteReaction()
    {
        buttonPressed = false;
        SetButtonActive();
    }
}
