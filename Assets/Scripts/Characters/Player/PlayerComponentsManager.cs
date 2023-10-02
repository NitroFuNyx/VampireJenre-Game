using UnityEngine;
using Zenject;

public class PlayerComponentsManager : MonoBehaviour
{
    private HapticManager _hapticManager;
    private GameProcessManager _gameProcessManager;

    private PlayerCollisionsManager collisionsManager;
    private PlayerController movementManager;
    private PlayerCharactersManager charactersManager;
    private PlayerAnimationsManager animationsManager;

    private void Awake()
    {
        collisionsManager = GetComponent<PlayerCollisionsManager>();
        movementManager = GetComponent<PlayerController>();
        charactersManager = GetComponent<PlayerCharactersManager>();
        animationsManager = GetComponent<PlayerAnimationsManager>();
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
        collisionsManager.SetCanCheckCollisionsState(true);
        animationsManager.SetAnimation_Idle();
    }

    private void SubscribeOnEvents()
    {
        collisionsManager.OnPlayerOutOfHp += CollisionManager_PlayerOutOfHp_ExecuteReaction;
        collisionsManager.OnDamageReceived += CollisionManager_DamageReceived_ExecuteReaction;
        //collisionsManager.OnStairsColliderEnter += CollisionManager_StairsColliderEnter_ExecuteReaction;
        //collisionsManager.OnStairsColliderExit += CollisionManager_StairsColliderExit_ExecuteReaction;

        _gameProcessManager.OnPlayerWon += GameProcessManager_PlayerWon_ExecuteReaction;
        _gameProcessManager.OnLevelDataReset += GameProcessManager_LevelDataReset_ExecuteReaction;
        _gameProcessManager.OnPlayerRecoveryOptionUsed += GameProcessManager_OnPlayerRecoveryOptionUsed_ExecuteReaction;

        charactersManager.OnPlayerModelChanged += CharactersManager_OnPlayerModelChanged_ExecuteReaction;

        animationsManager.OnDieAnimationFinished += AnimationManager_DieAnimationFinished_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        collisionsManager.OnPlayerOutOfHp -= CollisionManager_PlayerOutOfHp_ExecuteReaction;
        collisionsManager.OnDamageReceived -= CollisionManager_DamageReceived_ExecuteReaction;
        //collisionsManager.OnStairsColliderEnter -= CollisionManager_StairsColliderEnter_ExecuteReaction;
        //collisionsManager.OnStairsColliderExit -= CollisionManager_StairsColliderExit_ExecuteReaction;

        _gameProcessManager.OnPlayerWon -= GameProcessManager_PlayerWon_ExecuteReaction;
        _gameProcessManager.OnLevelDataReset -= GameProcessManager_LevelDataReset_ExecuteReaction;
        _gameProcessManager.OnPlayerRecoveryOptionUsed -= GameProcessManager_OnPlayerRecoveryOptionUsed_ExecuteReaction;

        charactersManager.OnPlayerModelChanged -= CharactersManager_OnPlayerModelChanged_ExecuteReaction;

        animationsManager.OnDieAnimationFinished -= AnimationManager_DieAnimationFinished_ExecuteReaction;
    }

    private void CollisionManager_PlayerOutOfHp_ExecuteReaction()
    {
        if(_gameProcessManager.CurrentGameMode == GameModes.Standart)
        {
            if (!_gameProcessManager.GameVictoryResultIsBeingShown)
            {
                _gameProcessManager.GameDefeatResultIsBeingShown = true;

                movementManager.ChangeCanMoveState(false);
                collisionsManager.StopRegeneration();
                collisionsManager.SetCanCheckCollisionsState(false);
                animationsManager.SetAnimation_Die();
            }
        }
        else if(_gameProcessManager.CurrentGameMode == GameModes.Deathmatch)
        {
            movementManager.ChangeCanMoveState(false);
            collisionsManager.StopRegeneration();
            collisionsManager.SetCanCheckCollisionsState(false);
            animationsManager.SetAnimation_Die();
        }
    }

    private void CollisionManager_DamageReceived_ExecuteReaction()
    {
        _hapticManager.Vibrate();
    }

    private void CollisionManager_StairsColliderEnter_ExecuteReaction()
    {
        movementManager.ChangeRigidBodyConstraintY(true);
    }

    private void CollisionManager_StairsColliderExit_ExecuteReaction()
    {
        movementManager.ChangeRigidBodyConstraintY(false);
    }

    private void GameProcessManager_OnPlayerRecoveryOptionUsed_ExecuteReaction()
    {
        _gameProcessManager.GameDefeatResultIsBeingShown = false;
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
        animationsManager.SetAnimation_Idle();
    }

    private void CharactersManager_OnPlayerModelChanged_ExecuteReaction(Animator animator)
    {
        animationsManager.SetAnimator(animator);
    }

    private void AnimationManager_DieAnimationFinished_ExecuteReaction()
    {
        _gameProcessManager.GameLost_ExecuteReaction();
    }
}
