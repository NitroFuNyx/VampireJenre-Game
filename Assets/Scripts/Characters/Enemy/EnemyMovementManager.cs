using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovementManager : MonoBehaviour
{
    [Header("Move Data")]
    [Space]
    [SerializeField] private float moveVelocity = 10f;
    [SerializeField] private float rotationVelocity = 10f;

    private PlayerController player;

    private Rigidbody rb;

    private bool canMove = false;

    private void Awake()
    {
        CashComponents();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            rb.MovePosition(Vector3.Lerp(transform.position, targetPos, moveVelocity * Time.fixedDeltaTime));
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), rotationVelocity * Time.fixedDeltaTime));
        }
    }

    public void MoveToPlayer()
    {
        canMove = true;
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
