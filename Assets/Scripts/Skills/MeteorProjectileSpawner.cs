
using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class MeteorProjectileSpawner : ProjectileSpawnerBase
{
    [Header("Gizmo")] [SerializeField] private bool drawSpawnZone;
    [Range(0.1f, 100)] [SerializeField] private float spawnZoneWidth;
    [Range(0.1f, 100)] [SerializeField] private float spawnZoneLenght;
    [Range(0.1f, 100)] [SerializeField] private float spawnZoneHeight;
    [SerializeField] private BoxCollider spawnZone;
    private PoolItemsManager _poolmanager;

    [Inject]
    private void InjectDependencies(PoolItemsManager poolmanager)
    {
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
    protected override IEnumerator SettingUpProjectile()
    {
        Vector3 spawnPoint = new Vector3(GetSpawnPosition(), spawnZone.transform.position.y + spawnZone.size.y,
            GetSpawnPosition());
        PoolItem meteor = _poolmanager.SpawnItemFromPool(PoolItemsTypes.Meteor_Projectile, spawnPoint,Quaternion.identity, dynamicEnvironment);
        if (meteor != null)
        {
            //meteor.CashComponents(dynamicEnvironment);
            meteor.SetObjectAwakeState();
        }
        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        if(drawSpawnZone)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(new Vector3(transform.position.x, spawnZoneHeight / 4, transform.position.z ), new Vector3(spawnZoneWidth, spawnZoneHeight/2, spawnZoneLenght));
            
        }
    }

    private float GetSpawnPosition()
    {
        return Random.Range((float) -spawnZoneWidth/2, (float) spawnZoneWidth/2);
    }
}
