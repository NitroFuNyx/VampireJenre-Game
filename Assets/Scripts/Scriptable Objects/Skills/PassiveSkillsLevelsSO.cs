using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSkillsLevelsSO", menuName = "ScriptableObjects/PassiveSkillsLevelsSO")]
public class PassiveSkillsLevelsSO : ScriptableObject
{
    [Header("Passive Skills Upgrades")]
    [Space]
    [SerializeField] private List<PassiveSkillUpgradeDataStruct> increaseRangeUpgradesDataList = new List<PassiveSkillUpgradeDataStruct>();
    [SerializeField] private List<PassiveSkillUpgradeDataStruct> increaseDamageUpgradesDataList = new List<PassiveSkillUpgradeDataStruct>();
    [SerializeField] private List<PassiveSkillUpgradeDataStruct> increaseMovementSpeedUpgradesDataList = new List<PassiveSkillUpgradeDataStruct>();
    [SerializeField] private List<PassiveSkillUpgradeDataStruct> decreaseIncomeDamageUpgradesDataList = new List<PassiveSkillUpgradeDataStruct>();
    [SerializeField] private List<PassiveSkillUpgradeDataStruct> increaseRegenerationUpgradesDataList = new List<PassiveSkillUpgradeDataStruct>();
    [SerializeField] private List<PassiveSkillUpgradeDataStruct> increaseCritChanceUpgradesDataList = new List<PassiveSkillUpgradeDataStruct>();
    [SerializeField] private List<PassiveSkillUpgradeDataStruct> increaseCritPowerUpgradesDataList = new List<PassiveSkillUpgradeDataStruct>();
    [SerializeField] private List<PassiveSkillUpgradeDataStruct> increaseProjectilesAmountUpgradesDataList = new List<PassiveSkillUpgradeDataStruct>();

    public List<PassiveSkillUpgradeDataStruct> IncreaseRangeUpgradesDataList { get => increaseRangeUpgradesDataList; }
    public List<PassiveSkillUpgradeDataStruct> IncreaseDamageUpgradesDataList { get => increaseDamageUpgradesDataList; }
    public List<PassiveSkillUpgradeDataStruct> IncreaseMovementSpeedUpgradesDataList { get => increaseMovementSpeedUpgradesDataList; }
    public List<PassiveSkillUpgradeDataStruct> DecreaseIncomeDamageUpgradesDataList { get => decreaseIncomeDamageUpgradesDataList; }
    public List<PassiveSkillUpgradeDataStruct> IncreaseRegenerationUpgradesDataList { get => increaseRegenerationUpgradesDataList; }
    public List<PassiveSkillUpgradeDataStruct> IncreaseCritChanceUpgradesDataList { get => increaseCritChanceUpgradesDataList; }
    public List<PassiveSkillUpgradeDataStruct> IncreaseCritPowerUpgradesDataList { get => increaseCritPowerUpgradesDataList; }
    public List<PassiveSkillUpgradeDataStruct> IncreaseProjectilesAmountUpgradesDataList { get => increaseProjectilesAmountUpgradesDataList; }
}
