using UnityEngine;
using Localization;
using Zenject;

public abstract class TextsLanguageUpdateHandler : MonoBehaviour
{
    protected LanguageManager _languageManager;

    private void Start()
    {
        _languageManager.OnLanguageChanged += OnLanguageChange_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _languageManager.OnLanguageChanged -= OnLanguageChange_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(LanguageManager languageManager)
    {
        _languageManager = languageManager;
    }
    #endregion Zenject

    public abstract void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder);
}
