using Zenject;

public class SpinButton : ButtonInteractionHandler
{
    private RewardWheelSpinner _rewardWheelSpinner;

    #region Zenject
    [Inject]
    private void Construct(RewardWheelSpinner rewardWheelSpinner)
    {
        _rewardWheelSpinner = rewardWheelSpinner;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if(_rewardWheelSpinner.CanSpin)
        {
            ShowAnimation_ButtonPressed();
            _rewardWheelSpinner.Spin();
        }
    }
}
