using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class EnemyCollisionsManager : MonoBehaviour
{
    [Header("Hp Data")]
    [Space]
    [SerializeField] private float startHp = 100f;
    [SerializeField] private float currentHp = 100f;
    
    private Collider _collider;
    private bool canCheckCollisions = true;
    private int startLayer;
    [SerializeField] private float auraCooldown=1;//Delete
    
    #region Events Declaration
    public event Action OnCharacterOutOfHp;
    public event Action OnDamageReceived;

    public event Action OnSpeedDebuffCollision;
    public event Action OnSpeedReset;

    public event Func<ActiveSkills,float> OnSkillCollision; 
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
            if (collision.gameObject.layer == Layers.MeteorPuddle)
            {

                OnSpeedDebuffCollision?.Invoke();
            }

            if (collision.gameObject.layer == Layers.SingleShotSkill)
            {
                DecreaseHp(OnSkillCollision.Invoke(ActiveSkills.SingleShot));
            }
            if (collision.gameObject.layer == Layers.FireballSkill)
            {
                DecreaseHp(OnSkillCollision.Invoke(ActiveSkills.Fireballs));
            }
            if (collision.gameObject.layer == Layers.ChainLightning)
            {
                DecreaseHp(OnSkillCollision.Invoke(ActiveSkills.ChainLightning));
            }
            if (collision.gameObject.layer == Layers.PulseAuraSkill)
            {
                DecreaseHp(OnSkillCollision.Invoke(ActiveSkills.PulseAura));
            }
            if (collision.gameObject.layer == Layers.MeteorSkill)
            {
                DecreaseHp(OnSkillCollision.Invoke(ActiveSkills.Meteor));
            }
            if (collision.gameObject.layer == Layers.WeaponStrikeSkill)
            {
                DecreaseHp(OnSkillCollision.Invoke(ActiveSkills.WeaponStrike));
            }
            if (collision.gameObject.layer == Layers.MissilesSkill)
            {
                DecreaseHp(OnSkillCollision.Invoke(ActiveSkills.AllDirectionsShots));
            }
            if (collision.gameObject.layer == Layers.LightningBolt)
            {
                DecreaseHp(OnSkillCollision.Invoke(ActiveSkills.LightningBolt));
            }
            if (collision.gameObject.layer == Layers.ForceWave)
            {
                DecreaseHp(OnSkillCollision.Invoke(ActiveSkills.ForceWave));
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
                DecreaseHp(OnSkillCollision.Invoke(ActiveSkills.MagicAura));

                auraCooldown = 0;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == Layers.MeteorPuddle)
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

    private void DecreaseHp(float amount)
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
