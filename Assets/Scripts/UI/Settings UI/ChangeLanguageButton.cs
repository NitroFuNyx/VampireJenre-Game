using UnityEngine;
using Localization;
using Zenject;

public class ChangeLanguageButton : ButtonInteractionHandler
{
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite ukrainianFlag;
    [SerializeField] private Sprite englishFlag;

    private LanguageManager _languageManager;

    private void Start()
    {
        _languageManager.OnLanguageChanged += LanguageChanged_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _languageManager.OnLanguageChanged -= LanguageChanged_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(LanguageManager languageManager)
    {
        _languageManager = languageManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        ChangeLanguage();
    }

    private void ChangeLanguage()
    {
        _languageManager.ChangeLanguage();
    }

    private void LanguageChanged_ExecuteReaction(LanguageTextsHolder _)
    {
        if(_languageManager.CurrentLanguage == Languages.Ukrainian)
        {
            buttonImage.sprite = ukrainianFlag;
        }
        else if(_languageManager.CurrentLanguage == Languages.English)
        {
            buttonImage.sprite = englishFlag;
        }
    }
}
