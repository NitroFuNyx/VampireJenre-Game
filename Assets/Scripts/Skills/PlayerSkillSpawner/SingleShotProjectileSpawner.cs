
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SingleShotProjectileSpawner : ProjectileSpawnerBase
{
    [Range(0, 1)] [SerializeField] protected float skillCooldownBetweenShots;

    [SerializeField] private int maxProjectileSpawnerCount = 0;
    [SerializeField] private TargetsHolder targetsHolder;

    private Transform thisTransform;
    private PoolItemsManager _poolmanager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    [SerializeField] private List<PoolItem> projectiles;

    private void OnEnable()
    {
        thisTransform = transform;

        StartCoroutine(SpawningProjectile());
    }

    [Inject]
    private void InjectDependencies(PoolItemsManager poolmanager,PlayerCharacteristicsManager playerCharacteristicsManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
        _poolmanager = poolmanager;
    }

    protected override IEnumerator SpawningProjectile()
    {
        while (true)
        {
            StartCoroutine(SettingUpProjectile());

            yield return new WaitForSeconds(_playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.singleShotSkillData.cooldown);
        }
    }
    protected override IEnumerator SettingUpProjectile()
    {
        int projectileSpawnerCounter = 0;
        while (projectileSpawnerCounter < _playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.singleShotSkillData.projectilesAmount)
        {
            thisTransform.rotation = Quaternion.Euler(0, Random.Range(0, 361), 0);
            PoolItem missile = _poolmanager.SpawnItemFromPool(PoolItemsTypes.Singleshot_Projectile,
                spawnPoint.position, thisTransform.rotation, dynamicEnvironment);
            
            if (missile != null)
            {
                if (missile.TryGetComponent(out SingleShotSkillProjectile projectile))
                {
                    projectile.TargetHolder = targetsHolder;
                }
                missile.SetObjectAwakeState();
            }
            projectileSpawnerCounter++;
            yield return new WaitForSeconds(skillCooldownBetweenShots);
        }

       
    }
}
