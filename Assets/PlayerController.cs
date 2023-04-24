using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [Header("Parametres")]
    [Range(0.1f, 10)] [SerializeField] private float speed;
    [Space]
    [Header("References")]
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private Animator playerAnimator;

    
    //TODO: CHECK GAME STATE
    private void Update()
    {
        playerRigidBody.velocity = new Vector3(joystick.Horizontal * speed, playerRigidBody.velocity.y
            , joystick.Vertical * speed);
        if (joystick.Vertical != 0 || joystick.Horizontal !=0)
        {
            transform.rotation = Quaternion.LookRotation(playerRigidBody.velocity);
            playerAnimator.SetBool(PlayerAnimations.IsRunning,true);
        }
        else 
            playerAnimator.SetBool(PlayerAnimations.IsRunning,false);

    }
}
