
using System.Collections;
using UnityEngine;
using Zenject;

public class ChainLightningProjectileSpawner : ProjectileSpawnerBase
{
    [Range(0, 1)] [SerializeField] protected float skillCooldownBetweenShots;
    [SerializeField] private int maxProjectileSpawnerCount = 0;
    [SerializeField] private TargetsHolder targetsHolder;

    private PoolItemsManager _poolmanager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    [Inject]
    private void InjectDependencies(PoolItemsManager poolmanager,PlayerCharacteristicsManager playerCharacteristicsManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
        _poolmanager = poolmanager;
    }

    private void OnEnable()
    {
        StartCoroutine(SpawningProjectile());
    }

    protected override IEnumerator SpawningProjectile()
    {
        while (true)
        {
            StartCoroutine(SettingUpProjectile());

            yield return new WaitForSecondsRealtime(_playerCharacteristicsManager.CurrentPlayerData.playerSkillsData
                .chainLightningSkillData.cooldown);        }
    }
    protected override IEnumerator SettingUpProjectile()
    {
        int projectileSpawnerCounter = 0;
        while (projectileSpawnerCounter < maxProjectileSpawnerCount)
        {
            PoolItem missile = _poolmanager.SpawnItemFromPool(PoolItemsTypes.ChainLightning_Skill,
                spawnPoint.position, Quaternion.identity, dynamicEnvironment);

            if (missile != null)
            {
                if (missile.TryGetComponent(out ChainLightningProjectile projectile))
                {
                    projectile.TargetHolder = targetsHolder;
                    projectile.JumpsCount = _playerCharacteristicsManager.CurrentPlayerData.playerSkillsData
                        .chainLightningSkillData.jumpsAmount;
                }
                missile.SetObjectAwakeState();
            }

            projectileSpawnerCounter++;
            yield return new WaitForSecondsRealtime(skillCooldownBetweenShots);
        }

        yield return new WaitForSecondsRealtime(skillCooldown);
    }
}