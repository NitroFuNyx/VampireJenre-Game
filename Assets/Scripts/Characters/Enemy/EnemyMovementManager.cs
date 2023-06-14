using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovementManager : MonoBehaviour
{
    [Header("Move Data")]
    [Space]
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float enemyTypeSpeedBonusPercent;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private int movementDecreasePrecent = 20;

    private EnemiesCharacteristicsManager _enemiesCharacteristicsManager;

    private PlayerController player;

    private Rigidbody rb;

    private BossDataHolder bossDataHolder;

    private bool canMove = false;
    private bool isBoss;

    private float setStartCharacteristicsDelay = 1f;

    private void Awake()
    {
        CashComponents();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            //rb.MovePosition(Vector3.Lerp(transform.position, targetPos, currentMoveSpeed * Time.fixedDeltaTime));
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), rotationSpeed * Time.fixedDeltaTime));
            rb.MovePosition(transform.position + (player.transform.position - transform.position).normalized * currentMoveSpeed * Time.fixedDeltaTime);
        }
    }

    public void MoveToPlayer()
    {
        canMove = true;
    }

    public void StopMoving()
    {
        canMove = false;
    }

    public void DecreaseMovementSpeed()
    {
        float debuffValue = (currentMoveSpeed * movementDecreasePrecent) / CommonValues.maxPercentAmount;
        currentMoveSpeed -= debuffValue;

        if (currentMoveSpeed < 0f)
        {
            currentMoveSpeed = 0f;
        }
    }

    public void ResetMovementSpeed()
    {
        if(!isBoss)
        {
            currentMoveSpeed = _enemiesCharacteristicsManager.CurrentEnemiesData.speed + GetSpeedBonusValue();
        }
        else
        {
            currentMoveSpeed = bossDataHolder.MoveSpeed;
        }
    }

    public void CashExternalComponents(EnemiesCharacteristicsManager enemiesCharacteristicsManager, bool boss)
    {
        _enemiesCharacteristicsManager = enemiesCharacteristicsManager;
        isBoss = boss;

        UpdateCharacteristics();
    }

    public void UpdateCharacteristics()
    {
        if(!isBoss)
        {
            currentMoveSpeed = _enemiesCharacteristicsManager.CurrentEnemiesData.speed + GetSpeedBonusValue();
        }
    }

    private void CashComponents()
    {
        player = FindObjectOfType<PlayerController>();

        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rb = rigidbody;
        }
        else
        {
            Debug.LogWarning($"There is no Rigidbody component attached to {gameObject}", gameObject);
        }

        if (TryGetComponent(out BossDataHolder data))
        {
            bossDataHolder = data;
            currentMoveSpeed = bossDataHolder.MoveSpeed;
        }
    }

    private float GetSpeedBonusValue()
    {
        float percentValue = (_enemiesCharacteristicsManager.CurrentEnemiesData.speed * enemyTypeSpeedBonusPercent) / CommonValues.maxPercentAmount;

        return percentValue;
    }
}
