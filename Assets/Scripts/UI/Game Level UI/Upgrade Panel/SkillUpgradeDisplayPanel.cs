using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Zenject;

public class SkillUpgradeDisplayPanel : MonoBehaviour
{
    [Header("Skill Data")]
    [Space]
    [SerializeField] private SkillBasicTypes skillType;
    [SerializeField] private ActiveSkills activeSkill;
    [SerializeField] private PassiveSkills passiveSkill;
    [Header("Internal References")]
    [Space]
    [SerializeField] private Image skillImage;
    [SerializeField] private TextMeshProUGUI skillLevelText;
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private List<TextMeshProUGUI> describtionTextsList = new List<TextMeshProUGUI>();
    [Header("Translation Data")]
    [Space]
    [SerializeField] private SkilllsDescribtionTextsTranslationSO skillsDescribtionTextsTranslationSO;
    [Header("Skills Levels Data")]
    [Space]
    [SerializeField] private ActiveSkillsLevelsSO activesSkillsLevelsData;
    [SerializeField] private PassiveSkillsLevelsSO passiveSkillsLevelsData;

    private LanguageManager _languageManager;

    public SkillBasicTypes SkillType { get => skillType; set => skillType = value; }
    public ActiveSkills ActiveSkill { get => activeSkill; set => activeSkill = value; }
    public PassiveSkills PassiveSkill { get => passiveSkill; set => passiveSkill = value; }

    #region Zenject
    [Inject]
    private void Construct(LanguageManager languageManager)
    {
        _languageManager = languageManager;
    }
    #endregion Zenject

    public void UpdateUI(UpgradeSkillData upgradeSkillData)
    {
        skillType = upgradeSkillData.SkillType;

        ClearSkillUpgradeDescribtion();

        if (skillType == SkillBasicTypes.Active)
        {
            activeSkill = upgradeSkillData.ActiveSkill;

            SetActiveSkillDescribtion(upgradeSkillData);
        }
        else
        {
            passiveSkill = upgradeSkillData.PassiveSkill;

            SetPassiveSkillDescribtion(upgradeSkillData);
        }
        skillImage.sprite = upgradeSkillData.SkillSprite;
        skillLevelText.text = upgradeSkillData.SkillLevelString;
        skillNameText.text = upgradeSkillData.SkillNameString;
    }

    private void ClearSkillUpgradeDescribtion()
    {
        for (int i = 0; i < describtionTextsList.Count; i++)
        {
            describtionTextsList[i].text = "";
        }
    }

    private void SetActiveSkillDescribtion(UpgradeSkillData upgradeSkillData)
    {
        string translatedText = "";

        if (activeSkill == ActiveSkills.ForceWave)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.ForceWaveTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                if(upgradeSkillData.SkillLevel != 0)
                {
                    translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.ForceWaveTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);
                    describtionTextsList[i].text = $"+ {GetForceWaveSkillUpgradeValue(upgradeSkillData.SkillLevel + 1) - GetForceWaveSkillUpgradeValue(upgradeSkillData.SkillLevel)} {translatedText}";
                }
                else
                {
                    translatedText = "";
                }
            }
        }
        else if(activeSkill == ActiveSkills.SingleShot)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.SingleShotTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                if (upgradeSkillData.SkillLevel != 0)
                {
                    translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.SingleShotTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);
                    describtionTextsList[i].text = $"+ {GetSingleShotSkillUpgradeValue(upgradeSkillData.SkillLevel + 1) - GetSingleShotSkillUpgradeValue(upgradeSkillData.SkillLevel)} {translatedText}";
                }
                else
                {
                    translatedText = "";
                }
            }
        }
        else if (activeSkill == ActiveSkills.MagicAura)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.MagicAuraTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.MagicAuraTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (activeSkill == ActiveSkills.PulseAura)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.PulseAuraTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.PulseAuraTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (activeSkill == ActiveSkills.Meteor)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.MeteorTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.MeteorTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (activeSkill == ActiveSkills.LightningBolt)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.LightningBoltTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.LightningBoltTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (activeSkill == ActiveSkills.ChainLightning)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.ChainLightningTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.ChainLightningTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (activeSkill == ActiveSkills.Fireballs)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.FireballsTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.FireballsTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (activeSkill == ActiveSkills.AllDirectionsShots)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.AllDiractionsTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.AllDiractionsTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (activeSkill == ActiveSkills.WeaponStrike)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.WeaponStrikeTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.WeaponStrikeTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
    }

    private void SetPassiveSkillDescribtion(UpgradeSkillData upgradeSkillData)
    {
        string translatedText = "";

        if (passiveSkill == PassiveSkills.IncreaseRange)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseRangeTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseRangeTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseDamage)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseDamageTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseDamageTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseMovementSpeed)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseMovementSpeedTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseMovementSpeedTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (passiveSkill == PassiveSkills.DecreaseIncomeDamage)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.DecreaseIncomeDamageTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.DecreaseIncomeDamageTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseRegeneration)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseRegenerationTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseRegenerationTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseCritChance)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseCritChanceTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseCritChanceTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseCritPower)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseCritPowerTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseCritPowerTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseProjectileAmount)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseProjectileAmountTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseProjectileAmountTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = translatedText;
            }
        }
    }

    private float GetForceWaveSkillUpgradeValue(int skillLevel)
    {
        float value = 0;

        for (int i = 0; i < skillsDescribtionTextsTranslationSO.ForceWaveTranslationData[skillLevel].skillDescribtionTexts.Count; i++)
        {
            var characteristic = skillsDescribtionTextsTranslationSO.ForceWaveTranslationData[skillLevel].skillDescribtionTexts[i].characteristic;
            Debug.Log($"{characteristic} level {skillLevel}");
            if(characteristic == PlayerCharacteristicsForTranslation.Damage)
            {
                value = activesSkillsLevelsData.ForceWaveUpgradesDataList[skillLevel].damage;
            }
            else if(characteristic == PlayerCharacteristicsForTranslation.Range)
            {
                value = activesSkillsLevelsData.ForceWaveUpgradesDataList[skillLevel].range;
            }
            else if (characteristic == PlayerCharacteristicsForTranslation.Width)
            {
                value = activesSkillsLevelsData.ForceWaveUpgradesDataList[skillLevel].width;
            }
        }
        return value;
    }

    private float GetSingleShotSkillUpgradeValue(int skillLevel)
    {
        float value = 0;

        for (int i = 0; i < skillsDescribtionTextsTranslationSO.SingleShotTranslationData[skillLevel].skillDescribtionTexts.Count; i++)
        {
            var characteristic = skillsDescribtionTextsTranslationSO.SingleShotTranslationData[skillLevel].skillDescribtionTexts[i].characteristic;
            Debug.Log($"{characteristic} level {skillLevel}");
            if (characteristic == PlayerCharacteristicsForTranslation.Damage)
            {
                value = activesSkillsLevelsData.SingleShotUpgradesDataList[skillLevel].damage;
            }
            else if (characteristic == PlayerCharacteristicsForTranslation.ProjectilesAmount)
            {
                value = activesSkillsLevelsData.SingleShotUpgradesDataList[skillLevel].projectilesAmount;
            }
            else if (characteristic == PlayerCharacteristicsForTranslation.Cooldown)
            {
                value = activesSkillsLevelsData.SingleShotUpgradesDataList[skillLevel].cooldown;
            }
        }
        return value;
    }
}
