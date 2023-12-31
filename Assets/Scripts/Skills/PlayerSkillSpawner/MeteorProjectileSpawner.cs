
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class MeteorProjectileSpawner : ProjectileSpawnerBase
{
    [Range(0, 1)] [SerializeField] protected float skillCooldownBetweenShots;
    [Header("Gizmo")] [SerializeField] private bool drawSpawnZone;
    [Range(0.1f, 100)] [SerializeField] private float spawnZoneWidth;
    [Range(0.1f, 100)] [SerializeField] private float spawnZoneLenght;
    [Range(0.1f, 100)] [SerializeField] private float spawnZoneHeight;
    [SerializeField] private BoxCollider spawnZone;
    private PoolItemsManager _poolmanager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    [SerializeField] private List<PoolItem> projectiles;

    
    [Inject]
    private void InjectDependencies(PoolItemsManager poolmanager,PlayerCharacteristicsManager playerCharacteristicsManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
        _poolmanager = poolmanager;
    }
    private void Start()
    {
        spawnZone.center = new Vector3(transform.position.x, spawnZoneHeight / 4 , transform.position.z);
        spawnZone.size = new Vector3(spawnZoneWidth, spawnZoneHeight/2, spawnZoneLenght);
       
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

            yield return new WaitForSeconds(_playerCharacteristicsManager.CurrentPlayerData.playerSkillsData
                .meteorSkillData.cooldown);        }
    }
    protected override IEnumerator SettingUpProjectile()
    {
        //int projectileSpawnerCounter = 0;
        while (projectiles.Count < _playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.meteorSkillData.projectilesAmount)
        {
            Vector3 spawnPoint = new Vector3(GetSpawnPositionX(), spawnZone.transform.position.y + spawnZone.size.y,
                GetSpawnPositionZ());
            PoolItem meteor = _poolmanager.SpawnItemFromPool(PoolItemsTypes.Meteor_Projectile, spawnPoint,
                Quaternion.identity, dynamicEnvironment);
            if (meteor != null)
            {
                projectiles.Add(meteor);

                //meteor.CashComponents(dynamicEnvironment);
                if (meteor.TryGetComponent(out MeteorSkillProjectile projectile))
                {
                    projectile.OnItemReturnToPool += RemoveProjectileFromList;
                    projectile.PuddleLifeTime = _playerCharacteristicsManager.CurrentPlayerData.playerSkillsData
                        .meteorSkillData.postEffectDuration;
                }
                meteor.SetObjectAwakeState();
            }

            yield return new WaitForSeconds(skillCooldownBetweenShots);
        }
       
    }

    public void RemoveProjectileFromList(PoolItem item)
    {
        projectiles.Remove(item);
    }
    private void OnDrawGizmosSelected()
    {
        if(drawSpawnZone)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(new Vector3(transform.position.x, spawnZoneHeight / 4, transform.position.z ), new Vector3(spawnZoneWidth, spawnZoneHeight/2, spawnZoneLenght));
            
        }
    }

    private float GetSpawnPositionX()
    {
        return Random.Range((float) (-spawnZoneWidth / 2)+transform.position.x, (float) spawnZoneWidth / 2+transform.position.x);
    }
    private float GetSpawnPositionZ()
    {
        return Random.Range((float) (-spawnZoneWidth / 2)+transform.position.z, (float) spawnZoneWidth / 2+transform.position.z);
    }
}
