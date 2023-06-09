using UnityEngine;

public class EnemyComponentsManager : EnemyBehaviour
{
    [Header("Components")]
    [Space]
    [SerializeField] private EnemyAnimationsManager animationsManager;
    [Header("Enemy Type")]
    [Space]
    [SerializeField] private bool boss = false;
    [Header("VFX")]
    [Space]
    [SerializeField] private ParticleSystem getHitVfx;
    [Header("Positions")]
    [Space]
    [SerializeField] private Transform bloodPuddleVfxPos;

    private EnemyMovementManager movementManager;
    private EnemyCollisionsManager collisionsManager;

    private void Start()
    {
        SubscribeOnEvents();

        movementManager.CashExternalComponents(poolItemComponent._EnemiesCharacteristicsManager, boss);
        collisionsManager.CashExternalComponents(poolItemComponent._EnemiesCharacteristicsManager, boss);
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    public override void CashHeirComponents()
    {
        if (TryGetComponent(out EnemyMovementManager enemyMovementManager))
        {
            movementManager = enemyMovementManager;
        }
        if (TryGetComponent(out EnemyCollisionsManager enemyCollisionsManager))
        {
            collisionsManager = enemyCollisionsManager;
        }
    }

    private void SubscribeOnEvents()
    {
        if(poolItemComponent != null)
        {
            poolItemComponent.OnItemResetRequired += PoolItemComponent_ResetRequired_ExecuteReaction;
            poolItemComponent.OnObjectAwakeStateSet += PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction;

            poolItemComponent._EnemiesCharacteristicsManager.OnEnemyCharacteristicsUpgraded += EnemyCharacteristicsUpgraded_ExecuteReaction;
        }

        collisionsManager.OnCharacterOutOfHp += CollisionManager_CharacterOutOfHp_ExecuteReaction;
        collisionsManager.OnDamageReceived += CollisionManager_DamageReceived_ExecuteReaction;
        collisionsManager.OnSpeedDebuffCollision += CollisionManager_SpeedDebuffCollision_ExecuteReaction;
        collisionsManager.OnSpeedReset += CollisionManager_SpeedReset_ExecuteReaction;
        collisionsManager.OnSkillCollision += CollisionManager_SkillCollision_ExecuteReaction;

        animationsManager.OnDieAnimationFinished += AnimationManager_DieAnimationFinished_ExecuteReaction;
        animationsManager.OnGetHitAnimationFinished += AnimationManager_GetHitAnimationFinished_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        if (poolItemComponent != null)
        {
            poolItemComponent.OnItemResetRequired -= PoolItemComponent_ResetRequired_ExecuteReaction;
            poolItemComponent.OnObjectAwakeStateSet -= PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction;

            poolItemComponent._EnemiesCharacteristicsManager.OnEnemyCharacteristicsUpgraded -= EnemyCharacteristicsUpgraded_ExecuteReaction;
        }

        collisionsManager.OnCharacterOutOfHp -= CollisionManager_CharacterOutOfHp_ExecuteReaction;
        collisionsManager.OnDamageReceived -= CollisionManager_DamageReceived_ExecuteReaction;
        collisionsManager.OnSpeedDebuffCollision -= CollisionManager_SpeedDebuffCollision_ExecuteReaction;
        collisionsManager.OnSpeedReset -= CollisionManager_SpeedReset_ExecuteReaction;
        collisionsManager.OnSkillCollision -= CollisionManager_SkillCollision_ExecuteReaction;

        animationsManager.OnDieAnimationFinished -= AnimationManager_DieAnimationFinished_ExecuteReaction;
        animationsManager.OnGetHitAnimationFinished -= AnimationManager_GetHitAnimationFinished_ExecuteReaction;
    }

    #region Pool Item Component Events Reactions
    private void PoolItemComponent_ResetRequired_ExecuteReaction()
    {
        // reset enemy
        animationsManager.SetAnimation_Idle();
        collisionsManager.ResetComponent();
        collisionsManager.SetStandardLayer();
    }

    private void PoolItemComponent_ObjectAwakeStateSet_ExecuteReaction()
    {
        collisionsManager.ChangeColliderActivationState(true);
        movementManager.MoveToPlayer();
        animationsManager.SetAnimation_Moving();
        poolItemComponent.SpawnEnemiesManager.AddEnemyToOnMapList(this);
    }
    #endregion Pool Item Component Events Reactions

    #region Collision Manager Events Reaction
    private void CollisionManager_CharacterOutOfHp_ExecuteReaction()
    {
        movementManager.StopMoving();
        animationsManager.SetAnimation_Die();
        getHitVfx.Play();
        PoolItem bloodPuddleVfx = poolItemComponent.PoolItemsManager.SpawnItemFromPool(PoolItemsTypes.BloodPuddleVFX, transform.position,
                                                                                       Quaternion.identity, null);
        bloodPuddleVfx.SetObjectAwakeState();
        poolItemComponent.SpawnEnemiesManager.RemoveEnemyFronOnMapList(this);

        if (!boss)
        {
            poolItemComponent.PlayerExperienceManager.IncreaseXpValue(0.25f);
            poolItemComponent.PickableItemsManager.SpawnResourceForKillingEnemy(transform.position);
            poolItemComponent.GameProcessManager.IncreaseCurrentProgressValue();
        }
        else
        {
            Debug.Log($"Game Won");
            poolItemComponent.GameProcessManager.GameWin();
        }
    }

    private void CollisionManager_DamageReceived_ExecuteReaction()
    {
        animationsManager.SetAnimation_GetHit();
        movementManager.StopMoving();
        getHitVfx.Play();
    }

    private void CollisionManager_SpeedDebuffCollision_ExecuteReaction()
    {
        movementManager.DecreaseMovementSpeed();
    }

    private void CollisionManager_SpeedReset_ExecuteReaction()
    {
        movementManager.ResetMovementSpeed();
    }

    private float CollisionManager_SkillCollision_ExecuteReaction(ActiveSkills skill)
    {
        float value;
        if (skill == ActiveSkills.ForceWave)
            value = poolItemComponent.CharacteristicsManager.CurrentPlayerData.playerSkillsData.playerForceWaveData
                .damage;
        else if (skill == ActiveSkills.SingleShot)
            value = poolItemComponent.CharacteristicsManager.CurrentPlayerData.playerSkillsData.singleShotSkillData
                .damage;
        else if (skill == ActiveSkills.MagicAura)
            value = poolItemComponent.CharacteristicsManager.CurrentPlayerData.playerSkillsData.magicAuraSkillData
                .damage;
        else if (skill == ActiveSkills.PulseAura)
            value = poolItemComponent.CharacteristicsManager.CurrentPlayerData.playerSkillsData.pulseAuraSkillData
                .damage;
        else if (skill == ActiveSkills.Meteor)
            value = poolItemComponent.CharacteristicsManager.CurrentPlayerData.playerSkillsData.meteorSkillData
                .damage;
        else if (skill == ActiveSkills.LightningBolt)
            value = poolItemComponent.CharacteristicsManager.CurrentPlayerData.playerSkillsData.lightningBoltSkillData
                .damage;
        else if (skill == ActiveSkills.ChainLightning)
            value = poolItemComponent.CharacteristicsManager.CurrentPlayerData.playerSkillsData.chainLightningSkillData
                .damage;
        else if (skill == ActiveSkills.Fireballs)
            value = poolItemComponent.CharacteristicsManager.CurrentPlayerData.playerSkillsData.fireballsSkillData
                .damage;
        else if (skill == ActiveSkills.AllDirectionsShots)
            value = poolItemComponent.CharacteristicsManager.CurrentPlayerData.playerSkillsData.allDirectionsShotsSkillData
                .damage;
        else if (skill == ActiveSkills.WeaponStrike)
            value = poolItemComponent.CharacteristicsManager.CurrentPlayerData.playerSkillsData.weaponStrikeSkillData
                .damage;
        else
        {
            Debug.LogWarning($"Unknown type got caught during damage processing {skill}");
            value = 0;
        }

        float passiveSkillDamage = GetAdditionalDamageAmountFromPlayerPassiveSkill(value);
        float critDamage = GetCritDamage(value);

        Debug.Log($"Skill: {skill} damages: {value + passiveSkillDamage + critDamage}\n" +
                  $"Base Skill Damage {value}, From Passive Skill Damage {passiveSkillDamage} Crit Damage {critDamage}");
                    
        return value + GetAdditionalDamageAmountFromPlayerPassiveSkill(value) + GetCritDamage(value);
    }
   
    #endregion Collision Manager Events Reaction

    #region Animation Manager Events Reaction
    private void AnimationManager_DieAnimationFinished_ExecuteReaction()
    {
        poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
    }

    private void AnimationManager_GetHitAnimationFinished_ExecuteReaction()
    {
        movementManager.MoveToPlayer();
    }
    #endregion Animation Manager Events Reaction

    #region Enemies Characteristics Manager Events Reaction
    private void EnemyCharacteristicsUpgraded_ExecuteReaction(bool battleHasNotStarted)
    {
        if(!boss)
        {
            collisionsManager.UpdateCharacteristics(battleHasNotStarted);
            movementManager.UpdateCharacteristics();
        }
    }
    #endregion Enemis Characteristics Manager Events Reaction

    private float GetAdditionalDamageAmountFromPlayerPassiveSkill(float skillDamage)
    {
        float damageAmount = (poolItemComponent.CharacteristicsManager.CurrentPlayerData.characterDamageIncreasePercent * skillDamage) / 
                              CommonValues.maxPercentAmount;
        return damageAmount;
    }

    private float GetCritDamage(float skillDamage)
    {
        float critDamage = 0f;

        float critIndex = Random.Range(0, CommonValues.maxPercentAmount);

        if(critIndex < poolItemComponent.CharacteristicsManager.CurrentPlayerData.characterCritChance)
        {
            critDamage = skillDamage * poolItemComponent.CharacteristicsManager.CurrentPlayerData.characterCritPower;
        }

        return critDamage;
    }
}
