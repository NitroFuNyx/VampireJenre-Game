using UnityEngine;
using System;

public class EnemyAnimationsManager : MonoBehaviour
{
    private Animator animator;

    #region Events Declaration
    public event Action OnDieAnimationFinished;
    public event Action OnGetHitAnimationFinished;
    #endregion Events Declaration

    private void Awake()
    {
        if(TryGetComponent(out Animator animatorComponent))
        {
            animator = animatorComponent;
        }
        else
        {
            Debug.LogError($"There is no Animator component attached to {gameObject}");
        }
    }

    public void SetAnimation_Idle()
    {
        animator.SetTrigger(EnemyAnimations.Idle);
    }

    public void SetAnimation_Moving()
    {
        animator.SetTrigger(EnemyAnimations.Move);
    }

    public void SetAnimation_Die()
    {
        animator.SetTrigger(EnemyAnimations.Die);
    }

    public void SetAnimation_GetHit()
    {
        animator.SetTrigger(EnemyAnimations.GetHit);
    }

    public void AnimationEvent_DieAnimationFinished_ExecuteReaction()
    {
        OnDieAnimationFinished?.Invoke();
    }

    public void AnimationEvent_GetHitAnimationFinished_ExecuteReaction()
    {
        OnGetHitAnimationFinished?.Invoke();
    }
}
