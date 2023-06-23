using UnityEngine;
using Zenject;

public class PlayerComponentsManager : MonoBehaviour
{
    private HapticManager _hapticManager;
    private GameProcessManager _gameProcessManager;

    private PlayerCollisionsManager collisionsManager;
    private PlayerController movementManager;
    private PlayerCharactersManager charactersManager;

    private void Awake()
    {
        collisionsManager = GetComponent<PlayerCollisionsManager>();
        movementManager = GetComponent<PlayerController>();
        charactersManager = GetComponent<PlayerCharactersManager>();
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
        movementManager.ChangeCanMoveState(true);
        collisionsManager.ResetComponent();
        collisionsManager.StartRegeneration();
        movementManager.SetAnimation_Idle();
    }

    private void SubscribeOnEvents()
    {
        collisionsManager.OnPlayerOutOfHp += CollisionManager_PlayerOutOfHp_ExecuteReaction;
        collisionsManager.OnDamageReceived += CollisionManager_DamageReceived_ExecuteReaction;

        _gameProcessManager.OnPlayerWon += GameProcessManager_PlayerWon_ExecuteReaction;
        _gameProcessManager.OnLevelDataReset += GameProcessManager_LevelDataReset_ExecuteReaction;
        _gameProcessManager.OnPlayerRecoveryOptionUsed += GameProcessManager_OnPlayerRecoveryOptionUsed_ExecuteReaction;

        charactersManager.OnPlayerModelChanged += CharactersManager_OnPlayerModelChanged_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        collisionsManager.OnPlayerOutOfHp -= CollisionManager_PlayerOutOfHp_ExecuteReaction;
        collisionsManager.OnDamageReceived -= CollisionManager_DamageReceived_ExecuteReaction;

        _gameProcessManager.OnPlayerWon -= GameProcessManager_PlayerWon_ExecuteReaction;
        _gameProcessManager.OnLevelDataReset -= GameProcessManager_LevelDataReset_ExecuteReaction;
        _gameProcessManager.OnPlayerRecoveryOptionUsed -= GameProcessManager_OnPlayerRecoveryOptionUsed_ExecuteReaction;

        charactersManager.OnPlayerModelChanged -= CharactersManager_OnPlayerModelChanged_ExecuteReaction;
    }

    private void CollisionManager_PlayerOutOfHp_ExecuteReaction()
    {
        _gameProcessManager.GameLost_ExecuteReaction();
        movementManager.ChangeCanMoveState(false);

        collisionsManager.StopRegeneration();

        movementManager.SetAnimation_Die();
    }

    private void CollisionManager_DamageReceived_ExecuteReaction()
    {
        _hapticManager.Vibrate();
    }

    private void GameProcessManager_OnPlayerRecoveryOptionUsed_ExecuteReaction()
    {
        StartGame();
    }

    private void GameProcessManager_PlayerWon_ExecuteReaction()
    {
        movementManager.ChangeCanMoveState(false);

        collisionsManager.StopRegeneration();
    }

    private void GameProcessManager_LevelDataReset_ExecuteReaction()
    {
        movementManager.ResetComponent();
        collisionsManager.ResetComponent();
        movementManager.SetAnimation_Idle();
    }

    private void CharactersManager_OnPlayerModelChanged_ExecuteReaction(Animator animator)
    {
        movementManager.SetAnimator(animator);
    }
}
