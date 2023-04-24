using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;
using Zenject;

public abstract class ButtonInteractionHandler : MonoBehaviour
{
    [Header("Images")]
    [Space]
    [SerializeField] protected Image buttonImage;
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 minScale = new Vector3(0.8f, 0.8f, 0.8f);
    [SerializeField] private float scaleDuration = 0.3f;
    [Header("Audio")]
    [Space]
    [SerializeField] protected bool standartAudio = true;

    private AudioManager _audioManager;

    private Button _button;
    private Image _image;

    private float changeButtonAlphaDuration = 0.1f;

    protected Button ButtonComponent { get => _button; set => _button = value; }

    private void Awake()
    {
        if (TryGetComponent(out Button button))
        {
            _button = button;
            ButtonComponent.onClick.AddListener(ButtonActivated);
            if(standartAudio)
            {
                ButtonComponent.onClick.AddListener(PlayButtonInteractionSound);
            }
        }
        if (TryGetComponent(out Image image))
        {
            _image = image;
        }
    }

    #region Zenject
    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    #endregion Zenject

    public void SetButtonActive()
    {
        ButtonComponent.interactable = true;
    }

    public void SetButtonDisabled()
    {
        ButtonComponent.interactable = false;
    }

    public void ShowButton()
    {
        SetButtonActive();
        _image.DOFade(1f, changeButtonAlphaDuration);
    }

    public void HideButton()
    {
        SetButtonDisabled();
        _image.DOFade(0f, changeButtonAlphaDuration);
    }

    public void ShowAnimation_ButtonPressed()
    {
        transform.DOScale(minScale, scaleDuration).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, scaleDuration);
        });
    }

    public abstract void ButtonActivated();

    protected void PlayButtonInteractionSound()
    {
        if(_audioManager)
        _audioManager.PlaySFXSound_PressButtonUI();
    }

    protected IEnumerator ActivateDelayedButtonMethodCoroutine(Action DelayedButtonMethod)
    {
        yield return new WaitForSeconds(scaleDuration * 2);
        DelayedButtonMethod();
    }
}
