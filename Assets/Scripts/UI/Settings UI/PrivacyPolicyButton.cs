using Zenject;

public class PrivacyPolicyButton : ButtonInteractionHandler
{
    private SettingsUI _settingsUI;

    #region Zenject
    [Inject]
    private void Construct(SettingsUI settingsUI)
    {
        _settingsUI = settingsUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        StartCoroutine(ActivateDelayedButtonMethodCoroutine(_settingsUI.ShowPrivacyPolicyPanel));
    }
}
