using UnityEngine;
using Localization;
using TMPro;

public class MainScreenUITextsLanguageHandler : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI playButtonText;
    [SerializeField] private TextMeshProUGUI playDeathmatchButtonText;
    [SerializeField] private TextMeshProUGUI deathmatchFinishedText;
    [SerializeField] private TextMeshProUGUI deathmatchPanelTitleText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        playButtonText.text = languageHolder.data.mainscreenUITexts.playButtonText;
        playDeathmatchButtonText.text = languageHolder.data.mainscreenUITexts.playDeathmatchButtonText;
        deathmatchFinishedText.text = languageHolder.data.mainscreenUITexts.deathmatchFinishedText;
        deathmatchPanelTitleText.text = languageHolder.data.mainscreenUITexts.deathmatchPanelTitleText;
    }
}
