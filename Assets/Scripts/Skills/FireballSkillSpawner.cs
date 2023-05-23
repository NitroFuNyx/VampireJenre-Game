
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Zenject;

public class FireballSkillSpawner : ProjectileSpawnerBase
{
    [SerializeField] private List<PoolItem> projectiles;
    private PoolItemsManager _poolmanager;

    
    [Range(-50, 50)] [SerializeField] private float rotationSpeed;
    [Range(0, 10)] [SerializeField] private float radius;
    [Range(0, 10)] [SerializeField] private float height;
    [Range(0, 10)] [SerializeField] private int projectileCount;
    
    [Inject]
    private void InjectDependencies(PoolItemsManager poolmanager)
    {
        _poolmanager = poolmanager;
    }

    private void Update()
    {
        spawnPoint.rotation *= Quaternion.Euler(0,rotationSpeed*Time.deltaTime,0);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawningProjectile());
    }
    protected override IEnumerator SettingUpProjectile()
    {
        if (projectileCount-1 < projectiles.Count) yield break;
        PoolItem lightning = _poolmanager.SpawnItemFromPool(PoolItemsTypes.Fireball_Skill, transform.position,Quaternion.identity, spawnPoint);
        if (lightning != null) 
        {
            projectiles.Add(lightning);
            ReformatCircle();
            lightning.SetObjectAwakeState();
        }
        yield return null;
    }

    private void ReformatCircle()
    {
        for(int i = 0; i < projectiles.Count; ++i)
        {
            float circlePosition = (float)i / (float)projectiles.Count;
            float x = Mathf.Sin( circlePosition * Mathf.PI * 2.0f ) * radius;
            float z = Mathf.Cos( circlePosition * Mathf.PI * 2.0f ) * radius;
            projectiles[i].transform.position = new Vector3(x, height, z);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector3(transform.position.x,height,transform.position.z),radius);
    }
}
