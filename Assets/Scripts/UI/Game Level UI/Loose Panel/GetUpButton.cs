using UnityEngine;
using Zenject;

public class GetUpButton : ButtonInteractionHandler
{
    private GameProcessManager _gameProcessManager;
    private AdsManager _adsManager;
    private AdsController _adsController;

    private bool buttonPressed = false;
    private bool rewardRequested = false;

    private void Start()
    {
        _gameProcessManager.OnLevelDataReset += GameProcessManager_OnLevelDataReset_ExecuteReaction;

        _adsController.OnRewardAdViewed += AdsController_RewardGranted_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _gameProcessManager.OnLevelDataReset -= GameProcessManager_OnLevelDataReset_ExecuteReaction;

        _adsController.OnRewardAdViewed -= AdsController_RewardGranted_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(GameProcessManager gameProcessManager, AdsManager adsManager, AdsController adsController)
    {
        _gameProcessManager = gameProcessManager;
        _adsManager = adsManager;
        _adsController = adsController;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        Debug.Log($"Get Up Button Pressed {buttonPressed}");
        if (!buttonPressed)
        {
            buttonPressed = true;
            ShowAnimation_ButtonPressed();
            StartCoroutine(ActivateDelayedButtonMethodCoroutine(SetButtonDisabled));

            if(!_adsManager.BlockAdsOptionPurchased)
            {
                rewardRequested = true;
                _adsController.LoadRewarded();
            }
            //_gameProcessManager.UsePlayerRecoveryOption();
        }
    }

    private void GameProcessManager_OnLevelDataReset_ExecuteReaction()
    {
        buttonPressed = false;
        SetButtonActive();
    }

    private void AdsController_RewardGranted_ExecuteReaction()
    {
        if(rewardRequested)
        {
            rewardRequested = false;
            Debug.Log($"Reward Loose Panel");
            _gameProcessManager.UsePlayerRecoveryOption();
        }
    }
}
