
using System.Collections;
using UnityEngine;
using Zenject;

public class PowerWaveProjectileSpawner : ProjectileSpawnerBase
{
    [Range(0, 1)] [SerializeField] protected float skillCooldownBetweenShots;

    [SerializeField] private int maxProjectileSpawnerCount = 0;

    private Transform thisTransform;
    private PoolItemsManager _poolmanager;

    private void OnEnable()
    {
        thisTransform = transform;

        StartCoroutine(SpawningProjectile());
    }

    [Inject]
    private void InjectDependencies(PoolItemsManager poolmanager)
    {
        _poolmanager = poolmanager;
    }
    protected override IEnumerator SettingUpProjectile()
    {
        int projectileSpawnerCounter = 0;
        while (projectileSpawnerCounter < maxProjectileSpawnerCount)
        {
            thisTransform.rotation = Quaternion.Euler(0, Random.Range(0, 361), 0);
            PoolItem missile = _poolmanager.SpawnItemFromPool(PoolItemsTypes.PowerWave_Skill,
                spawnPoint.position, thisTransform.rotation, dynamicEnvironment);
            
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
