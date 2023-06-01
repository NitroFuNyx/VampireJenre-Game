using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkilllsDescribtionTextsTranslationSO", menuName = "ScriptableObjects/SkilllsDescribtionTextsTranslationSO")]
public class SkilllsDescribtionTextsTranslationSO : ScriptableObject
{
    [Header("Active Skills")]
    [Space]
    [SerializeField] private List<ActiveSkillsTranslationDataStruct> forceWaveTranslationData = new List<ActiveSkillsTranslationDataStruct>();
    [SerializeField] private List<ActiveSkillsTranslationDataStruct> singleShotTranslationData = new List<ActiveSkillsTranslationDataStruct>();
    [SerializeField] private List<ActiveSkillsTranslationDataStruct> magicAuraTranslationData = new List<ActiveSkillsTranslationDataStruct>();
    [SerializeField] private List<ActiveSkillsTranslationDataStruct> pulseAuraTranslationData = new List<ActiveSkillsTranslationDataStruct>();
    [SerializeField] private List<ActiveSkillsTranslationDataStruct> meteorTranslationData = new List<ActiveSkillsTranslationDataStruct>();
    [SerializeField] private List<ActiveSkillsTranslationDataStruct> lightningBoltTranslationData = new List<ActiveSkillsTranslationDataStruct>();
    [SerializeField] private List<ActiveSkillsTranslationDataStruct> chainLightningTranslationData = new List<ActiveSkillsTranslationDataStruct>();
    [SerializeField] private List<ActiveSkillsTranslationDataStruct> fireballsTranslationData = new List<ActiveSkillsTranslationDataStruct>();
    [SerializeField] private List<ActiveSkillsTranslationDataStruct> allDiractionsTranslationData = new List<ActiveSkillsTranslationDataStruct>();
    [SerializeField] private List<ActiveSkillsTranslationDataStruct> weaponStrikeTranslationData = new List<ActiveSkillsTranslationDataStruct>();
    [Header("Passive Skills")]
    [Space]
    [SerializeField] private PassiveSkillsTranslationDataStruct increaseRangeTranslationData;
    [SerializeField] private PassiveSkillsTranslationDataStruct increaseDamageTranslationData;
    [SerializeField] private PassiveSkillsTranslationDataStruct increaseMovementSpeedTranslationData;
    [SerializeField] private PassiveSkillsTranslationDataStruct decreaseIncomeDamageTranslationData;
    [SerializeField] private PassiveSkillsTranslationDataStruct increaseRegenerationTranslationData;
    [SerializeField] private PassiveSkillsTranslationDataStruct increaseCritChanceTranslationData;
    [SerializeField] private PassiveSkillsTranslationDataStruct increaseCritPowerTranslationData;
    [SerializeField] private PassiveSkillsTranslationDataStruct increaseProjectileAmountTranslationData;
}
