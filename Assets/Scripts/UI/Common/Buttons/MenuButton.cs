using UnityEngine;
using System.Collections;
using Zenject;

public class MenuButton : ButtonInteractionHandler
{
    [Header("Button Type")]
    [Space]
    [SerializeField] bool loosePanelButton = false;
    [SerializeField] private bool pauseButton = false;

    private MainUI _mainUI;
    private GameProcessManager _gameProcessManager;

    #region Zenject
    [Inject]
    private void Construct(MainUI mainUI, GameProcessManager gameProcessManager)
    {
        _mainUI = mainUI;
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if(loosePanelButton && !_gameProcessManager.PlayerRecoveryOptionUsed)
        {
            Debug.Log($"Loose Without Get Up");

            _gameProcessManager.GameLostSecondTime_ExecuteReaction();
            StartCoroutine(ReturnToMenuCoroutine());
        }
        else
        {
            Debug.Log($"Menu");
            _mainUI.ShowMainScreen();
            _gameProcessManager.ResetLevelDataWithSaving();
        }
    }

    private IEnumerator ReturnToMenuCoroutine()
    {
        yield return null;
        _mainUI.ShowMainScreen();
        _gameProcessManager.ResetLevelDataWithSaving();
    }
}
