using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    //[Header("Parametres")]
    //[Range(0.1f, 10)] [SerializeField] private float speed;
    [Space]
    [Header("References")]
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private Animator playerAnimator;

    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    private bool canMove = true;

    [SerializeField] private Transform playerTargetPosition;

    public Transform PlayerTargetPosition => playerTargetPosition;

    //TODO: CHECK GAME STATE
    private void FixedUpdate()
    {
        if(canMove)
        {
            playerRigidBody.velocity = new Vector3(joystick.Horizontal * _playerCharacteristicsManager.CurrentPlayerData.characterSpeed, 
                                      playerRigidBody.velocity.y, joystick.Vertical * _playerCharacteristicsManager.CurrentPlayerData.characterSpeed);

            if (joystick.Vertical != 0 || joystick.Horizontal != 0)
            {
                transform.rotation = Quaternion.LookRotation(playerRigidBody.velocity);
                playerAnimator.SetBool(PlayerAnimations.IsRunning, true);
            }
            else
            {
                playerAnimator.SetBool(PlayerAnimations.IsRunning, false);
            }
        }
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerCharacteristicsManager playerCharacteristicsManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
    }
    #endregion Zenject

    public void ChangeCanMoveState(bool canMove)
    {
        this.canMove = canMove;
    }

    public void CashExternalComponents()
    {

    }

    public void ResetComponent()
    {
        transform.position = new Vector3(0f, transform.position.y, 0f);
        transform.rotation = Quaternion.identity;
    }

    public void SetAnimator(Animator animator)
    {
        playerAnimator = animator;
    }
}
