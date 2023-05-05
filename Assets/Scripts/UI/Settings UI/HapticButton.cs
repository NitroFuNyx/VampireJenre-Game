using UnityEngine;
using Zenject;

public class HapticButton : ButtonInteractionHandler
{
    private HapticManager _hapticManager;

    private bool canVibrate;

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
    private void Construct(HapticManager hapticManager)
    {
        _hapticManager = hapticManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        canVibrate = !canVibrate;
        _hapticManager.ChangeHapticState(canVibrate);
    }

    private void SubscribeOnEvents()
    {
        _hapticManager.OnHapticStateChanged += HapticStateChanged_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _hapticManager.OnHapticStateChanged -= HapticStateChanged_ExecuteReaction;
    }

    private void HapticStateChanged_ExecuteReaction(bool canVibrateComponent)
    {
        canVibrate = canVibrateComponent;
        //ChangePanelSprite();
    }
}
