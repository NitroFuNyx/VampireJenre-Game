using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ForceWaveLevelsSO", menuName = "ScriptableObjects/ForceWaveLevelsSO", order = 4)]
public class ForceWaveLevelsSO : ScriptableObject
{
    [Header("Player Default Characteristics")]
    [Space]
    [SerializeField] private List<PlayerForceWaveSkillDataStruct> forceWaveUpgradesDataList = new List<PlayerForceWaveSkillDataStruct>();

    public List<PlayerForceWaveSkillDataStruct> ForceWaveUpgradesDataList { get => forceWaveUpgradesDataList;  }
}
