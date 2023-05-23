using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class PlayerCharacteristicsManager : MonoBehaviour, IDataPersistance
{
    [Header("Start Data")]
    [Space]
    [SerializeField] private PlayerCharacteristicsSO playerCharacteristicsSO;
    [Header("Current Data")]
    [Space]
    [SerializeField] private PlayerBasicCharacteristicsStruct currentPlayerData;

    private DataPersistanceManager _dataPersistanceManager;

    private const float maxPercentAmount = 100f;

    public PlayerBasicCharacteristicsStruct CurrentPlayerData { get => currentPlayerData; private set => currentPlayerData = value; }

    private void Awake()
    {
        currentPlayerData = playerCharacteristicsSO.PlayerBasicDataLists[0];
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
    }
    #endregion Zenject

    #region Save/Load Methods
    public void LoadData(GameData data)
    {
        currentPlayerData = data.playerCharacteristcsData;
    }

    public void SaveData(GameData data)
    {
        data.playerCharacteristcsData = currentPlayerData;
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
}
