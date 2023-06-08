using UnityEngine;
using System;

public class EnemyCollisionsManager : MonoBehaviour
{
    [Header("Hp Data")]
    [Space]
    [SerializeField] private float startHp = 100f;
    [SerializeField] private float currentHp = 100f;

    private EnemiesCharacteristicsManager _enemiesCharacteristicsManager;

    private BossDataHolder bossDataHolder;

    private Collider _collider;
    private bool canCheckCollisions = true;
    private bool isBoss = false;
    private int startLayer;

    private float setStartCharacteristicsDelay = 1f;

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

    public void SetStandardLayer()
    {
        gameObject.layer = startLayer;
    }
    public void ApplyExplosion()
    {
        DecreaseHp(startHp);
    }

    public void CashExternalComponents(EnemiesCharacteristicsManager enemiesCharacteristicsManager, bool boss)
    {
        _enemiesCharacteristicsManager = enemiesCharacteristicsManager;
        isBoss = boss;

        if(!isBoss)
        {
            startHp = enemiesCharacteristicsManager.CurrentEnemiesData.hp;
        }
        else
        {
            startHp = bossDataHolder.StartHp;
        }

        currentHp = startHp;
    }

    public void UpdateCharacteristics(bool battleHasNotStarted)
    {
        if(!isBoss)
        {
            startHp = _enemiesCharacteristicsManager.CurrentEnemiesData.hp;
            if(battleHasNotStarted)
            {
                currentHp = startHp;
            }
        }
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
            if (collision.gameObject.layer == Layers.MeteorExplosion)
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
            if (collision.gameObject.layer == Layers.AuraSkill)
            {
                DecreaseHp(OnSkillCollision.Invoke(ActiveSkills.MagicAura));
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

        if(TryGetComponent(out BossDataHolder data))
        {
            bossDataHolder = data;
            startHp = bossDataHolder.StartHp;
            currentHp = startHp;
        }
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
