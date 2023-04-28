using UnityEngine;

public class EnemyCollisionsManager : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        CashComponents();
        ChangeColliderActivationState(false);
    }

    public void ChangeColliderActivationState(bool enabled)
    {
        if (_collider != null)
        {
            _collider.enabled = enabled;
        }
    }

    private void CashComponents()
    {
        if (TryGetComponent(out Collider characterCollider))
        {
            _collider = characterCollider;
        }
        else
        {
            Debug.LogWarning($"There is no Collider component attached to {gameObject}", gameObject);
        }
    }
}
