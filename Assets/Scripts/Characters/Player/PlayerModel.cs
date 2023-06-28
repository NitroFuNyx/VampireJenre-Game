using UnityEngine;
using Zenject;

public class PlayerModel : MonoBehaviour
{
    [Header("Model Data")]
    [Space]
    [SerializeField] private PlayerClasses modelType;

    private PlayerAnimationsManager _animationsManager;

    private Animator animatorComponent;

    public PlayerClasses ModelType { get => modelType; }
    public Animator AnimatorComponent { get => animatorComponent; }

    private void Awake()
    {
        animatorComponent = GetComponent<Animator>();
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerAnimationsManager playerAnimationsManager)
    {
        _animationsManager = playerAnimationsManager;
    }
    #endregion Zenject

    public void FinishDieAnimation()
    {
        _animationsManager.SetAnimationState_DieAnimationFinished();
    }
}
