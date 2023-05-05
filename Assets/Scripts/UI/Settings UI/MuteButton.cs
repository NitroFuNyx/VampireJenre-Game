using UnityEngine;

public class MuteButton : ButtonInteractionHandler
{
    [Header("Button Sprites")]
    [Space]
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    private bool muted = false;

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    public override void ButtonActivated()
    {
        muted = !muted;
        ShowAnimation_ButtonPressed();
        _audioManager.ChangeMuteState(muted);
    }

    private void SubscribeOnEvents()
    {
        _audioManager.OnAudioMuteStateChanged += AudioMuteStateChanged_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _audioManager.OnAudioMuteStateChanged -= AudioMuteStateChanged_ExecuteReaction;
    }

    private void ChangePanelSprite()
    {
        if (muted)
        {
            buttonImage.sprite = soundOffSprite;
        }
        else
        {
            buttonImage.sprite = soundOnSprite;
        }
    }

    private void AudioMuteStateChanged_ExecuteReaction(bool audioMuted)
    {
        muted = audioMuted;
        //ChangePanelSprite();
    }
}
