using UnityEngine;
using System.Collections.Generic;
using Localization;
using TMPro;
using Zenject;

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
    [Header("Privacy Policy Texts")]
    [Space]
    [SerializeField] private List<PrivacyPolicyText> ppTextsList = new List<PrivacyPolicyText>();

    private LanguageManager _languageManager;

    #region Zenject
    [Inject]
    private void Construct(LanguageManager languageManager)
    {
        _languageManager = languageManager;
    }
    #endregion Zenject

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        changeLanguageButtonText.text = languageHolder.data.settingsUITexts.changeLanguageButtonText;
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
