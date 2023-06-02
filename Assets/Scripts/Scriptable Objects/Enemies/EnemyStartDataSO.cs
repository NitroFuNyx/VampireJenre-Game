using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStartDataSO", menuName = "ScriptableObjects/Enemies/EnemyStartDataSO")]
public class EnemyStartDataSO : ScriptableObject
{
    [Header("Enemy Start Data")]
    [Space]
    [SerializeField] private EnemyDataStruct enemyStartData;

    public EnemyDataStruct EnemyStartData { get => enemyStartData; }
}
