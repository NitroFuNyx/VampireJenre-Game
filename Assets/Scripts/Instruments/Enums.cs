
public enum Languages
{
    English, Ukrainian
}

public enum UIPanels
{
    MainLoaderPanel, MainScreenPanel, GameLevelUI , TalentWheelUI, ShopUI, CharacterSelectionUI, SettingsUI, MenuButtonsUI, RewardsUI, RoadmapUI,
    SkillsInfoUI, AdsBlockerPurchasedUI, NoAdsUI,DailyRewardsUI
}

public enum DailyRewards
{
    Gold,Gems
}
public enum SettingsPanels
{
    MainSettings, LanguageSettings, InfoPanel, PrivacyPolicyPanel, StoryPanel
}

public enum GameLevelPanels
{
    PausePanel, LevelUpgradePanel, VictoryPanel, LoosePanel, SkillScrollInfoPanel, TreasureChestInfoPanel, DeathmatchFinishPanel
}

public enum PoolItemsTypes
{
    Enemy_Skeleton,Enemy_Ghost,Enemy_Zombie,XP, Singleshot_Projectile, Missile_Projectile, Meteor_Projectile, Lightning_Bolt_Skill,
    Fireball_Skill, ChainLightning_Skill, PowerWave_Skill,
    Boss_Dark_Missile,Zombie_Boss,TreasureChest, SkillScroll, CoinsMagnet, Gem_Green, Gem_Orange, Gem_Purple, Coin, Orc_Boss, Demon_Boss,
    BloodPuddleVFX
}

public enum RewardIndexes
{
    Reward_0, Reward_1, Reward_2, Reward_3, Reward_4, Reward_5, Reward_6, Reward_7,
}

public enum ResourcesTypes
{
    Coins, Gems
}

public enum TalentsIndexes
{
    Talent_0, Talent_1, Talent_2, Talent_3, Talent_4, Talent_5, Talent_6, Talent_7, Talent_8
}

public enum SkillBasicTypes
{
    Active, Passive
}

public enum PassiveCharacteristicsTypes
{
    IncreseItemDropChance, IncreseSkillsRadius, IncreaseDamage, IncreaseMovementSpeed, DecreaseIncomeDamage, IncreaseRegeneration,
    IncreaseCritChance, IncreaseCritPower, IncreaseCoinsSurplus, IncreaseProjectilesAmount
}

public enum ActiveSkills
{
    ForceWave, SingleShot, MagicAura, PulseAura, Meteor, LightningBolt, ChainLightning, Fireballs, AllDirectionsShots, WeaponStrike
}

public enum PassiveSkills
{
    IncreaseRange, IncreaseDamage, IncreaseMovementSpeed, DecreaseIncomeDamage, IncreaseRegeneration, IncreaseCritChance, IncreaseCritPower, IncreaseProjectileAmount,  
}

public enum PlayerClasses
{
    Knight, Dryad, Wizzard
}

public enum SkillUpgradesTypes
{
    PercentSurplus, PercentOfValue
}

public enum PickableItemsCategories
{
    TreasureChest, SkillScroll, CoinsMagnet
}

public enum PlayerCharacteristicsForTranslation
{
    Damage, Range, Width, Cooldown, ProjectilesAmount, PostEffectDuration, JumpsAmount, Size, MovementSpeed, IncomeDamage, Regeneration, CritChance,
    CritPower, ItemDropChance, coinsSurplus
}

public enum ResourcesSaveTypes
{
    GeneralAmount, CurrentLevelAmount
}

public enum TreasureChestItems
{
    Skill, Gems, Coins
}

public enum SelectionArrowTypes
{
    Left, Right
}

public enum ShowNextCharacterButtonsTypes
{
    ArrowButton, FixedCharacterButton
}

public enum GameModes
{
    Standart, Deathmatch
}

public enum LevelMaps
{
    Cementary, Castle
}