using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TalentsWheelDataSO", menuName = "ScriptableObjects/TalentsWheelDataSO", order = 2)]
public class TalentsWheelDataSO : ScriptableObject
{
    [Header("Rewards")]
    [Space]
    [SerializeField] private List<TalentDataStruct> talentsLists = new List<TalentDataStruct>();

    public List<TalentDataStruct> TalentsLists { get => talentsLists; }
}
