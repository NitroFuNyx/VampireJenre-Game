using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerClassesDataSO", menuName = "ScriptableObjects/Player/PlayerClassesDataSO")]
public class PlayerClassesDataSO : ScriptableObject
{
    [Header("Classes Data")]
    [Space]
    [SerializeField] private List<PlayerClassDataStruct> playerClassesDataList = new List<PlayerClassDataStruct>();

    public List<PlayerClassDataStruct> PlayerClassesDataList { get => playerClassesDataList; }
}
