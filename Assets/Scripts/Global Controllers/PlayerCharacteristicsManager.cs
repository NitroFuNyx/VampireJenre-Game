using UnityEngine;
using System.Collections.Generic;
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

    private const float maxPercentAmount = 100f;

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
    }

    private void OnDestroy()
    {
        _gameProcessManager.OnGameStarted -= GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost -= GameProcessManager_PlayerLost_ExecuteReaction;
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
        currentValue += (currentValue * upgradePercent) / maxPercentAmount;
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

    #region Active Skills Upgrade Methods

    private void UpgradeActiveSkill_ForceWave(int skillLevel)
    {
        for (int i = 0; i < activesSkillsLevelsData.ForceWaveUpgradesDataList.Count; i++)
        {
            if (skillLevel == i + 1)
            {
                PlayerForceWaveSkillDataStruct tempSkillDataForUpgrade = new PlayerForceWaveSkillDataStruct();
                tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.playerForceWaveData.damage + activesSkillsLevelsData.ForceWaveUpgradesDataList[i].damage;
                tempSkillDataForUpgrade.range = currentPlayerData.playerSkillsData.playerForceWaveData.range + activesSkillsLevelsData.ForceWaveUpgradesDataList[i].range;
                tempSkillDataForUpgrade.width = currentPlayerData.playerSkillsData.playerForceWaveData.width + activesSkillsLevelsData.ForceWaveUpgradesDataList[i].width;
                tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.playerForceWaveData.cooldown + activesSkillsLevelsData.ForceWaveUpgradesDataList[i].cooldown;

                currentPlayerData.playerSkillsData.playerForceWaveData = tempSkillDataForUpgrade;
                break;
            }
        }
    }

    private void UpgradeActiveSkill_SingleShot(int skillLevel)
    {
        for (int i = 0; i < activesSkillsLevelsData.SingleShotUpgradesDataList.Count; i++)
        {
            if (skillLevel == i + 1)
            {
                SingleShotSkillDataStruct tempSkillDataForUpgrade = new SingleShotSkillDataStruct();
                tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.singleShotSkillData.damage + activesSkillsLevelsData.SingleShotUpgradesDataList[i].damage;
                tempSkillDataForUpgrade.projectilesAmount = currentPlayerData.playerSkillsData.singleShotSkillData.projectilesAmount + activesSkillsLevelsData.SingleShotUpgradesDataList[i].projectilesAmount;
                tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.singleShotSkillData.cooldown + activesSkillsLevelsData.SingleShotUpgradesDataList[i].cooldown;

                currentPlayerData.playerSkillsData.singleShotSkillData = tempSkillDataForUpgrade;
                break;
            }
        }
    }

    private void UpgradeActiveSkill_MagicAura(int skillLevel)
    {
        for (int i = 0; i < activesSkillsLevelsData.MagicAuraUpgradesDataList.Count; i++)
        {
            if (skillLevel == i + 1)
            {
                MagicAuraSkillDataStruct tempSkillDataForUpgrade = new MagicAuraSkillDataStruct();
                tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.magicAuraSkillData.damage + activesSkillsLevelsData.MagicAuraUpgradesDataList[i].damage;
                tempSkillDataForUpgrade.radius = currentPlayerData.playerSkillsData.magicAuraSkillData.radius + activesSkillsLevelsData.MagicAuraUpgradesDataList[i].radius;
                tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.magicAuraSkillData.cooldown + activesSkillsLevelsData.MagicAuraUpgradesDataList[i].cooldown;

                currentPlayerData.playerSkillsData.magicAuraSkillData = tempSkillDataForUpgrade;
                break;
            }
        }
    }

    private void UpgradeActiveSkill_PulseAura(int skillLevel)
    {
        for (int i = 0; i < activesSkillsLevelsData.PulseAuraUpgradesDataList.Count; i++)
        {
            if (skillLevel == i + 1)
            {
                PulseAuraSkillDataStruct tempSkillDataForUpgrade = new PulseAuraSkillDataStruct();
                tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.pulseAuraSkillData.damage + activesSkillsLevelsData.PulseAuraUpgradesDataList[i].damage;
                tempSkillDataForUpgrade.radius = currentPlayerData.playerSkillsData.pulseAuraSkillData.radius + activesSkillsLevelsData.PulseAuraUpgradesDataList[i].radius;
                tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.pulseAuraSkillData.cooldown + activesSkillsLevelsData.PulseAuraUpgradesDataList[i].cooldown;

                currentPlayerData.playerSkillsData.pulseAuraSkillData = tempSkillDataForUpgrade;
                break;
            }
        }
    }

    private void UpgradeActiveSkill_Meteor(int skillLevel)
    {
        for (int i = 0; i < activesSkillsLevelsData.MeteorUpgradesDataList.Count; i++)
        {
            if (skillLevel == i + 1)
            {
                MeteorSkillDataStruct tempSkillDataForUpgrade = new MeteorSkillDataStruct();
                tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.meteorSkillData.damage + activesSkillsLevelsData.MeteorUpgradesDataList[i].damage;
                tempSkillDataForUpgrade.postEffectDuration = currentPlayerData.playerSkillsData.meteorSkillData.postEffectDuration + activesSkillsLevelsData.MeteorUpgradesDataList[i].postEffectDuration;
                tempSkillDataForUpgrade.projectilesAmount = currentPlayerData.playerSkillsData.meteorSkillData.projectilesAmount + activesSkillsLevelsData.MeteorUpgradesDataList[i].projectilesAmount;
                tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.meteorSkillData.cooldown + activesSkillsLevelsData.MeteorUpgradesDataList[i].cooldown;

                currentPlayerData.playerSkillsData.meteorSkillData = tempSkillDataForUpgrade;
                break;
            }
        }
    }

    private void UpgradeActiveSkill_LightningBolt(int skillLevel)
    {
        for (int i = 0; i < activesSkillsLevelsData.LightningBoltUpgradesDataList.Count; i++)
        {
            if (skillLevel == i + 1)
            {
                LightningBoltSkillDataStruct tempSkillDataForUpgrade = new LightningBoltSkillDataStruct();
                tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.lightningBoltSkillData.damage + activesSkillsLevelsData.LightningBoltUpgradesDataList[i].damage;
                tempSkillDataForUpgrade.projectilesAmount = currentPlayerData.playerSkillsData.lightningBoltSkillData.projectilesAmount + activesSkillsLevelsData.LightningBoltUpgradesDataList[i].projectilesAmount;
                tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.lightningBoltSkillData.cooldown + activesSkillsLevelsData.LightningBoltUpgradesDataList[i].cooldown;

                currentPlayerData.playerSkillsData.lightningBoltSkillData = tempSkillDataForUpgrade;
                break;
            }
        }
    }

    private void UpgradeActiveSkill_ChainLightning(int skillLevel)
    {
        for (int i = 0; i < activesSkillsLevelsData.ChainLightningUpgradesDataList.Count; i++)
        {
            if (skillLevel == i + 1)
            {
                ChainLightningSkillDataStruct tempSkillDataForUpgrade = new ChainLightningSkillDataStruct();
                tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.chainLightningSkillData.damage + activesSkillsLevelsData.ChainLightningUpgradesDataList[i].damage;
                tempSkillDataForUpgrade.jumpsAmount = currentPlayerData.playerSkillsData.chainLightningSkillData.jumpsAmount + activesSkillsLevelsData.ChainLightningUpgradesDataList[i].jumpsAmount;
                tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.chainLightningSkillData.cooldown + activesSkillsLevelsData.ChainLightningUpgradesDataList[i].cooldown;

                currentPlayerData.playerSkillsData.chainLightningSkillData = tempSkillDataForUpgrade;
                break;
            }
        }
    }

    private void UpgradeActiveSkill_Fireballs(int skillLevel)
    {
        for (int i = 0; i < activesSkillsLevelsData.FireBallssUpgradesDataList.Count; i++)
        {
            if (skillLevel == i + 1)
            {
                FireballsSkillDataStruct tempSkillDataForUpgrade = new FireballsSkillDataStruct();
                tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.fireballsSkillData.damage + activesSkillsLevelsData.FireBallssUpgradesDataList[i].damage;
                tempSkillDataForUpgrade.radius = currentPlayerData.playerSkillsData.fireballsSkillData.radius + activesSkillsLevelsData.FireBallssUpgradesDataList[i].radius;
                tempSkillDataForUpgrade.projectilesAmount = currentPlayerData.playerSkillsData.fireballsSkillData.projectilesAmount + activesSkillsLevelsData.FireBallssUpgradesDataList[i].projectilesAmount;

                currentPlayerData.playerSkillsData.fireballsSkillData = tempSkillDataForUpgrade;
                break;
            }
        }
    }

    private void UpgradeActiveSkill_AllDirectionsShots(int skillLevel)
    {
        for (int i = 0; i < activesSkillsLevelsData.AllDirectionsShotsUpgradesDataList.Count; i++)
        {
            if (skillLevel == i + 1)
            {
                AllDirectionsShotsSkillDataStruct tempSkillDataForUpgrade = new AllDirectionsShotsSkillDataStruct();
                tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.allDirectionsShotsSkillData.damage + activesSkillsLevelsData.AllDirectionsShotsUpgradesDataList[i].damage;
                tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.allDirectionsShotsSkillData.cooldown + activesSkillsLevelsData.AllDirectionsShotsUpgradesDataList[i].cooldown;
                tempSkillDataForUpgrade.projectilesAmount = currentPlayerData.playerSkillsData.allDirectionsShotsSkillData.projectilesAmount + activesSkillsLevelsData.AllDirectionsShotsUpgradesDataList[i].projectilesAmount;

                currentPlayerData.playerSkillsData.allDirectionsShotsSkillData = tempSkillDataForUpgrade;
                break;
            }
        }
    }

    private void UpgradeActiveSkill_WeaponStrikeShots(int skillLevel)
    {
        for (int i = 0; i < activesSkillsLevelsData.WeaponStrikeUpgradesDataList.Count; i++)
        {
            if (skillLevel == i + 1)
            {
                WeaponStrikeSkillDataStruct tempSkillDataForUpgrade = new WeaponStrikeSkillDataStruct();
                tempSkillDataForUpgrade.damage = currentPlayerData.playerSkillsData.weaponStrikeSkillData.damage + activesSkillsLevelsData.WeaponStrikeUpgradesDataList[i].damage;
                tempSkillDataForUpgrade.size = currentPlayerData.playerSkillsData.weaponStrikeSkillData.size + activesSkillsLevelsData.WeaponStrikeUpgradesDataList[i].size;
                tempSkillDataForUpgrade.cooldown = currentPlayerData.playerSkillsData.weaponStrikeSkillData.cooldown + activesSkillsLevelsData.WeaponStrikeUpgradesDataList[i].cooldown;

                currentPlayerData.playerSkillsData.weaponStrikeSkillData = tempSkillDataForUpgrade;
                break;
            }
        }
    }
    #endregion Active Skills Upgrade Methods
}
