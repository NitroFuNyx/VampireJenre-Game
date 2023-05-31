using UnityEngine;
using Zenject;

public class PlayerVisionBorder : MonoBehaviour
{
    private PlayerCollisionsManager _playerCollisionsManager;
    private Transform player;

    private BoxCollider boxCollider;

    public BoxCollider BoxCollider { get => boxCollider; }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        player = _playerCollisionsManager.transform;
    }

    private void Update()
    {
        transform.position = player.position;
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerCollisionsManager playerCollisionsManager)
    {
        _playerCollisionsManager = playerCollisionsManager;
    }
    #endregion Zenject
}
