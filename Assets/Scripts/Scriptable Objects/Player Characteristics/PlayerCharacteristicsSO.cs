using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacteristicsSO", menuName = "ScriptableObjects/PlayerCharacteristicsSO", order = 3)]
public class PlayerCharacteristicsSO : ScriptableObject
{
    [Header("Player Default Characteristics")]
    [Space]
    [SerializeField] private List<PlayerBasicCharacteristicsStruct> playerBasicDataLists = new List<PlayerBasicCharacteristicsStruct>();
}
