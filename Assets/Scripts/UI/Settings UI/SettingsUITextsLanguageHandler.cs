using UnityEngine;
using System.Collections.Generic;
using Localization;
using TMPro;

public class SettingsUITextsLanguageHandler : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI changeLanguageButtonText;
    [SerializeField] private TextMeshProUGUI storyButtonText;
    [SerializeField] private TextMeshProUGUI privacyPolicyButtonText;
    [Header("Common Texts")]
    [Space]
    [SerializeField] private List<TextMeshProUGUI> backButtonsTextsList = new List<TextMeshProUGUI>();

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        changeLanguageButtonText.text = languageHolder.data.settingsUITexts.changeLanguageButtonText;
        storyButtonText.text = languageHolder.data.settingsUITexts.storyButtonText;
        privacyPolicyButtonText.text = languageHolder.data.settingsUITexts.privacyPolicyButtonText;

        for (int i = 0; i < backButtonsTextsList.Count; i++)
        {
            backButtonsTextsList[i].text = languageHolder.data.settingsUITexts.backButtonText;
        }
    }
}
