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

    public void StartGame()
    {
        movementManager.ChaneCanMoveState(true);
        collisionsManager.ResetComponent();
        collisionsManager.StartRegeneration();
    }

    private void SubscribeOnEvents()
    {
        collisionsManager.OnPlayerOutOfHp += CollisionManager_PlayerOutOfHp_ExecuteReaction;
        collisionsManager.OnDamageReceived += CollisionManager_DamageReceived_ExecuteReaction;

        _gameProcessManager.OnPlayerWon += GameProcessManager_PlayerWon_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        collisionsManager.OnPlayerOutOfHp -= CollisionManager_PlayerOutOfHp_ExecuteReaction;
        collisionsManager.OnDamageReceived -= CollisionManager_DamageReceived_ExecuteReaction;

        _gameProcessManager.OnPlayerWon -= GameProcessManager_PlayerWon_ExecuteReaction;
    }

    private void CollisionManager_PlayerOutOfHp_ExecuteReaction()
    {
        _gameProcessManager.GameLost_ExecuteReaction();
        movementManager.ChaneCanMoveState(false);
        movementManager.ResetComponent();
        collisionsManager.ResetComponent();
        collisionsManager.StopRegeneration();
    }

    private void CollisionManager_DamageReceived_ExecuteReaction()
    {
        _hapticManager.Vibrate();
    }

    private void GameProcessManager_PlayerWon_ExecuteReaction()
    {
        movementManager.ChaneCanMoveState(false);
        movementManager.ResetComponent();
        collisionsManager.ResetComponent();
        collisionsManager.StopRegeneration();
    }
}
