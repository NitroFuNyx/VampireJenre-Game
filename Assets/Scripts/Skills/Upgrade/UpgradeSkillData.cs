using UnityEngine;

public class UpgradeSkillData
{
    [Header("SKill Type Data")]
    [Space]
    [SerializeField] private SkillBasicTypes skillType;
    [SerializeField] private ActiveSkills activeSkill;
    [SerializeField] private PassiveSkills passiveSkill;
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite skillSprite;
    [Header("Strings")]
    [Space]
    [SerializeField] private string skillLevelString;
    [SerializeField] private string skillNameString;

    public SkillBasicTypes SkillType { get => skillType; set => skillType = value; }
    public ActiveSkills ActiveSkill { get => activeSkill; set => activeSkill = value; }
    public PassiveSkills PassiveSkill { get => passiveSkill; set => passiveSkill = value; }
    public Sprite SkillSprite { get => skillSprite; set => skillSprite = value; }
    public string SkillLevelString { get => skillLevelString; set => skillLevelString = value; }
    public string SkillNameString { get => skillNameString; set => skillNameString = value; }
}
