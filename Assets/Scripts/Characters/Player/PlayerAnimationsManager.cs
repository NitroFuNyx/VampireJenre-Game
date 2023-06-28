using System;
using UnityEngine;

public class PlayerAnimationsManager : MonoBehaviour
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private Animator animator;

    public event Action OnDieAnimationFinished;

    public void SetAnimator(Animator animatorComponent)
    {
        animator = animatorComponent;
    }

    public void SetRunningAnimationState(bool isRunning)
    {
        animator.SetBool(PlayerAnimations.IsRunning, isRunning);
    }

    public void SetAnimation_Idle()
    {
        animator.SetTrigger(PlayerAnimations.Idle);
    }

    public void SetAnimation_Die()
    {
        animator.SetTrigger(PlayerAnimations.Die);
    }

    public void SetAnimationState_DieAnimationFinished()
    {
        OnDieAnimationFinished?.Invoke();
    }
}
