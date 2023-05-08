using UnityEngine;
using Zenject;

public class HapticButton : ButtonInteractionHandler
{
    [Header("Button Sprites")]
    [Space]
    [SerializeField] private Sprite hapticOnSprite;
    [SerializeField] private Sprite hapticOffSprite;

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
        ShowAnimation_ButtonPressed();
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
        ChangePanelSprite();
    }

    private void ChangePanelSprite()
    {
        if (canVibrate)
        {
            buttonImage.sprite = hapticOnSprite;
        }
        else
        {
            buttonImage.sprite = hapticOffSprite;
        }
    }
}
