using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class PlayerCharacteristicsManager : MonoBehaviour, IDataPersistance
{
    [Header("Start Data")]
    [Space]
    [SerializeField] private PlayerCharacteristicsSO playerCharacteristicsSO;
    [SerializeField] private PlayerClassesDataSO playerClassesDataSO;
    [SerializeField] private WeaponUpgradeDataSO weaponUpgradeDataSO;
    [SerializeField] private PlayerBasicCharacteristicsStruct playerPersistentData;
    [Header("Current Data")]
    [Space]
    [SerializeField] private PlayerBasicCharacteristicsStruct currentPlayerData;
    [SerializeField] private List<PlayerBasicCharacteristicsStruct> charactersClasesDataList = new List<PlayerBasicCharacteristicsStruct>();
    [Header("Skills Levels Data")]
    [Space]
    [SerializeField] private ActiveSkillsLevelsSO activesSkillsLevelsData;
    [SerializeField] private PassiveSkillsLevelsSO passiveSkillsLevelsData;

    private DataPersistanceManager _dataPersistanceManager;
    private GameProcessManager _gameProcessManager;
    private PlayerCharactersManager _playerCharactersManager;
    private CharacterSelectionUI _characterSelectionUI;
    private BuyCharacterButton _buyCharacterButton;

    private int characterMaxLevel = 10;

    public PlayerBasicCharacteristicsStruct CurrentPlayerData { get => currentPlayerData; private set => currentPlayerData = value; }
    public PlayerCharacteristicsSO PlayerCharactersClasesDataSO { get => playerCharacteristicsSO; }
    public List<PlayerBasicCharacteristicsStruct> CharactersClasesDataList { get => charactersClasesDataList; }

    public event System.Action OnCharacterClassUpgraded;
    public event System.Action OnWeaponUpgraded;

    private void Awake()
    {
        //currentPlayerData = GetCharacterCharacteristicsData(PlayerClasses.Knight);
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    private void Start()
    {
        _gameProcessManager.OnGameStarted += GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost += GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon += GameProcessManager_PlayerWon_ExecuteReaction;

        _buyCharacterButton.OnNewCharacterBought += CharacterBought_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _gameProcessManager.OnGameStarted -= GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnPlayerLost -= GameProcessManager_PlayerLost_ExecuteReaction;
        _gameProcessManager.OnPlayerWon -= GameProcessManager_PlayerWon_ExecuteReaction;

        _buyCharacterButton.OnNewCharacterBought -= CharacterBought_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager, GameProcessManager gameProcessManager, 
                           PlayerCharactersManager playerCharactersManager, CharacterSelectionUI characterSelectionUI,
                           BuyCharacterButton buyCharacterButton)
    {
        _dataPersistanceManager = dataPersistanceManager;
        _gameProcessManager = gameProcessManager;
        _playerCharactersManager = playerCharactersManager;
        _characterSelectionUI = characterSelectionUI;
        _buyCharacterButton = buyCharacterButton;
    }
    #endregion Zenject

    #region Save/Load Methods
    public void LoadData(GameData data)
    {
        if(charactersClasesDataList.Count > 0)
        {
            for (int i = 0; i < charactersClasesDataList.Count; i++)
            {
                charactersClasesDataList[i] = data.playerClasesDataList[i];
            }
        }
        else
        {
            for (int i = 0; i < data.playerClasesDataList.Count; i++)
            {
                charactersClasesDataList.Add(data.playerClasesDataList[i]);
            }
        }

        SetCurrentCharacterData(data.lastPlayedClass);
        _playerCharactersManager.SetPlayCharacterModel(data.lastPlayedClass);
        _characterSelectionUI.ChangeVisibleCharacterButtonPressed_ExecuteReaction(ShowNextCharacterButtonsTypes.FixedCharacterButton,
                                                                                  SelectionArrowTypes.Left, data.lastPlayedClass);
    }

    public void SaveData(GameData data)
    {
        if (!_gameProcessManager.BattleStarted)
        {
            for (int i = 0; i < charactersClasesDataList.Count; i++)
            {
                if(charactersClasesDataList[i].playerCharacterType == data.playerClasesDataList[i].playerCharacterType)
                {
                    data.playerClasesDataList[i] = charactersClasesDataList[i];
                }
            }

            data.lastPlayedClass = currentPlayerData.playerCharacterType;
        }
    }
    #endregion Save/Load Methods

    public PlayerBasicCharacteristicsStruct GetCharacterClassStartValues(int characterIndex)
    {
        PlayerBasicCharacteristicsStruct data = new PlayerBasicCharacteristicsStruct();
        data = playerCharacteristicsSO.PlayerBasicDataLists[characterIndex];

        data.characterSpeed = (playerCharacteristicsSO.StartStandartSpeed *
                              playerClassesDataSO.PlayerClassesDataList[characterIndex].startSpeedPercentValue) /
                              CommonValues.maxPercentAmount;
        data.currentClassProgressValue_Speed = playerClassesDataSO.PlayerClassesDataList[characterIndex].startSpeedPercentValue;

        data.characterDamageIncreasePercent = (playerCharacteristicsSO.StartStandartDamage *
                              playerClassesDataSO.PlayerClassesDataList[characterIndex].startDamagePercentValue) /
                              CommonValues.maxPercentAmount;
        data.currentClassProgressValue_Damage = playerClassesDataSO.PlayerClassesDataList[characterIndex].startDamagePercentValue;

        data.characterHp = (playerCharacteristicsSO.StartStandartHealth *
                              playerClassesDataSO.PlayerClassesDataList[characterIndex].startHealthPercentValue) /
                              CommonValues.maxPercentAmount;
        data.currentClassProgressValue_Health = playerClassesDataSO.PlayerClassesDataList[characterIndex].startHealthPercentValue;

        return data;
    }

    public void UpgradePlayerData_DueToTalentAquired(TalentDataStruct talentData)
    {
        Debug.Log($"Talent {talentData.passiveSkillType}");
        PlayerBasicCharacteristicsStruct tempSkillStruct = new PlayerBasicCharacteristicsStruct();

        if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreseItemDropChance)
        {
            for (int i = 0; i < charactersClasesDataList.Count; i++)
            {
                tempSkillStruct = charactersClasesDataList[i];
                UpgradePassiveCharacteristic_AddPercent(ref tempSkillStruct.characterItemDropChancePercent, talentData.upgradePercent);
                charactersClasesDataList[i] = tempSkillStruct;
            }
        }
        else if(talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreseSkillsRadius)
        {
            for (int i = 0; i < charactersClasesDataList.Count; i++)
            {
                tempSkillStruct = charactersClasesDataList[i];
                UpgradePassiveCharacteristic_AddPercent(ref tempSkillStruct.skillsRangeIncreasePercent, talentData.upgradePercent);
                charactersClasesDataList[i] = tempSkillStruct;
            }
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseDamage)
        {
            for (int i = 0; i < charactersClasesDataList.Count; i++)
            {
                tempSkillStruct = charactersClasesDataList[i];
                UpgradePassiveCharacteristic_AddPercent(ref tempSkillStruct.characterDamageIncreasePercent, talentData.upgradePercent);
                charactersClasesDataList[i] = tempSkillStruct;
            }
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseMovementSpeed)
        {
            for (int i = 0; i < charactersClasesDataList.Count; i++)
            {
                tempSkillStruct = charactersClasesDataList[i];
                UpgradePassiveCharacteristic_PercentOfValue(ref tempSkillStruct.characterSpeed, talentData.upgradePercent);
                charactersClasesDataList[i] = tempSkillStruct;
            }
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.DecreaseIncomeDamage)
        {
            for (int i = 0; i < charactersClasesDataList.Count; i++)
            {
                tempSkillStruct = charactersClasesDataList[i];
                UpgradePassiveCharacteristic_AddPercent(ref tempSkillStruct.characterDamageReductionPercent, talentData.upgradePercent);
                charactersClasesDataList[i] = tempSkillStruct;
            }
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseRegeneration)
        {
            for (int i = 0; i < charactersClasesDataList.Count; i++)
            {
                tempSkillStruct = charactersClasesDataList[i];
                UpgradePassiveCharacteristic_AddPercent(ref tempSkillStruct.characterRegenerationPercent, talentData.upgradePercent);
                charactersClasesDataList[i] = tempSkillStruct;
            }
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseCritChance)
        {
            for (int i = 0; i < charactersClasesDataList.Count; i++)
            {
                tempSkillStruct = charactersClasesDataList[i];
                UpgradePassiveCharacteristic_AddPercent(ref tempSkillStruct.characterCritChance, talentData.upgradePercent);
                charactersClasesDataList[i] = tempSkillStruct;
            }
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseCritPower)
        {
            for (int i = 0; i < charactersClasesDataList.Count; i++)
            {
                tempSkillStruct = charactersClasesDataList[i];
                UpgradePassiveCharacteristic_AddPercent(ref tempSkillStruct.characterCritPower, talentData.upgradePercent);
                charactersClasesDataList[i] = tempSkillStruct;
            }
        }
        else if (talentData.passiveSkillType == PassiveCharacteristicsTypes.IncreaseCoinsSurplus)
        {
            for (int i = 0; i < charactersClasesDataList.Count; i++)
            {
                tempSkillStruct = charactersClasesDataList[i];
                UpgradePassiveCharacteristic_AddPercent(ref tempSkillStruct.characterCoinsSurplusPercent, talentData.upgradePercent);
                charactersClasesDataList[i] = tempSkillStruct;
            }
        }

        SetCurrentCharacterData(_playerCharactersManager.CurrentClass);

        //_dataPersistanceManager.SaveGame();
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

    public void UpgradeCharacterClassData(PlayerClasses playerClass)
    {
        PlayerBasicCharacteristicsStruct tempSkillStruct = new PlayerBasicCharacteristicsStruct();

        for (int i = 0; i < charactersClasesDataList.Count; i++)
        {
            if (charactersClasesDataList[i].playerCharacterType == playerClass)
            {
                tempSkillStruct = charactersClasesDataList[i];
                if (charactersClasesDataList[i].characterLevel < characterMaxLevel)
                {
                    tempSkillStruct.characterLevel++;

                    tempSkillStruct.characterSpeed += GetUpgradeValueForSingleClassCharacteristic(playerClassesDataSO.PlayerClassesDataList[i].levelUpgradeSpeedPercentValue, playerCharacteristicsSO.StartStandartSpeed);
                    tempSkillStruct.currentClassProgressValue_Speed += playerClassesDataSO.PlayerClassesDataList[i].levelUpgradeSpeedPercentValue;

                    tempSkillStruct.characterDamageIncreasePercent += GetUpgradeValueForSingleClassCharacteristic(playerClassesDataSO.PlayerClassesDataList[i].levelUpgradeDamagePercentValue, playerCharacteristicsSO.StartStandartDamage);
                    tempSkillStruct.currentClassProgressValue_Damage += playerClassesDataSO.PlayerClassesDataList[i].levelUpgradeDamagePercentValue;

                    tempSkillStruct.characterHp += GetUpgradeValueForSingleClassCharacteristic(playerClassesDataSO.PlayerClassesDataList[i].levelUpgradeHealthPercentValue, playerCharacteristicsSO.StartStandartHealth);
                    tempSkillStruct.currentClassProgressValue_Health += playerClassesDataSO.PlayerClassesDataList[i].levelUpgradeHealthPercentValue;


                    charactersClasesDataList[i] = tempSkillStruct;
                    if(currentPlayerData.playerCharacterType == playerClass)
                    {
                        currentPlayerData = charactersClasesDataList[i];
                    }

                    OnCharacterClassUpgraded?.Invoke();
                }
                else
                {
                    // Show Max Level Message
                }
                break;
            }
        }

        _dataPersistanceManager.SaveGame();
    }

    public WeaponUpgradeDataStruct GetWeaponUpgradeData(PlayerClasses playerClass)
    {
        WeaponUpgradeDataStruct weaponUpgradeData = weaponUpgradeDataSO.WeaponUpgradeDataList[0];

        for (int i = 0; i < weaponUpgradeDataSO.WeaponUpgradeDataList.Count; i++)
        {
            if (weaponUpgradeDataSO.WeaponUpgradeDataList[i].playerClass == playerClass)
            {
                weaponUpgradeData = weaponUpgradeDataSO.WeaponUpgradeDataList[i];
                break;
            }
        }

        return weaponUpgradeData;
    }

    public void UpgradePlayerWeapon(PlayerClasses playerClass)
    {
        PlayerBasicCharacteristicsStruct tempSkillStruct = new PlayerBasicCharacteristicsStruct();

        WeaponUpgradeDataStruct weaponUpgradeData = GetWeaponUpgradeData(playerClass);


        for (int i = 0; i < charactersClasesDataList.Count; i++)
        {
            if (charactersClasesDataList[i].playerCharacterType == playerClass)
            {
                tempSkillStruct = charactersClasesDataList[i];
                if (charactersClasesDataList[i].weaponLevel < weaponUpgradeData.maxWeaponLevel)
                {
                    tempSkillStruct.weaponLevel++;

                    if(weaponUpgradeData.characteristicForUpgrade == PassiveCharacteristicsTypes.DecreaseIncomeDamage)
                    {
                        tempSkillStruct.characterDamageReductionPercent += weaponUpgradeData.upgradeValue;
                    }
                    else if(weaponUpgradeData.characteristicForUpgrade == PassiveCharacteristicsTypes.IncreaseMovementSpeed)
                    {
                        tempSkillStruct.characterSpeed += weaponUpgradeData.upgradeValue;
                    }
                    else if (weaponUpgradeData.characteristicForUpgrade == PassiveCharacteristicsTypes.IncreaseDamage)
                    {
                        tempSkillStruct.characterDamageIncreasePercent += weaponUpgradeData.upgradeValue;
                    }

                    charactersClasesDataList[i] = tempSkillStruct;
                    if (currentPlayerData.playerCharacterType == playerClass)
                    {
                        currentPlayerData = charactersClasesDataList[i];
                    }

                    OnWeaponUpgraded?.Invoke();
                }
                else
                {
                    // Show Max Level Message
                }
                break;
            }
        }

        _dataPersistanceManager.SaveGame();
    }

    private float GetUpgradeValueForSingleClassCharacteristic(float upgradePercent, float defaultValue)
    {
        float upgradeValue = (defaultValue * upgradePercent) / CommonValues.maxPercentAmount;
        return upgradeValue;
    }

    public bool CheckIfCharacterClassIsFullyUpgraded(PlayerClasses playerClass)
    {
        bool isFullyUpgraded = false;

        for (int i = 0; i < charactersClasesDataList.Count; i++)
        {
            if (charactersClasesDataList[i].playerCharacterType == playerClass)
            {
                if (charactersClasesDataList[i].characterLevel == characterMaxLevel)
                {
                    isFullyUpgraded = true;
                }
                break;
            }
        }

        return isFullyUpgraded;
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

    private void GameProcessManager_PlayerLost_ExecuteReaction(GameModes _)
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

    public PlayerBasicCharacteristicsStruct GetCharacterCharacteristicsData(PlayerClasses characterType)
    {
        PlayerBasicCharacteristicsStruct characterData = new PlayerBasicCharacteristicsStruct();

        for(int i = 0; i < charactersClasesDataList.Count; i++)
        {
            if(charactersClasesDataList[i].playerCharacterType == characterType)
            {
                characterData = charactersClasesDataList[i];
            }
        }

        return characterData;
    }

    public void SetCurrentCharacterData(PlayerClasses playerClass)
    {
        for (int i = 0; i < charactersClasesDataList.Count; i++)
        {
            if (playerClass == charactersClasesDataList[i].playerCharacterType)
            {
                currentPlayerData = charactersClasesDataList[i];
                break;
            }
        }

        _playerCharactersManager.SetPlayCharacterModel(playerClass);
        _dataPersistanceManager.SaveGame();
    }

    private void CharacterBought_ExecuteReaction(PlayerClasses playerClass)
    {
        for (int i = 0; i < charactersClasesDataList.Count; i++)
        {
            if (playerClass == charactersClasesDataList[i].playerCharacterType)
            {
                PlayerBasicCharacteristicsStruct tempStruct = charactersClasesDataList[i];
                tempStruct.locked = false;
                charactersClasesDataList[i] = tempStruct;
                break;
            }
        }

        _dataPersistanceManager.SaveGame();
    }
}
