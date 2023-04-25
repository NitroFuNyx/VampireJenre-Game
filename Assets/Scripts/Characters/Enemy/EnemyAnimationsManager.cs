using UnityEngine;

public class EnemyAnimationsManager : MonoBehaviour
{
    private Animator animator;

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

    public void SetAnimation_Moving()
    {
        animator.SetTrigger(EnemyAnimations.Move);
    }
}
