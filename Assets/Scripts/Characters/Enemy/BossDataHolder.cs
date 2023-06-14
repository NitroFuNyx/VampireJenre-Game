using UnityEngine;

public class BossDataHolder : MonoBehaviour
{
    [Header("Boss Data")]
    [Space]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float startHp;

    public float MoveSpeed { get => moveSpeed; }
    public float StartHp { get => startHp; }
}
