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
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.ForceWaveTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);

                if (upgradeSkillData.SkillLevel != 0)
                {
                    describtionTextsList[i].text = $"+ {GetForceWaveSkillUpgradeValue(upgradeSkillData.SkillLevel) - GetForceWaveSkillUpgradeValue(upgradeSkillData.SkillLevel - 1)} {translatedText}";
                }
                else
                {
                    describtionTextsList[i].text = $"{GetForceWaveSkillUpgradeValue(upgradeSkillData.SkillLevel)} {translatedText}";
                }
            }
        }
        else if(activeSkill == ActiveSkills.SingleShot)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.SingleShotTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.SingleShotTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);

                if (upgradeSkillData.SkillLevel != 0)
                {
                    describtionTextsList[i].text = $"+ {GetSingleShotSkillUpgradeValue(upgradeSkillData.SkillLevel) - GetSingleShotSkillUpgradeValue(upgradeSkillData.SkillLevel - 1)} {translatedText}";
                }
                else
                {
                    describtionTextsList[i].text = $"{GetSingleShotSkillUpgradeValue(upgradeSkillData.SkillLevel)} {translatedText}";
                }
            }
        }
        else if (activeSkill == ActiveSkills.MagicAura)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.MagicAuraTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.MagicAuraTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);

                if (upgradeSkillData.SkillLevel != 0)
                {
                    describtionTextsList[i].text = $"+ {GetMagicAuraSkillUpgradeValue(upgradeSkillData.SkillLevel) - GetMagicAuraSkillUpgradeValue(upgradeSkillData.SkillLevel - 1)} {translatedText}";
                }
                else
                {
                    describtionTextsList[i].text = $"{GetMagicAuraSkillUpgradeValue(upgradeSkillData.SkillLevel)} {translatedText}";
                }
            }
        }
        else if (activeSkill == ActiveSkills.PulseAura)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.PulseAuraTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.PulseAuraTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);

                if (upgradeSkillData.SkillLevel != 0)
                {
                    describtionTextsList[i].text = $"+ {GetPulseAuraSkillUpgradeValue(upgradeSkillData.SkillLevel) - GetPulseAuraSkillUpgradeValue(upgradeSkillData.SkillLevel - 1)} {translatedText}";
                }
                else
                {
                    describtionTextsList[i].text = $"{GetPulseAuraSkillUpgradeValue(upgradeSkillData.SkillLevel)} {translatedText}";
                }
            }
        }
        else if (activeSkill == ActiveSkills.Meteor)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.MeteorTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.MeteorTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);

                if (upgradeSkillData.SkillLevel != 0)
                {
                    describtionTextsList[i].text = $"+ {GetMeteorSkillUpgradeValue(upgradeSkillData.SkillLevel) - GetMeteorSkillUpgradeValue(upgradeSkillData.SkillLevel - 1)} {translatedText}";
                }
                else
                {
                    describtionTextsList[i].text = $"{GetMeteorSkillUpgradeValue(upgradeSkillData.SkillLevel)} {translatedText}";
                }
            }
        }
        else if (activeSkill == ActiveSkills.LightningBolt)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.LightningBoltTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.LightningBoltTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);

                if (upgradeSkillData.SkillLevel != 0)
                {
                    describtionTextsList[i].text = $"+ {GetLightningBoltSkillUpgradeValue(upgradeSkillData.SkillLevel) - GetLightningBoltSkillUpgradeValue(upgradeSkillData.SkillLevel - 1)} {translatedText}";
                }
                else
                {
                    describtionTextsList[i].text = $"{GetLightningBoltSkillUpgradeValue(upgradeSkillData.SkillLevel)} {translatedText}";
                }
            }
        }
        else if (activeSkill == ActiveSkills.ChainLightning)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.ChainLightningTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.ChainLightningTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);

                if (upgradeSkillData.SkillLevel != 0)
                {
                    describtionTextsList[i].text = $"+ {GetChainLightningSkillUpgradeValue(upgradeSkillData.SkillLevel) - GetChainLightningSkillUpgradeValue(upgradeSkillData.SkillLevel - 1)} {translatedText}";
                }
                else
                {
                    describtionTextsList[i].text = $"{GetChainLightningSkillUpgradeValue(upgradeSkillData.SkillLevel)} {translatedText}";
                }
            }
        }
        else if (activeSkill == ActiveSkills.Fireballs)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.FireballsTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.FireballsTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);

                if (upgradeSkillData.SkillLevel != 0)
                {
                    describtionTextsList[i].text = $"+ {GetFireballsSkillUpgradeValue(upgradeSkillData.SkillLevel) - GetFireballsSkillUpgradeValue(upgradeSkillData.SkillLevel - 1)} {translatedText}";
                }
                else
                {
                    describtionTextsList[i].text = $"{GetFireballsSkillUpgradeValue(upgradeSkillData.SkillLevel)} {translatedText}";
                }
            }
        }
        else if (activeSkill == ActiveSkills.AllDirectionsShots)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.AllDiractionsTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.AllDiractionsTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);

                if (upgradeSkillData.SkillLevel != 0)
                {
                    describtionTextsList[i].text = $"+ {GetAllDirectionsShotsSkillUpgradeValue(upgradeSkillData.SkillLevel) - GetAllDirectionsShotsSkillUpgradeValue(upgradeSkillData.SkillLevel - 1)} {translatedText}";
                }
                else
                {
                    describtionTextsList[i].text = $"{GetAllDirectionsShotsSkillUpgradeValue(upgradeSkillData.SkillLevel)} {translatedText}";
                }
            }
        }
        else if (activeSkill == ActiveSkills.WeaponStrike)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.WeaponStrikeTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.WeaponStrikeTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i].characteristic);

                if (upgradeSkillData.SkillLevel != 0)
                {
                    describtionTextsList[i].text = $"+ {GetWeaponStrikeSkillUpgradeValue(upgradeSkillData.SkillLevel) - GetWeaponStrikeSkillUpgradeValue(upgradeSkillData.SkillLevel - 1)} {translatedText}";
                }
                else
                {
                    describtionTextsList[i].text = $"{GetWeaponStrikeSkillUpgradeValue(upgradeSkillData.SkillLevel)} {translatedText}";
                }
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
                
                //if (upgradeSkillData.SkillLevel != 0)
                //{
                    describtionTextsList[i].text = $"+ {passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[upgradeSkillData.SkillLevel].upgradeValue} % {translatedText}";
                //}
                //else
                //{
                 //   describtionTextsList[i].text = $"+ {GetWeaponStrikeSkillUpgradeValue(upgradeSkillData.SkillLevel)} % {translatedText}";
                //}
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseDamage)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseDamageTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseDamageTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = $"+ {passiveSkillsLevelsData.IncreaseDamageUpgradesDataList[upgradeSkillData.SkillLevel].upgradeValue} % {translatedText}";
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseMovementSpeed)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseMovementSpeedTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseMovementSpeedTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = $"+ {passiveSkillsLevelsData.IncreaseMovementSpeedUpgradesDataList[upgradeSkillData.SkillLevel].upgradeValue} % {translatedText}"; ;
            }
        }
        else if (passiveSkill == PassiveSkills.DecreaseIncomeDamage)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.DecreaseIncomeDamageTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.DecreaseIncomeDamageTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = $"+ {passiveSkillsLevelsData.DecreaseIncomeDamageUpgradesDataList[upgradeSkillData.SkillLevel].upgradeValue} % {translatedText}";
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseRegeneration)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseRegenerationTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseRegenerationTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = $"+ {passiveSkillsLevelsData.IncreaseRegenerationUpgradesDataList[upgradeSkillData.SkillLevel].upgradeValue} % {translatedText}";
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseCritChance)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseCritChanceTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseCritChanceTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = $"+ {passiveSkillsLevelsData.IncreaseCritChanceUpgradesDataList[upgradeSkillData.SkillLevel].upgradeValue} % {translatedText}";
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseCritPower)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseCritPowerTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseCritPowerTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = $"+ {passiveSkillsLevelsData.IncreaseCritPowerUpgradesDataList[upgradeSkillData.SkillLevel].upgradeValue} % {translatedText}";
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseProjectileAmount)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseProjectileAmountTranslationData.skillDescribtionTexts.Count; i++)
            {
                translatedText = _languageManager.SkillsTranslationHandler.GetTranslatedSkillText(skillsDescribtionTextsTranslationSO.IncreaseProjectileAmountTranslationData.skillDescribtionTexts[i].characteristic);
                describtionTextsList[i].text = $"+ {passiveSkillsLevelsData.IncreaseProjectilesAmountUpgradesDataList[upgradeSkillData.SkillLevel].upgradeValue} % {translatedText}";
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

    private float GetMagicAuraSkillUpgradeValue(int skillLevel)
    {
        float value = 0;

        for (int i = 0; i < skillsDescribtionTextsTranslationSO.MagicAuraTranslationData[skillLevel].skillDescribtionTexts.Count; i++)
        {
            var characteristic = skillsDescribtionTextsTranslationSO.MagicAuraTranslationData[skillLevel].skillDescribtionTexts[i].characteristic;
            Debug.Log($"{characteristic} level {skillLevel}");
            if (characteristic == PlayerCharacteristicsForTranslation.Damage)
            {
                value = activesSkillsLevelsData.MagicAuraUpgradesDataList[skillLevel].damage;
            }
            else if (characteristic == PlayerCharacteristicsForTranslation.Range)
            {
                value = activesSkillsLevelsData.MagicAuraUpgradesDataList[skillLevel].radius;
            }
        }
        return value;
    }

    private float GetPulseAuraSkillUpgradeValue(int skillLevel)
    {
        float value = 0;

        for (int i = 0; i < skillsDescribtionTextsTranslationSO.PulseAuraTranslationData[skillLevel].skillDescribtionTexts.Count; i++)
        {
            var characteristic = skillsDescribtionTextsTranslationSO.PulseAuraTranslationData[skillLevel].skillDescribtionTexts[i].characteristic;
            Debug.Log($"{characteristic} level {skillLevel}");
            if (characteristic == PlayerCharacteristicsForTranslation.Damage)
            {
                value = activesSkillsLevelsData.PulseAuraUpgradesDataList[skillLevel].damage;
            }
            else if (characteristic == PlayerCharacteristicsForTranslation.Range)
            {
                value = activesSkillsLevelsData.PulseAuraUpgradesDataList[skillLevel].radius;
            }
        }
        return value;
    }

    private float GetMeteorSkillUpgradeValue(int skillLevel)
    {
        float value = 0;

        for (int i = 0; i < skillsDescribtionTextsTranslationSO.MeteorTranslationData[skillLevel].skillDescribtionTexts.Count; i++)
        {
            var characteristic = skillsDescribtionTextsTranslationSO.MeteorTranslationData[skillLevel].skillDescribtionTexts[i].characteristic;
            Debug.Log($"{characteristic} level {skillLevel}");
            if (characteristic == PlayerCharacteristicsForTranslation.Damage)
            {
                value = activesSkillsLevelsData.MeteorUpgradesDataList[skillLevel].damage;
            }
            else if (characteristic == PlayerCharacteristicsForTranslation.PostEffectDuration)
            {
                value = activesSkillsLevelsData.MeteorUpgradesDataList[skillLevel].postEffectDuration;
            }
        }
        return value;
    }

    private float GetLightningBoltSkillUpgradeValue(int skillLevel)
    {
        float value = 0;

        for (int i = 0; i < skillsDescribtionTextsTranslationSO.LightningBoltTranslationData[skillLevel].skillDescribtionTexts.Count; i++)
        {
            var characteristic = skillsDescribtionTextsTranslationSO.LightningBoltTranslationData[skillLevel].skillDescribtionTexts[i].characteristic;
            Debug.Log($"{characteristic} level {skillLevel}");
            if (characteristic == PlayerCharacteristicsForTranslation.Damage)
            {
                value = activesSkillsLevelsData.LightningBoltUpgradesDataList[skillLevel].damage;
            }
            else if (characteristic == PlayerCharacteristicsForTranslation.Cooldown)
            {
                value = activesSkillsLevelsData.LightningBoltUpgradesDataList[skillLevel].cooldown;
            }
        }
        return value;
    }

    private float GetChainLightningSkillUpgradeValue(int skillLevel)
    {
        float value = 0;

        for (int i = 0; i < skillsDescribtionTextsTranslationSO.ChainLightningTranslationData[skillLevel].skillDescribtionTexts.Count; i++)
        {
            var characteristic = skillsDescribtionTextsTranslationSO.ChainLightningTranslationData[skillLevel].skillDescribtionTexts[i].characteristic;
            Debug.Log($"{characteristic} level {skillLevel}");
            if (characteristic == PlayerCharacteristicsForTranslation.Damage)
            {
                value = activesSkillsLevelsData.ChainLightningUpgradesDataList[skillLevel].damage;
            }
            else if (characteristic == PlayerCharacteristicsForTranslation.JumpsAmount)
            {
                value = activesSkillsLevelsData.ChainLightningUpgradesDataList[skillLevel].jumpsAmount;
            }
        }
        return value;
    }

    private float GetFireballsSkillUpgradeValue(int skillLevel)
    {
        float value = 0;

        for (int i = 0; i < skillsDescribtionTextsTranslationSO.FireballsTranslationData[skillLevel].skillDescribtionTexts.Count; i++)
        {
            var characteristic = skillsDescribtionTextsTranslationSO.FireballsTranslationData[skillLevel].skillDescribtionTexts[i].characteristic;
            Debug.Log($"{characteristic} level {skillLevel}");
            if (characteristic == PlayerCharacteristicsForTranslation.Damage)
            {
                value = activesSkillsLevelsData.FireBallssUpgradesDataList[skillLevel].damage;
            }
            else if (characteristic == PlayerCharacteristicsForTranslation.ProjectilesAmount)
            {
                value = activesSkillsLevelsData.FireBallssUpgradesDataList[skillLevel].projectilesAmount;
            }
        }
        return value;
    }

    private float GetAllDirectionsShotsSkillUpgradeValue(int skillLevel)
    {
        float value = 0;

        for (int i = 0; i < skillsDescribtionTextsTranslationSO.AllDiractionsTranslationData[skillLevel].skillDescribtionTexts.Count; i++)
        {
            var characteristic = skillsDescribtionTextsTranslationSO.AllDiractionsTranslationData[skillLevel].skillDescribtionTexts[i].characteristic;
            Debug.Log($"{characteristic} level {skillLevel}");
            if (characteristic == PlayerCharacteristicsForTranslation.Damage)
            {
                value = activesSkillsLevelsData.AllDirectionsShotsUpgradesDataList[skillLevel].damage;
            }
            else if (characteristic == PlayerCharacteristicsForTranslation.ProjectilesAmount)
            {
                value = activesSkillsLevelsData.AllDirectionsShotsUpgradesDataList[skillLevel].projectilesAmount;
            }
        }
        return value;
    }

    private float GetWeaponStrikeSkillUpgradeValue(int skillLevel)
    {
        float value = 0;

        for (int i = 0; i < skillsDescribtionTextsTranslationSO.WeaponStrikeTranslationData[skillLevel].skillDescribtionTexts.Count; i++)
        {
            var characteristic = skillsDescribtionTextsTranslationSO.WeaponStrikeTranslationData[skillLevel].skillDescribtionTexts[i].characteristic;
            Debug.Log($"{characteristic} level {skillLevel}");
            if (characteristic == PlayerCharacteristicsForTranslation.Damage)
            {
                value = activesSkillsLevelsData.WeaponStrikeUpgradesDataList[skillLevel].damage;
            }
        }
        return value;
    }
}
