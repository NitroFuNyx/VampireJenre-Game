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
    public PassiveCharacteristicsTypes passiveSkillType;
    public float upgradePercent;
}

[Serializable]
public struct PlayerBasicCharacteristicsStruct
{
    public PlayersCharactersTypes playerCharacterType;
    public float characterDefaultHp; // Talent
    public float characterDefaultSpeed;
    public float characterDefaultDamageIncreasePercent;
    public float characterDefaultDamageReductionPercent;
    public float characterDefaultCritChance;
    public float characterDefaultCritPower;
    public float characterDefaultRegenerationPercent;
    public float characterDefaultItemDropChancePercent; // Common
    public float characterDefaultCoinsSurplusPercent;  // Common

    public PlayerSkillsCharacteristicsStruct playerDefaultSkillsData;
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
    public float defaultDamage;
    public float defaultRange;
    public float defaultWidth;
    public float defaultCooldown;
}

[Serializable]
public struct SingleShotSkillDataStruct
{
    public float defaultDamage;
    public float defaultProjectilesAmount;
    public float defaultCooldown;
}

[Serializable]
public struct MagicAuraSkillDataStruct
{
    public float defaultDamage;
    public float defaultRadius;
    public float defaultCooldown;
}

[Serializable]
public struct PulseAuraSkillDataStruct
{
    public float defaultDamage;
    public float defaultRadius;
    public float defaultCooldown;
}

[Serializable]
public struct MeteorSkillDataStruct
{
    public float defaultDamage;
    public float defaultPostEffectDuration;
    public float defaultProjectilesAmount;
    public float defaultCooldown;
}

[Serializable]
public struct LightningBoltSkillDataStruct
{
    public float defaultDamage;
    public float defaultProjectilesAmount;
    public float defaultCooldown;
}

[Serializable]
public struct ChainLightningSkillDataStruct
{
    public float defaultDamage;
    public int defaultJumpsAmount;
    public float defaultCooldown;
}

[Serializable]
public struct FireballsSkillDataStruct
{
    public float defaultDamage;
    public float radius;
    public int defaultProjectilesAmount;
}

[Serializable]
public struct AllDirectionsShotsSkillDataStruct
{
    public float defaultDamage;
    public int defaultProjectilesAmount;
    public float defaultCooldown;
}

[Serializable]
public struct WeaponStrikeSkillDataStruct
{
    public float defaultDamage;
    public float size;
    public float defaultCooldown;
}
