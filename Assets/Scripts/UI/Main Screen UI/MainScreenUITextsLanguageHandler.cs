using UnityEngine;
using Localization;
using TMPro;

public class MainScreenUITextsLanguageHandler : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI playButtonText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        playButtonText.text = languageHolder.data.mainscreenUITexts.playButtonText;
    }
}
