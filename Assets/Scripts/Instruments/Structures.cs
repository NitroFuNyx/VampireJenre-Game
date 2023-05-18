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
    public TalentsIndexes talentIndex;
    public PassiveCharacteristicsTypes passiveSkillType;
    public SkillUpgradesTypes skillUpgradeType;
    public float upgradePercent;
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
    public int projectilesAmount;
}

[Serializable]
public struct AllDirectionsShotsSkillDataStruct
{
    public float damage;
    public int projectilesAmount;
    public float cooldown;
}

[Serializable]
public struct WeaponStrikeSkillDataStruct
{
    public float damage;
    public float size;
    public float cooldown;
}
