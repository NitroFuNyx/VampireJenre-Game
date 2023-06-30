using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacteristicsSO", menuName = "ScriptableObjects/Player/PlayerCharacteristicsSO")]
public class PlayerCharacteristicsSO : ScriptableObject
{
    [Header("Player Default Characteristics")]
    [Space]
    [SerializeField] private List<PlayerBasicCharacteristicsStruct> playerBasicDataLists = new List<PlayerBasicCharacteristicsStruct>();

    public List<PlayerBasicCharacteristicsStruct> PlayerBasicDataLists { get => playerBasicDataLists; }
}
