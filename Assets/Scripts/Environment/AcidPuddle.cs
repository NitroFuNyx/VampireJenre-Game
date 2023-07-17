using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    [Header("Damage Data")]
    [Space]
    [SerializeField] private float damageAmountToPlayer = 5f;

    public float DamageAmountToPlayer { get => damageAmountToPlayer; }
}
