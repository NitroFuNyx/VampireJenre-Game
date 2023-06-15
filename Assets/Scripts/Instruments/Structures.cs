using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RewardDataStruct
{
    public RewardIndexes rewardIndex;
    public ResourcesTypes resourceType;
    public int ResourceAmount;
}

[Serializable]
public struct ResourceBonusItemStruct
{
    public ResourcesTypes resourceType;
    public int ResourceAmount;
    public bool canBeCollected;
}

[Serializable]
public struct TalentDataStruct
{
    public TalentsIndexes talentIndex;
    public PassiveCharacteristicsTypes passiveSkillType;
    public SkillUpgradesTypes skillUpgradeType;
    public PlayerCharacteristicsForTranslation talentNameForTranslation;
    public string talentDescribtion;
    public float upgradePercent;
    public Sprite talentSprite;
}

[Serializable]
public struct TalentLevelStruct
{
    public PassiveCharacteristicsTypes passiveSkillType;
    public int level;
}

[Serializable]
public struct ActiveSkillsDisplayDataStruct
{
    public ActiveSkills skill;
    public Sprite skillSprite;
    public string skillName;
    public List<string> skillDescribtionsList;
}

[Serializable]
public struct PassiveSkillsDisplayDataStruct
{
    public PassiveSkills skill;
    public Sprite skillSprite;
    public string skillName;
    public List<string> skillDescribtionsList;
}

[Serializable]
public struct ActiveSkillInGameDataStruct
{
    public ActiveSkills skillType;
    public int skillLevel;
}

[Serializable]
public struct PassiveSkillInGameDataStruct
{
    public PassiveSkills skillType;
    public int skillLevel;
}

[Serializable]
public struct PlayerBasicCharacteristicsStruct
{
    public PlayersCharactersTypes playerCharacterType;
    public float characterHp; // Talent
    public float characterSpeed;
    public float characterDamageIncreasePercent;
    public float characterDamageReductionPercent;
    public float characterCritChance;
    public float characterCritPower;
    public float characterRegenerationPercent;
    public float characterItemDropChancePercent; // Common
    public float characterCoinsSurplusPercent;  // Common

    public PlayerSkillsCharacteristicsStruct playerSkillsData;
}

[Serializable]
public struct PlayerSkillsCharacteristicsStruct
{
    public PlayerForceWaveSkillDataStruct playerForceWaveData;
    public SingleShotSkillDataStruct singleShotSkillData;
    public MagicAuraSkillDataStruct magicAuraSkillData;
    public PulseAuraSkillDataStruct pulseAuraSkillData;
    public MeteorSkillDataStruct meteorSkillData;
    public LightningBoltSkillDataStruct lightningBoltSkillData;
    public ChainLightningSkillDataStruct chainLightningSkillData;
    public FireballsSkillDataStruct fireballsSkillData;
    public AllDirectionsShotsSkillDataStruct allDirectionsShotsSkillData;
    public WeaponStrikeSkillDataStruct weaponStrikeSkillData;
}

[Serializable]
public struct PlayerForceWaveSkillDataStruct
{
    public float damage;
    public float range;
    public float width;
    public float cooldown;
    public float projectilesAmount;

}

[Serializable]
public struct SingleShotSkillDataStruct
{
    public float damage;
    public float projectilesAmount;
    public float cooldown;
}

[Serializable]
public struct MagicAuraSkillDataStruct
{
    public float damage;
    public float radius;
    public float cooldown;
}

[Serializable]
public struct PulseAuraSkillDataStruct
{
    public float damage;
    public float radius;
    public float cooldown;
}

[Serializable]
public struct MeteorSkillDataStruct
{
    public float damage;
    public float postEffectDuration;
    public float projectilesAmount;
    public float cooldown;
}

[Serializable]
public struct LightningBoltSkillDataStruct
{
    public float damage;
    public float projectilesAmount;
    public float cooldown;
}

[Serializable]
public struct ChainLightningSkillDataStruct
{
    public float damage;
    public int jumpsAmount;
    public float cooldown;
}

[Serializable]
public struct FireballsSkillDataStruct
{
    public float damage;
    public float radius;
    public float projectilesAmount;
    public float cooldown;

}

[Serializable]
public struct AllDirectionsShotsSkillDataStruct
{
    public float damage;
    public float projectilesAmount;
    public float cooldown;
}

[Serializable]
public struct WeaponStrikeSkillDataStruct
{
    public float damage;
    public float size;
    public float cooldown;
}

[Serializable]
public struct PassiveSkillUpgradeDataStruct
{
    public float upgradeValue;
}

[Serializable]
public struct ActiveSkillsTranslationDataStruct
{
    public ActiveSkills skill;
    public List<SkillUpgradeValuesStruct> skillDescribtionTexts;
}

[Serializable]
public struct PassiveSkillsTranslationDataStruct
{
    public PassiveSkills skill;
    public List<SkillUpgradeValuesStruct> skillDescribtionTexts;
    //public float upgradeValue;
}

[Serializable]
public struct EnemyDataStruct
{
    public float hp;
    public float damage;
    public float speed;
}

[Serializable]
public struct SkillUpgradeValuesStruct
{
    public PlayerCharacteristicsForTranslation characteristic;
    public float upgradeValue;
}

[Serializable]
public struct TreasureChestResourceDataStruct
{
    public ResourcesTypes resourceType;
    public int resourceAmount;
    public Sprite resourceSprite;
}
