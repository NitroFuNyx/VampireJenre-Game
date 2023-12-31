using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public PlayerClasses playerCharacterType;
    public bool locked;
    public int characterLevel;
    public int weaponLevel;
    public float characterHp; 
    public float characterSpeed;
    public float skillsRangeIncreasePercent;
    public float characterDamageIncreasePercent;
    public float characterDamageReductionPercent;
    public float characterCritChance;
    public float characterCritPower;
    public float characterRegenerationPercent;
    public float characterItemDropChancePercent; 
    public float characterCoinsSurplusPercent;

    public float currentClassProgressValue_Speed;
    public float currentClassProgressValue_Damage;
    public float currentClassProgressValue_Health;

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

[Serializable]
public struct PlayerClassDataStruct
{
    public PlayerClasses playerClass;
    public Sprite classSprite;
    public string characterName;
    public ResourcesTypes buyingCurrency;
    public float price;

    public float startSpeedPercentValue;
    public float maxSpeedPercentValue;
    public float levelUpgradeSpeedPercentValue;

    public float startDamagePercentValue;
    public float maxDamagePercentValue;
    public float levelUpgradeDamagePercentValue;

    public float startHealthPercentValue;
    public float maxHealthPercentValue;
    public float levelUpgradeHealthPercentValue;
}

[Serializable]
public struct WeaponUpgradeDataStruct
{
    public PlayerClasses playerClass;
    public ResourcesTypes buyingCurrency;
    public float price;
    public PassiveCharacteristicsTypes characteristicForUpgrade;
    public float upgradeValue;
    public int maxWeaponLevel;

    public List<Sprite> weaponSpritesList;
}

[Serializable]
public struct Secureint
{
    public int valueOffset;
    public int valueAmount;

    public Secureint(int coinsValue)
    {
        valueOffset = Random.Range(-1000, 1000);
        valueAmount = coinsValue + valueOffset;
    }

    public int GetValue()
    {
        return valueAmount - valueOffset;
    }

    public override string ToString()
    {
        return GetValue().ToString();
    }

    public static Secureint operator +(Secureint i1, Secureint i2)
    {
        return new Secureint(i1.GetValue() + i2.GetValue());
    }
    public static Secureint operator -(Secureint i1, Secureint i2)
    {
        return new Secureint(i1.GetValue() - i2.GetValue());
    }
    public static Secureint operator *(Secureint i1, Secureint i2)
    {
        return new Secureint(i1.GetValue() * i2.GetValue());
    }
    public static Secureint operator /(Secureint i1, Secureint i2)
    {
        return new Secureint(i1.GetValue() / i2.GetValue());
    }
}
