using UnityEngine;
using Localization;
using TMPro;

public class RewardsUITextsLanguageHandler : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI spinButtonText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        spinButtonText.text = languageHolder.data.rewardsUITexts.spinButtonText;
    }
}
