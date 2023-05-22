using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TalentsWheelDataSO", menuName = "ScriptableObjects/TalentsWheelDataSO", order = 2)]
public class TalentsWheelDataSO : ScriptableObject
{
    [Header("Rewards")]
    [Space]
    [SerializeField] private List<TalentDataStruct> talentsLists = new List<TalentDataStruct>();
    [SerializeField] private List<int> talentsLevelsList = new List<int>();

    public List<TalentDataStruct> TalentsLists { get => talentsLists; }
    public List<int> TalentsLevelsList { get => talentsLevelsList; set => talentsLevelsList = value; }
}
