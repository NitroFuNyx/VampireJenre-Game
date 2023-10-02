using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacteristicsSO", menuName = "ScriptableObjects/Player/PlayerCharacteristicsSO")]
public class PlayerCharacteristicsSO : ScriptableObject
{
    [Header("Player Default Characteristics")]
    [Space]
    [SerializeField] private List<PlayerBasicCharacteristicsStruct> playerBasicDataLists = new List<PlayerBasicCharacteristicsStruct>();
    [Header("Start Standart Values")]
    [Space]
    [SerializeField] private float startStandartSpeed = 4.5f;
    [SerializeField] private float startStandartDamage = 5f;
    [SerializeField] private float startStandartHealth = 100f;

    public List<PlayerBasicCharacteristicsStruct> PlayerBasicDataLists { get => playerBasicDataLists; }
    public float StartStandartSpeed { get => startStandartSpeed; }
    public float StartStandartDamage { get => startStandartDamage; }
    public float StartStandartHealth { get => startStandartHealth; }
}
