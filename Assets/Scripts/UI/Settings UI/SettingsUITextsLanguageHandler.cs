using UnityEngine;
using System.Collections.Generic;
using Localization;
using TMPro;

public class SettingsUITextsLanguageHandler : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private List<TextMeshProUGUI> changeLanguageButtonTextList = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI restoreIAPButtonText;
    [SerializeField] private TextMeshProUGUI storyButtonText;
    [SerializeField] private TextMeshProUGUI privacyPolicyButtonText;
    [Header("Common Texts")]
    [Space]
    [SerializeField] private List<TextMeshProUGUI> backButtonsTextsList = new List<TextMeshProUGUI>();
    [Header("Privacy Policy Texts")]
    [Space]
    [SerializeField] private List<PrivacyPolicyText> ppTextsList = new List<PrivacyPolicyText>();

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        for(int i = 0; i < changeLanguageButtonTextList.Count; i++)
        {
            changeLanguageButtonTextList[i].text = languageHolder.data.settingsUITexts.changeLanguageButtonText;
        }

        restoreIAPButtonText.text = languageHolder.data.settingsUITexts.restoreIapButtonText;
        storyButtonText.text = languageHolder.data.settingsUITexts.storyButtonText;
        privacyPolicyButtonText.text = languageHolder.data.settingsUITexts.privacyPolicyButtonText;

        ChangePrivacyPolicyTexts(_languageManager.CurrentLanguage);

        for (int i = 0; i < backButtonsTextsList.Count; i++)
        {
            backButtonsTextsList[i].text = languageHolder.data.settingsUITexts.backButtonText;
        }
    }

    private void ChangePrivacyPolicyTexts(Languages language)
    {
        for (int i = 0; i < ppTextsList.Count; i++)
        {
            if (ppTextsList[i].Language != language)
            {
                ppTextsList[i].gameObject.SetActive(false);
            }
            else
            {
                ppTextsList[i].gameObject.SetActive(true);
            }
        }
    }
}
