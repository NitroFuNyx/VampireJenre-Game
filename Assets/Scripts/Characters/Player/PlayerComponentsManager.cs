using UnityEngine;
using Zenject;

public class PlayerComponentsManager : MonoBehaviour
{
    private HapticManager _hapticManager;
    private GameProcessManager _gameProcessManager;

    private PlayerCollisionsManager collisionsManager;
    private PlayerController movementManager;

    private void Awake()
    {
        collisionsManager = GetComponent<PlayerCollisionsManager>();
        movementManager = GetComponent<PlayerController>();
    }

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(HapticManager hapticManager, GameProcessManager gameProcessManager)
    {
        _hapticManager = hapticManager;
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    private void SubscribeOnEvents()
    {
        collisionsManager.OnPlayerOutOfHp += CollisionManager_PlayerOutOfHp_ExecuteReaction;
        collisionsManager.OnDamageReceived += CollisionManager_DamageReceived_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        collisionsManager.OnPlayerOutOfHp -= CollisionManager_PlayerOutOfHp_ExecuteReaction;
        collisionsManager.OnDamageReceived -= CollisionManager_DamageReceived_ExecuteReaction;
    }

    private void CollisionManager_PlayerOutOfHp_ExecuteReaction()
    {
        movementManager.ChaneCanMoveState(false);
        _gameProcessManager.GameLost_ExecuteReaction();
    }

    private void CollisionManager_DamageReceived_ExecuteReaction()
    {
        _hapticManager.Vibrate();
    }
}
