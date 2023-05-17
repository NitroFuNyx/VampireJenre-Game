using System;

[Serializable]
public struct RewardDataStruct
{
    public RewardIndexes rewardIndex;
    public ResourcesTypes resourceTypes;
    public int ResourceAmount;
}

[Serializable]
public struct TalentDataStruct
{
    public TalentsIndexes talentType;
    public PassiveSkillsTypes passiveSkillType;
    public float upgradePercent;
}
