
using System.Collections;
using UnityEngine;
using Zenject;

public class ChainLightningProjectileSpawner : ProjectileSpawnerBase
{
    [Range(0, 1)] [SerializeField] protected float skillCooldownBetweenShots;
    [SerializeField] private int maxProjectileSpawnerCount = 0;
    private PoolItemsManager _poolmanager;


    [Inject]
    private void InjectDependencies(PoolItemsManager poolmanager)
    {
        _poolmanager = poolmanager;
    }

    private void OnEnable()
    {
        StartCoroutine(SpawningProjectile());
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
                missile.SetObjectAwakeState();
            }

            projectileSpawnerCounter++;
            yield return new WaitForSecondsRealtime(skillCooldownBetweenShots);
        }

        yield return new WaitForSecondsRealtime(skillCooldown);
    }
}