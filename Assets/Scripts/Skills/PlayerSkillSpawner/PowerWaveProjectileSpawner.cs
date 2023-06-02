
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PowerWaveProjectileSpawner : ProjectileSpawnerBase
{
    [Range(0, 1)] [SerializeField] protected float skillCooldownBetweenShots;
    [Range(0, 5)] [SerializeField] protected float CD;

    [SerializeField] private int maxProjectileSpawnerCount = 0;
    [SerializeField] private TargetsHolder targetsHolder;

    private Transform thisTransform;
    private PoolItemsManager _poolmanager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    
    [SerializeField] private List<PoolItem> projectiles;

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

            yield return new WaitForSeconds(CD);
           // yield return new WaitForSeconds(_playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.playerForceWaveData.cooldown);
        }
    }
    protected override IEnumerator SettingUpProjectile()
    {
        //int projectileSpawnerCounter = 0;
        while (projectiles.Count < _playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.playerForceWaveData.projectilesAmount)
        {
            thisTransform.rotation = Quaternion.Euler(0, Random.Range(0, 361), 0);
            PoolItem missile = _poolmanager.SpawnItemFromPool(PoolItemsTypes.PowerWave_Skill,
                spawnPoint.position, thisTransform.rotation, dynamicEnvironment);
            
            if (missile != null)
            {
                projectiles.Add(missile);

                if (missile.TryGetComponent(out PowerWaveSkillProjectile projectile))
                {
                    projectile.OnItemReturnToPool += RemoveProjectileFromList;

                    projectile.TargetHolder = targetsHolder;
                }
                missile.SetObjectAwakeState();
                
            }
            //projectileSpawnerCounter++;
            yield return new WaitForSeconds(skillCooldownBetweenShots);
        }

    }
    public void RemoveProjectileFromList(PoolItem item)
    {
        projectiles.Remove(item);
    }
}
