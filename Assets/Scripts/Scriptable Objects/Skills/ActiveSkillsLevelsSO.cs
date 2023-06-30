using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveSkillsLevelsSO", menuName = "ScriptableObjects/Skills/ActiveSkillsLevelsSO", order = 4)]
public class ActiveSkillsLevelsSO : ScriptableObject
{
    [Header("Active Skills Upgrades")]
    [Space]
    [SerializeField] private List<PlayerForceWaveSkillDataStruct> forceWaveUpgradesDataList = new List<PlayerForceWaveSkillDataStruct>();
    [Space]
    [SerializeField] private List<SingleShotSkillDataStruct> singleShotUpgradesDataList = new List<SingleShotSkillDataStruct>();
    [Space]
    [SerializeField] private List<MagicAuraSkillDataStruct> magicAuraUpgradesDataList = new List<MagicAuraSkillDataStruct>();
    [Space]
    [SerializeField] private List<PulseAuraSkillDataStruct> pulseAuraUpgradesDataList = new List<PulseAuraSkillDataStruct>();
    [Space]
    [SerializeField] private List<MeteorSkillDataStruct> meteorUpgradesDataList = new List<MeteorSkillDataStruct>();
    [Space]
    [SerializeField] private List<LightningBoltSkillDataStruct> lightningBoltUpgradesDataList = new List<LightningBoltSkillDataStruct>();
    [Space]
    [SerializeField] private List<ChainLightningSkillDataStruct> chainLightningUpgradesDataList = new List<ChainLightningSkillDataStruct>();
    [Space]
    [SerializeField] private List<FireballsSkillDataStruct> fireBallssUpgradesDataList = new List<FireballsSkillDataStruct>();
    [Space]
    [SerializeField] private List<AllDirectionsShotsSkillDataStruct> allDirectionsShotsUpgradesDataList = new List<AllDirectionsShotsSkillDataStruct>();
    [Space]
    [SerializeField] private List<WeaponStrikeSkillDataStruct> weaponStrikeUpgradesDataList = new List<WeaponStrikeSkillDataStruct>();

    public List<PlayerForceWaveSkillDataStruct> ForceWaveUpgradesDataList { get => forceWaveUpgradesDataList;  }
    public List<SingleShotSkillDataStruct> SingleShotUpgradesDataList { get => singleShotUpgradesDataList; }
    public List<MagicAuraSkillDataStruct> MagicAuraUpgradesDataList { get => magicAuraUpgradesDataList; }
    public List<PulseAuraSkillDataStruct> PulseAuraUpgradesDataList { get => pulseAuraUpgradesDataList; }
    public List<MeteorSkillDataStruct> MeteorUpgradesDataList { get => meteorUpgradesDataList; }
    public List<LightningBoltSkillDataStruct> LightningBoltUpgradesDataList { get => lightningBoltUpgradesDataList; }
    public List<ChainLightningSkillDataStruct> ChainLightningUpgradesDataList { get => chainLightningUpgradesDataList; }
    public List<FireballsSkillDataStruct> FireBallssUpgradesDataList { get => fireBallssUpgradesDataList; }
    public List<AllDirectionsShotsSkillDataStruct> AllDirectionsShotsUpgradesDataList { get => allDirectionsShotsUpgradesDataList; }
    public List<WeaponStrikeSkillDataStruct> WeaponStrikeUpgradesDataList { get => weaponStrikeUpgradesDataList; }
}
