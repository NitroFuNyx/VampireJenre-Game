using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;


[Serializable]
public class MissilesProjectileSpawner : ProjectileSpawnerBase
{
    [Range(0, 1)] [SerializeField] protected float skillCooldownBetweenShots;

    [SerializeField] private int maxProjectileSpawnerCount = 0;

    private Transform thisTransform;
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
        thisTransform = transform;
        StartCoroutine(SpawningProjectile());
    }

    protected override IEnumerator SpawningProjectile()
    {
        while (true)
        {
            StartCoroutine(SettingUpProjectile());

            yield return new WaitForSecondsRealtime(_playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.allDirectionsShotsSkillData.cooldown);
        }
    }
    
    protected override IEnumerator SettingUpProjectile()
    {
        int projectileSpawnerCounter = 0;
        while (projectileSpawnerCounter < _playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.allDirectionsShotsSkillData.projectilesAmount)
        {
            thisTransform.rotation = Quaternion.Euler(0, Random.Range(0, 361), 0);
            PoolItem missile = _poolmanager.SpawnItemFromPool(PoolItemsTypes.Missile_Projectile,
                spawnPoint.position, thisTransform.rotation, dynamicEnvironment);
            
            if (missile != null)
            {
                missile.SetObjectAwakeState();
            }
            projectileSpawnerCounter++;
            yield return new WaitForSecondsRealtime(skillCooldownBetweenShots);
        }

    }
    
}