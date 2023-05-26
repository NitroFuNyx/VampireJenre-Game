using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class EnemyCollisionsManager : MonoBehaviour
{
    [Header("Hp Data")]
    [Space]
    [SerializeField] private int startHp = 100;
    [SerializeField] private int currentHp = 100;
    
    private Collider _collider;
    private bool canCheckCollisions = true;
    private int startLayer;
    [SerializeField] private float auraCooldown=1;//Delete
    
    #region Events Declaration
    public event Action OnCharacterOutOfHp;
    public event Action OnDamageReceived;

    public event Action OnSpeedDebuffCollision;
    public event Action OnSpeedReset;
    #endregion Events Declaration

    private void Awake()
    {
        startLayer = gameObject.layer;
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

    public void SetStandardLayer()
    {
        gameObject.layer = startLayer;
    }
    public void ApplyExplosion()
    {
        DecreaseHp(startHp);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (canCheckCollisions)
        {
            if (collision.gameObject.layer == Layers.SkillArea)
            {

                OnSpeedDebuffCollision?.Invoke();
            }

            if (collision.gameObject.layer == Layers.PlayerSkillProjectile)
            {
                DecreaseHp(startHp);
            }

            if (collision.gameObject.layer == Layers.FireballSkill)
            {
                DecreaseHp(startHp);
            }

            if (collision.gameObject.layer == Layers.ChainLightning)
            {
                DecreaseHp(startHp);
            }

            if (collision.gameObject.layer == Layers.NovaSkill)
            {
                DecreaseHp(startHp);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Layers.AuraSkill)
        {
            auraCooldown += Time.fixedDeltaTime;
            if (auraCooldown > 0.1f)
            {
                DecreaseHp(startHp);

                auraCooldown = 0;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == Layers.SkillArea)
        {

            OnSpeedReset?.Invoke();
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
        currentHp = startHp;
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
            gameObject.layer = Layers.DeadEnemy;
            OnCharacterOutOfHp?.Invoke();
        }
        else
        {
            OnDamageReceived?.Invoke();
        }
    }
}
