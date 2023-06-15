using UnityEngine;
using System.Collections.Generic;
using Localization;
using Zenject;

public class SkillsTranslationHandler : MonoBehaviour
{
    private LanguageManager _languageManager;

    private LanguageTextsHolder currentLanguageTextsHolder;

    private Dictionary<PlayerCharacteristicsForTranslation, string> skillsCharacteristicsDictionary_English = new Dictionary<PlayerCharacteristicsForTranslation, string>();
    private Dictionary<PlayerCharacteristicsForTranslation, string> skillsCharacteristicsDictionary_Ukrainian = new Dictionary<PlayerCharacteristicsForTranslation, string>();

    private void Start()
    {
        FillEnglishDictionary();
        FillUkrainianDictionariy();
    }

    #region Zenject
    [Inject]
    private void Construct(LanguageManager languageManager)
    {
        _languageManager = languageManager;
    }
    #endregion Zenject

    public string GetTranslatedSkillText(PlayerCharacteristicsForTranslation characteristic)
    {
        string translatedText = "";

        if(_languageManager.CurrentLanguage == Languages.English)
        {
            translatedText = skillsCharacteristicsDictionary_English[characteristic];
        }
        else if(_languageManager.CurrentLanguage == Languages.Ukrainian)
        {
            translatedText = skillsCharacteristicsDictionary_Ukrainian[characteristic];
        }

        return translatedText;
    }

    private void FillEnglishDictionary()
    {
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.Damage, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.damageText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.Range, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.rangeText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.Width, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.widthText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.Cooldown, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.cooldownText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.ProjectilesAmount, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.projectilesAmountText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.PostEffectDuration, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.postEffectDurationText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.JumpsAmount, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.jumpsAmountText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.Size, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.sizeText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.MovementSpeed, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.movementSpeedText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.IncomeDamage, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.incomeDamageText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.Regeneration, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.regenerationText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.CritChance, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.critChanceText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.CritPower, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.critPowerText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.ItemDropChance, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.itemDropChanceText);
        skillsCharacteristicsDictionary_English.Add(PlayerCharacteristicsForTranslation.coinsSurplus, _languageManager.EnglishTextsHolder.data.skillCharacteristicsTexts.coinsSurplusText);
    }

    private void FillUkrainianDictionariy()
    {
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.Damage, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.damageText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.Range, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.rangeText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.Width, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.widthText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.Cooldown, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.cooldownText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.ProjectilesAmount, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.projectilesAmountText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.PostEffectDuration, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.postEffectDurationText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.JumpsAmount, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.jumpsAmountText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.Size, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.sizeText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.MovementSpeed, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.movementSpeedText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.IncomeDamage, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.incomeDamageText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.Regeneration, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.regenerationText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.CritChance, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.critChanceText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.CritPower, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.critPowerText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.ItemDropChance, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.itemDropChanceText);
        skillsCharacteristicsDictionary_Ukrainian.Add(PlayerCharacteristicsForTranslation.coinsSurplus, _languageManager.UkrainianTextsHolder.data.skillCharacteristicsTexts.coinsSurplusText);
    }
}
