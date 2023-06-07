using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovementManager : MonoBehaviour
{
    [Header("Move Data")]
    [Space]
    [SerializeField] private float startMoveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private int movementDecreasePrecent = 20;

    private PlayerController player;

    private Rigidbody rb;

    private EnemiesCharacteristicsManager _enemiesCharacteristicsManager;

    private float currentMoveSpeed;

    private bool canMove = false;

    private void Awake()
    {
        CashComponents();

        currentMoveSpeed = startMoveSpeed;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            rb.MovePosition(Vector3.Lerp(transform.position, targetPos, currentMoveSpeed * Time.fixedDeltaTime));
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), rotationSpeed * Time.fixedDeltaTime));
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
        float debuffValue = (currentMoveSpeed * movementDecreasePrecent) / 100;
        currentMoveSpeed -= debuffValue;

        if (currentMoveSpeed < 0f)
        {
            currentMoveSpeed = 0f;
        }
    }

    public void ResetMovementSpeed()
    {
        currentMoveSpeed = startMoveSpeed;
    }

    public void CashExternalComponents(EnemiesCharacteristicsManager enemiesCharacteristicsManager)
    {
        _enemiesCharacteristicsManager = enemiesCharacteristicsManager;
    }

    public void UpdateCharacteristics()
    {
        startMoveSpeed = _enemiesCharacteristicsManager.CurrentEnemiesData.hp;
        currentMoveSpeed = startMoveSpeed;
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
    }
}
