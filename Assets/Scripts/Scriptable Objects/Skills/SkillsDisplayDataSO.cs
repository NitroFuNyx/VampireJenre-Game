using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillsDisplayDataSO", menuName = "ScriptableObjects/Skills/SkillsDisplayDataSO")]
public class SkillsDisplayDataSO : ScriptableObject
{
    [Header("Active Skills Display Data")]
    [Space]
    [SerializeField] private List<ActiveSkillsDisplayDataStruct> activeSkillsDisplayDataList = new List<ActiveSkillsDisplayDataStruct>();
    [Header("Passive Skills Display Data")]
    [Space]
    [SerializeField] private List<PassiveSkillsDisplayDataStruct> passiveSkillsDisplayDataList = new List<PassiveSkillsDisplayDataStruct>();

    public List<ActiveSkillsDisplayDataStruct> ActiveSkillsDisplayDataList { get => activeSkillsDisplayDataList; }
    public List<PassiveSkillsDisplayDataStruct> PassiveSkillsDisplayDataList { get => passiveSkillsDisplayDataList; }
}
