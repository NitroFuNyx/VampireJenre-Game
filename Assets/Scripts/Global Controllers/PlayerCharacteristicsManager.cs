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

    private List<float> rangeValuesFromSkillsList = new List<float>();
    private List<float> damageValuesFromSkillsList = new List<float>();

    private void Awake()
    {
        currentPlayerData = playerCharacteristicsSO.PlayerBasicDataLists[0];
        FillRangeValuesList();
        FillDamageValuesList();
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
            //for (int i = 0; i < rangeValuesFromSkillsList.Count; i++)
            //{
            //    UpgradePassiveCharacteristic_PercentOfValue(ref rangeValuesFromSkillsList[i], talentData.upgradePercent);
            //}

            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.playerForceWaveData.range, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.magicAuraSkillData.radius, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.pulseAuraSkillData.radius, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.fireballsSkillData.radius, talentData.upgradePercent);
            UpgradePassiveCharacteristic_PercentOfValue(ref currentPlayerData.playerSkillsData.weaponStrikeSkillData.size, talentData.upgradePercent);
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseDamage)
        {
            //for (int i = 0; i < damageValuesFromSkillsList.Count; i++)
            //{
            //    UpgradePassiveCharacteristic_PercentOfValue(ref damageValuesFromSkillsList[i], talentData.upgradePercent);
            //}

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

    private void FillRangeValuesList()
    {
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.playerForceWaveData.range);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.magicAuraSkillData.radius);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.pulseAuraSkillData.radius);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.fireballsSkillData.radius);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.weaponStrikeSkillData.size);
    }

    private void FillDamageValuesList()
    {
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.playerForceWaveData.damage);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.singleShotSkillData.damage);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.magicAuraSkillData.damage);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.pulseAuraSkillData.damage);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.meteorSkillData.damage);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.lightningBoltSkillData.damage);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.chainLightningSkillData.damage);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.fireballsSkillData.damage);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.allDirectionsShotsSkillData.damage);
        rangeValuesFromSkillsList.Add(currentPlayerData.playerSkillsData.weaponStrikeSkillData.damage);
    }
}
