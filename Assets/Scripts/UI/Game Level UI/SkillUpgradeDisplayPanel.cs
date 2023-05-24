using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillUpgradeDisplayPanel : MonoBehaviour
{
    [Header("Skill Data")]
    [Space]
    [SerializeField] private SkillBasicTypes skillType;
    [SerializeField] private ActiveSkills activeSkill;
    [SerializeField] private PassiveSkills passiveSkill;
    [SerializeField] private Sprite skillSprite;
    [SerializeField] private string skillName;
    [Header("Internal References")]
    [Space]
    [SerializeField] private Image skillImage;
    [SerializeField] private TextMeshProUGUI skillLevelText;
    [SerializeField] private TextMeshProUGUI skillNameText;

    public void UpdateUI(UpgradeSkillData upgradeSkillData)
    {
        skillType = upgradeSkillData.SkillType;
        if(skillType == SkillBasicTypes.Active)
        {
            activeSkill = upgradeSkillData.ActiveSkill;
        }
        else
        {
            passiveSkill = upgradeSkillData.PassiveSkill;
        }
        skillImage.sprite = upgradeSkillData.SkillSprite;
        skillLevelText.text = upgradeSkillData.SkillLevelString;
        skillNameText.text = upgradeSkillData.SkillNameString;

        skillSprite = upgradeSkillData.SkillSprite; ;
        skillName = upgradeSkillData.SkillNameString; ;
    }
}
