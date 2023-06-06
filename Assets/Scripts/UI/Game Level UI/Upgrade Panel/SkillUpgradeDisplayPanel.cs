using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

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

    public SkillBasicTypes SkillType { get => skillType; set => skillType = value; }
    public ActiveSkills ActiveSkill { get => activeSkill; set => activeSkill = value; }
    public PassiveSkills PassiveSkill { get => passiveSkill; set => passiveSkill = value; }

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
        if(activeSkill == ActiveSkills.ForceWave)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.ForceWaveTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.ForceWaveTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i]}";
            }
        }
        else if(activeSkill == ActiveSkills.SingleShot)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.SingleShotTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.SingleShotTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i]}";
            }
        }
        else if (activeSkill == ActiveSkills.MagicAura)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.MagicAuraTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.MagicAuraTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i]}";
            }
        }
        else if (activeSkill == ActiveSkills.PulseAura)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.PulseAuraTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.PulseAuraTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i]}";
            }
        }
        else if (activeSkill == ActiveSkills.Meteor)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.MeteorTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.MeteorTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i]}";
            }
        }
        else if (activeSkill == ActiveSkills.LightningBolt)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.LightningBoltTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.LightningBoltTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i]}";
            }
        }
        else if (activeSkill == ActiveSkills.ChainLightning)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.ChainLightningTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.ChainLightningTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i]}";
            }
        }
        else if (activeSkill == ActiveSkills.Fireballs)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.FireballsTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.FireballsTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i]}";
            }
        }
        else if (activeSkill == ActiveSkills.AllDirectionsShots)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.AllDiractionsTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.AllDiractionsTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i]}";
            }
        }
        else if (activeSkill == ActiveSkills.WeaponStrike)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.WeaponStrikeTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.WeaponStrikeTranslationData[upgradeSkillData.SkillLevel].skillDescribtionTexts[i]}";
            }
        }
    }

    private void SetPassiveSkillDescribtion(UpgradeSkillData upgradeSkillData)
    {
        if (passiveSkill == PassiveSkills.IncreaseRange)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseRangeTranslationData.skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.IncreaseRangeTranslationData.skillDescribtionTexts[i]}";
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseDamage)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseDamageTranslationData.skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.IncreaseDamageTranslationData.skillDescribtionTexts[i]}";
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseMovementSpeed)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseMovementSpeedTranslationData.skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.IncreaseMovementSpeedTranslationData.skillDescribtionTexts[i]}";
            }
        }
        else if (passiveSkill == PassiveSkills.DecreaseIncomeDamage)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.DecreaseIncomeDamageTranslationData.skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.DecreaseIncomeDamageTranslationData.skillDescribtionTexts[i]}";
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseRegeneration)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseRegenerationTranslationData.skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.IncreaseRegenerationTranslationData.skillDescribtionTexts[i]}";
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseCritChance)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseCritChanceTranslationData.skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.IncreaseCritChanceTranslationData.skillDescribtionTexts[i]}";
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseCritPower)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseCritPowerTranslationData.skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.IncreaseCritPowerTranslationData.skillDescribtionTexts[i]}";
            }
        }
        else if (passiveSkill == PassiveSkills.IncreaseProjectileAmount)
        {
            for (int i = 0; i < skillsDescribtionTextsTranslationSO.IncreaseProjectileAmountTranslationData.skillDescribtionTexts.Count; i++)
            {
                describtionTextsList[i].text = $"{skillsDescribtionTextsTranslationSO.IncreaseProjectileAmountTranslationData.skillDescribtionTexts[i]}";
            }
        }
    }
}
