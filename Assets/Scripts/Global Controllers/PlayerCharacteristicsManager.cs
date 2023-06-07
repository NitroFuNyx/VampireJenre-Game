using UnityEngine;
using Zenject;

public class PlayerCharacteristicsManager : MonoBehaviour, IDataPersistance
{
    [Header("Start Data")]
    [Space]
    [SerializeField] private PlayerCharacteristicsSO playerCharacteristicsSO;
    [SerializeField] private PlayerBasicCharacteristicsStruct playerPersistentData;
    [Header("Current Data")]
    [Space]
    [SerializeField] private PlayerBasicCharacteristicsStruct currentPlayerData;
    [Header("Skills Levels Data")]
    [Space]
    [SerializeField] private ActiveSkillsLevelsSO activesSkillsLevelsData;
    [SerializeField] private PassiveSkillsLevelsSO passiveSkillsLevelsData;

    private DataPersistanceManager _dataPersistanceManager;
    private GameProcessManager _gameProcessManager;

    public PlayerBasicCharacteristicsStruct CurrentPlayerData { get => currentPlayerData; private set => currentPlayerData = value; }

    private void Awake()
    {
        currentPlayerData = playerCharacteristicsSO.PlayerBasicDataLists[0];
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    private void Start()
    {
        _gameProcessManager.OnGameStarted += GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost += GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon += GameProcessManager_PlayerWon_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _gameProcessManager.OnGameStarted -= GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost -= GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon -= GameProcessManager_PlayerWon_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager, GameProcessManager gameProcessManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    #region Save/Load Methods
    public void LoadData(GameData data)
    {
        currentPlayerData = data.playerCharacteristcsData;
    }

    public void SaveData(GameData data)
    {
        if (!_gameProcessManager.BattleStarted)
        {
            data.playerCharacteristcsData = currentPlayerData;
        }
    }
    #endregion Save/Load Methods

    public void UpgradePlayerDataWithSaving(TalentDataStruct talentData)
    {
        Debug.Log($"Talent {talentData.passiveSkillType}");

        if(talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreseItemDropChance)
        {
            UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.characterItemDropChancePercent, talentData.upgradePercent);
        }
        else if(talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreseSkillsRadius)
        {
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.playerForceWaveData.range, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.magicAuraSkillData.radius, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.pulseAuraSkillData.radius, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.fireballsSkillData.radius, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.weaponStrikeSkillData.size, talentData.upgradePercent);
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseDamage)
        {
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.playerForceWaveData.damage, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.singleShotSkillData.damage, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.magicAuraSkillData.damage, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.pulseAuraSkillData.damage, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.meteorSkillData.damage, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.lightningBoltSkillData.damage, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.chainLightningSkillData.damage, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.fireballsSkillData.damage, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.allDirectionsShotsSkillData.damage, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.weaponStrikeSkillData.damage, talentData.upgradePercent);
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseMovementSpeed)
        {
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.characterSpeed, talentData.upgradePercent);
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.DecreaseIncomeDamage)
        {
            UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.characterDamageReductionPercent, talentData.upgradePercent);
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseRegeneration)
        {
            UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.characterRegenerationPercent, talentData.upgradePercent);
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseCritChance)
        {
            UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.characterCritChance, talentData.upgradePercent);
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseCritPower)
        {
            UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.characterCritPower, talentData.upgradePercent);
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseCoinsSurplus)
        {
            UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.characterCoinsSurplusPercent, talentData.upgradePercent);
        }

        _dataPersistanceManager.SaveGame();
    }

    public void UpgradePlayerSkill(int skillCategoryIndex, int skillIndex, int skillLevel)
    {
        if(skillCategoryIndex == (int)SkillBasicTypes.Active)
        {
            if(skillIndex == (int)ActiveSkills.ForceWave)
            {
                UpgradeActiveSkill_ForceWave(skillLevel);
            }
            else if(skillIndex == (int)ActiveSkills.SingleShot)
            {
                UpgradeActiveSkill_SingleShot(skillLevel);
            }
            else if (skillIndex == (int)ActiveSkills.MagicAura)
            {
                UpgradeActiveSkill_MagicAura(skillLevel);
            }
            else if (skillIndex == (int)ActiveSkills.PulseAura)
            {
                UpgradeActiveSkill_PulseAura(skillLevel);
            }
            else if (skillIndex == (int)ActiveSkills.Meteor)
            {
                UpgradeActiveSkill_Meteor(skillLevel);
            }
            else if (skillIndex == (int)ActiveSkills.LightningBolt)
            {
                UpgradeActiveSkill_LightningBolt(skillLevel);
            }
            else if (skillIndex == (int)ActiveSkills.ChainLightning)
            {
                UpgradeActiveSkill_ChainLightning(skillLevel);
            }
            else if (skillIndex == (int)ActiveSkills.Fireballs)
            {
                UpgradeActiveSkill_Fireballs(skillLevel);
            }
            else if (skillIndex == (int)ActiveSkills.AllDirectionsShots)
            {
                UpgradeActiveSkill_AllDirectionsShots(skillLevel);
            }
            else if (skillIndex == (int)ActiveSkills.WeaponStrike)
            {
                UpgradeActiveSkill_WeaponStrikeShots(skillLevel);
            }
        }
        else
        {
            if (skillIndex == (int)PassiveSkills.IncreaseRange)
            {
                UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.playerForceWaveData.range, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.magicAuraSkillData.radius, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.pulseAuraSkillData.radius, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.fireballsSkillData.radius, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.weaponStrikeSkillData.size, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
            }
            else if (skillIndex == (int)PassiveSkills.IncreaseDamage)
            {
                //UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.playerForceWaveData.damage, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                //UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.singleShotSkillData.damage, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                //UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.magicAuraSkillData.damage, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                //UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.pulseAuraSkillData.damage, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                //UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.meteorSkillData.damage, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                //UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.lightningBoltSkillData.damage, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                //UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.chainLightningSkillData.damage, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                //UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.fireballsSkillData.damage, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                //UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.allDirectionsShotsSkillData.damage, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                //UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.weaponStrikeSkillData.damage, passiveSkillsLevelsData.IncreaseRangeUpgradesDataList[skillLevel - 1].upgradeValue);
                UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.characterDamageIncreasePercent, passiveSkillsLevelsData.IncreaseDamageUpgradesDataList[skillLevel - 1].upgradeValue);
            }
            else if (skillIndex == (int)PassiveSkills.IncreaseMovementSpeed)
            {
                UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.characterSpeed, passiveSkillsLevelsData.IncreaseMovementSpeedUpgradesDataList[skillLevel - 1].upgradeValue);
            }
            else if (skillIndex == (int)PassiveSkills.DecreaseIncomeDamage)
            {
                UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.characterDamageReductionPercent, passiveSkillsLevelsData.DecreaseIncomeDamageUpgradesDataList[skillLevel - 1].upgradeValue);
            }
            else if (skillIndex == (int)PassiveSkills.IncreaseRegeneration)
            {
                UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.characterRegenerationPercent, passiveSkillsLevelsData.IncreaseRegenerationUpgradesDataList[skillLevel - 1].upgradeValue);
            }
            else if (skillIndex == (int)PassiveSkills.IncreaseCritChance)
            {
                UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.characterCritChance, passiveSkillsLevelsData.IncreaseCritChanceUpgradesDataList[skillLevel - 1].upgradeValue);
            }
            else if (skillIndex == (int)PassiveSkills.IncreaseCritPower)
            {
                UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.characterCritPower, passiveSkillsLevelsData.IncreaseCritPowerUpgradesDataList[skillLevel - 1].upgradeValue);
            }
            else if (skillIndex == (int)PassiveSkills.IncreaseProjectileAmount)
            {
                UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.playerSkillsData.singleShotSkillData.projectilesAmount, passiveSkillsLevelsData.IncreaseProjectilesAmountUpgradesDataList[skillLevel - 1].upgradeValue);
                UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.playerSkillsData.meteorSkillData.projectilesAmount, passiveSkillsLevelsData.IncreaseProjectilesAmountUpgradesDataList[skillLevel - 1].upgradeValue);
                UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.playerSkillsData.lightningBoltSkillData.projectilesAmount, passiveSkillsLevelsData.IncreaseProjectilesAmountUpgradesDataList[skillLevel - 1].upgradeValue);
                UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.playerSkillsData.fireballsSkillData.projectilesAmount, passiveSkillsLevelsData.IncreaseProjectilesAmountUpgradesDataList[skillLevel - 1].upgradeValue);
                UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.playerSkillsData.allDirectionsShotsSkillData.projectilesAmount, passiveSkillsLevelsData.IncreaseProjectilesAmountUpgradesDataList[skillLevel - 1].upgradeValue);
                UpgradePassiveCharacteristic_AddPercent(ref currentPlayerData.playerSkillsData.playerForceWaveData.projectilesAmount, passiveSkillsLevelsData.IncreaseProjectilesAmountUpgradesDataList[skillLevel - 1].upgradeValue);
            }
        }
    }

    public void ResetPlayerCharacteristicAfterBattle()
    {
        currentPlayerData = playerPersistentData;
    }    

    #region Upgrade Passive Characteristics Methods
    private void UpgradePassiveCharacteristic_AddPercent(ref float currentPercent, float percentSurplus)
    {
        currentPercent += percentSurplus;
    }

    private void UpgradePassiveCharacteristic_PercentOfValue(ref float currentValue, float upgradePercent)
    {
        currentValue += (currentValue * upgradePercent) / CommonValues.maxPercentAmount;
    }
    #endregion Upgrade Passive Characteristics Methods

    private void GameProcessManager_GameStarted_ExecuteReaction()
    {
        playerPersistentData = currentPlayerData;
    }

    private void GameProcessManager_PlayerLost_ExecuteReaction()
    {
        ResetPlayerCharacteristicAfterBattle();
    }

    private void GameProcessManager_PlayerWon_ExecuteReaction()
    {
        ResetPlayerCharacteristicAfterBattle();
    }

    #region Active Skills Upgrade Methods

    private void UpgradeActiveSkill_ForceWave(int skillLevel)
    {
        PlayerForceWaveSkillDataStruct tempSkillDataForUpgrade = new PlayerForceWaveSkillDataStruct();
        tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.playerForceWaveData.damage + activesSkillsLevelsData.ForceWaveUpgradesDataList[skillLevel - 1].damage;
        tempSkillDataForUpgrade.range = currentPlayerData.playerSkillsData.playerForceWaveData.range + activesSkillsLevelsData.ForceWaveUpgradesDataList[skillLevel - 1].range;
        tempSkillDataForUpgrade.width = currentPlayerData.playerSkillsData.playerForceWaveData.width + activesSkillsLevelsData.ForceWaveUpgradesDataList[skillLevel - 1].width;
        tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.playerForceWaveData.cooldown + activesSkillsLevelsData.ForceWaveUpgradesDataList[skillLevel - 1].cooldown;
        tempSkillDataForUpgrade.projectilesAmount = currentPlayerData.playerSkillsData.playerForceWaveData.projectilesAmount + activesSkillsLevelsData.ForceWaveUpgradesDataList[skillLevel - 1].projectilesAmount;

        currentPlayerData.playerSkillsData.playerForceWaveData = tempSkillDataForUpgrade;
    }

    private void UpgradeActiveSkill_SingleShot(int skillLevel)
    {
        SingleShotSkillDataStruct tempSkillDataForUpgrade = new SingleShotSkillDataStruct();
        tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.singleShotSkillData.damage + activesSkillsLevelsData.SingleShotUpgradesDataList[skillLevel - 1].damage;
        tempSkillDataForUpgrade.projectilesAmount = currentPlayerData.playerSkillsData.singleShotSkillData.projectilesAmount + activesSkillsLevelsData.SingleShotUpgradesDataList[skillLevel - 1].projectilesAmount;
        tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.singleShotSkillData.cooldown + activesSkillsLevelsData.SingleShotUpgradesDataList[skillLevel - 1].cooldown;

        currentPlayerData.playerSkillsData.singleShotSkillData = tempSkillDataForUpgrade;
    }

    private void UpgradeActiveSkill_MagicAura(int skillLevel)
    {
        MagicAuraSkillDataStruct tempSkillDataForUpgrade = new MagicAuraSkillDataStruct();
        tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.magicAuraSkillData.damage + activesSkillsLevelsData.MagicAuraUpgradesDataList[skillLevel - 1].damage;
        tempSkillDataForUpgrade.radius = currentPlayerData.playerSkillsData.magicAuraSkillData.radius + activesSkillsLevelsData.MagicAuraUpgradesDataList[skillLevel - 1].radius;
        tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.magicAuraSkillData.cooldown + activesSkillsLevelsData.MagicAuraUpgradesDataList[skillLevel - 1].cooldown;

        currentPlayerData.playerSkillsData.magicAuraSkillData = tempSkillDataForUpgrade;
    }

    private void UpgradeActiveSkill_PulseAura(int skillLevel)
    {
        PulseAuraSkillDataStruct tempSkillDataForUpgrade = new PulseAuraSkillDataStruct();
        tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.pulseAuraSkillData.damage + activesSkillsLevelsData.PulseAuraUpgradesDataList[skillLevel - 1].damage;
        tempSkillDataForUpgrade.radius = currentPlayerData.playerSkillsData.pulseAuraSkillData.radius + activesSkillsLevelsData.PulseAuraUpgradesDataList[skillLevel - 1].radius;
        tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.pulseAuraSkillData.cooldown + activesSkillsLevelsData.PulseAuraUpgradesDataList[skillLevel - 1].cooldown;

        currentPlayerData.playerSkillsData.pulseAuraSkillData = tempSkillDataForUpgrade;
    }

    private void UpgradeActiveSkill_Meteor(int skillLevel)
    {
        MeteorSkillDataStruct tempSkillDataForUpgrade = new MeteorSkillDataStruct();
        tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.meteorSkillData.damage + activesSkillsLevelsData.MeteorUpgradesDataList[skillLevel - 1].damage;
        tempSkillDataForUpgrade.postEffectDuration = currentPlayerData.playerSkillsData.meteorSkillData.postEffectDuration + activesSkillsLevelsData.MeteorUpgradesDataList[skillLevel - 1].postEffectDuration;
        tempSkillDataForUpgrade.projectilesAmount = currentPlayerData.playerSkillsData.meteorSkillData.projectilesAmount + activesSkillsLevelsData.MeteorUpgradesDataList[skillLevel - 1].projectilesAmount;
        tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.meteorSkillData.cooldown + activesSkillsLevelsData.MeteorUpgradesDataList[skillLevel - 1].cooldown;

        currentPlayerData.playerSkillsData.meteorSkillData = tempSkillDataForUpgrade;
    }

    private void UpgradeActiveSkill_LightningBolt(int skillLevel)
    {
        LightningBoltSkillDataStruct tempSkillDataForUpgrade = new LightningBoltSkillDataStruct();
        tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.lightningBoltSkillData.damage + activesSkillsLevelsData.LightningBoltUpgradesDataList[skillLevel - 1].damage;
        tempSkillDataForUpgrade.projectilesAmount = currentPlayerData.playerSkillsData.lightningBoltSkillData.projectilesAmount + activesSkillsLevelsData.LightningBoltUpgradesDataList[skillLevel - 1].projectilesAmount;
        tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.lightningBoltSkillData.cooldown + activesSkillsLevelsData.LightningBoltUpgradesDataList[skillLevel - 1].cooldown;

        currentPlayerData.playerSkillsData.lightningBoltSkillData = tempSkillDataForUpgrade;
    }

    private void UpgradeActiveSkill_ChainLightning(int skillLevel)
    {
        ChainLightningSkillDataStruct tempSkillDataForUpgrade = new ChainLightningSkillDataStruct();
        tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.chainLightningSkillData.damage + activesSkillsLevelsData.ChainLightningUpgradesDataList[skillLevel - 1].damage;
        tempSkillDataForUpgrade.jumpsAmount = currentPlayerData.playerSkillsData.chainLightningSkillData.jumpsAmount + activesSkillsLevelsData.ChainLightningUpgradesDataList[skillLevel - 1].jumpsAmount;
        tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.chainLightningSkillData.cooldown + activesSkillsLevelsData.ChainLightningUpgradesDataList[skillLevel - 1].cooldown;

        currentPlayerData.playerSkillsData.chainLightningSkillData = tempSkillDataForUpgrade;
    }

    private void UpgradeActiveSkill_Fireballs(int skillLevel)
    {
        FireballsSkillDataStruct tempSkillDataForUpgrade = new FireballsSkillDataStruct();
        tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.fireballsSkillData.damage + activesSkillsLevelsData.FireBallssUpgradesDataList[skillLevel - 1].damage;
        tempSkillDataForUpgrade.radius = currentPlayerData.playerSkillsData.fireballsSkillData.radius + activesSkillsLevelsData.FireBallssUpgradesDataList[skillLevel - 1].radius;
        tempSkillDataForUpgrade.projectilesAmount = currentPlayerData.playerSkillsData.fireballsSkillData.projectilesAmount + activesSkillsLevelsData.FireBallssUpgradesDataList[skillLevel - 1].projectilesAmount;

        currentPlayerData.playerSkillsData.fireballsSkillData = tempSkillDataForUpgrade;
    }

    private void UpgradeActiveSkill_AllDirectionsShots(int skillLevel)
    {
        AllDirectionsShotsSkillDataStruct tempSkillDataForUpgrade = new AllDirectionsShotsSkillDataStruct();
        tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.allDirectionsShotsSkillData.damage + activesSkillsLevelsData.AllDirectionsShotsUpgradesDataList[skillLevel - 1].damage;
        tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.allDirectionsShotsSkillData.cooldown + activesSkillsLevelsData.AllDirectionsShotsUpgradesDataList[skillLevel - 1].cooldown;
        tempSkillDataForUpgrade.projectilesAmount = currentPlayerData.playerSkillsData.allDirectionsShotsSkillData.projectilesAmount + activesSkillsLevelsData.AllDirectionsShotsUpgradesDataList[skillLevel - 1].projectilesAmount;

        currentPlayerData.playerSkillsData.allDirectionsShotsSkillData = tempSkillDataForUpgrade;
    }

    private void UpgradeActiveSkill_WeaponStrikeShots(int skillLevel)
    {
        WeaponStrikeSkillDataStruct tempSkillDataForUpgrade = new WeaponStrikeSkillDataStruct();
        tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.weaponStrikeSkillData.damage + activesSkillsLevelsData.WeaponStrikeUpgradesDataList[skillLevel - 1].damage;
        tempSkillDataForUpgrade.size = currentPlayerData.playerSkillsData.weaponStrikeSkillData.size + activesSkillsLevelsData.WeaponStrikeUpgradesDataList[skillLevel - 1].size;
        tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.weaponStrikeSkillData.cooldown + activesSkillsLevelsData.WeaponStrikeUpgradesDataList[skillLevel - 1].cooldown;

        currentPlayerData.playerSkillsData.weaponStrikeSkillData = tempSkillDataForUpgrade;
    }
    #endregion Active Skills Upgrade Methods
}
