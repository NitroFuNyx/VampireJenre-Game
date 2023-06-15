using UnityEngine;
using Localization;
using TMPro;

public class TalentsWheelUITextsLanguageHandler : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI upgradeButtonText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        upgradeButtonText.text = languageHolder.data.talentsUITexts.upgradeButtonText;
    }
}
