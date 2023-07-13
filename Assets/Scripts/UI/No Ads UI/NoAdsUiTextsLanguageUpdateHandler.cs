using UnityEngine;
using Localization;
using TMPro;

public class NoAdsUiTextsLanguageUpdateHandler : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI sorryText;
    [SerializeField] private TextMeshProUGUI messageText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        sorryText.text = languageHolder.data.noAdsUITexts.sorryText;
        messageText.text = languageHolder.data.noAdsUITexts.messageText;
    }
}
