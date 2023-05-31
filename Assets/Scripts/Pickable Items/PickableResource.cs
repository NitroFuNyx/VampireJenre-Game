using UnityEngine;

public class PickableResource : PickableItem
{
    [Header("Resource Data")]
    [Space]
    [SerializeField] private ResourceBonusItemStruct resourceBonusData;

    private Rigidbody rb;

    private Transform player;

    private bool canMove = false;

    private float moveSpeed = 2f;

    private void Start()
    {
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rb = rigidbody;
        }
        else
        {
            Debug.LogWarning($"There is no Rigidbody component attached to {gameObject}", gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            rb.MovePosition(Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime));
        }
    }

    public void SetResourceData(ResourceBonusItemStruct resourceData)
    {
        resourceBonusData = resourceData;
    }

    public void MoveToPlayer(Transform player)
    {
        canMove = true;
        this.player = player;
    }

    protected override void PlayerCollision_ExecuteReaction()
    {
        canMove = false;
        poolItemComponent.ResourcesManager.AddResourceForKillingEnemy(resourceBonusData);
        poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
    }

    protected override void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        itemPickedUp = false;
    }
}
