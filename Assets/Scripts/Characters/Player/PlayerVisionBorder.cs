using UnityEngine;
using Zenject;

public class PlayerVisionBorder : MonoBehaviour
{
    private PlayerCollisionsManager _playerCollisionsManager;
    private Transform player;

    private BoxCollider boxCollider;
    private Rigidbody rb;

    public BoxCollider BoxCollider { get => boxCollider; }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        player = _playerCollisionsManager.transform;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(player.position);
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerCollisionsManager playerCollisionsManager)
    {
        _playerCollisionsManager = playerCollisionsManager;
    }
    #endregion Zenject
}
