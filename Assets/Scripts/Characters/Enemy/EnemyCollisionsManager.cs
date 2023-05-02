using UnityEngine;
using System;

public class EnemyCollisionsManager : MonoBehaviour
{
    [Header("Hp Data")]
    [Space]
    [SerializeField] private int startHp = 100;
    [SerializeField] private int currentHp = 100;

    private Collider _collider;

    private bool canCheckCollisions = true;

    #region Events Declaration
    public event Action OnPlayerOutOfHp;
    public event Action OnDamageReceived;
    #endregion Events Declaration

    private void Awake()
    {
        CashComponents();
        ChangeColliderActivationState(false);
    }

    private void Start()
    {
        SetStartSettings();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(canCheckCollisions)
        {
            if (collision.gameObject.layer == Layers.PlayerSkillProjectile)
            {
                DecreaseHp(startHp);
            }
        }    
    }

    public void ChangeColliderActivationState(bool enabled)
    {
        if (_collider != null)
        {
            _collider.enabled = enabled;
        }
    }

    public void ResetComponent()
    {
        canCheckCollisions = true;
        ChangeColliderActivationState(false);
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

    private void SetStartSettings()
    {
        currentHp = startHp;
    }

    private void DecreaseHp(int amount)
    {
        currentHp -= amount;
        if (currentHp <= 0)
        {
            canCheckCollisions = false;
            OnPlayerOutOfHp?.Invoke();
        }
        else
        {
            OnDamageReceived?.Invoke();
        }
    }
}
