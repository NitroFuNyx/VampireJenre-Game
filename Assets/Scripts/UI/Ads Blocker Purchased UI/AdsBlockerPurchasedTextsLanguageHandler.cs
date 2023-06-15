using UnityEngine;
using Localization;
using TMPro;

public class AdsBlockerPurchasedTextsLanguageHandler : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI titleText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        titleText.text = languageHolder.data.adsBlockerPurchasedUITexts.titleText;
    }
}
