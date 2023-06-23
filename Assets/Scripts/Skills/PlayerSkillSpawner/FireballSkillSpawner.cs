using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Zenject;

public class FireballSkillSpawner : ProjectileSpawnerBase
{
    [SerializeField] private List<PoolItem> projectiles;

    
    [Range(-50, 75)] [SerializeField] private float rotationSpeed;
    [Range(0, 10)] [SerializeField] private float radius;
    [Range(0, 10)] [SerializeField] private float height;
    //[Range(0, 10)] [SerializeField] private int projectileCount;
    
    private PoolItemsManager _poolmanager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    [Inject]
    private void InjectDependencies(PoolItemsManager poolmanager,PlayerCharacteristicsManager playerCharacteristicsManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
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

    private void OnDisable()
    {
        ReturnToPoolItems();
    }

    protected override IEnumerator SpawningProjectile()
    {
        while (true)
        {
            StartCoroutine(SettingUpProjectile());

            yield return new WaitForSeconds(0.1f);        }
    }
    protected override IEnumerator SettingUpProjectile()
    {
        if (_playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.fireballsSkillData.projectilesAmount-1 < projectiles.Count) yield break;
        Debug.Log(_playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.fireballsSkillData.projectilesAmount);
        PoolItem fireball = _poolmanager.SpawnItemFromPool(PoolItemsTypes.Fireball_Skill, transform.position,Quaternion.identity, spawnPoint);
        if (fireball != null) 
        {
            
            projectiles.Add(fireball);
            ReformatCircle();
            fireball.SetObjectAwakeState();
        }
        yield return null;
    }

    private void ReturnToPoolItems()
    {
        foreach (var fireball in projectiles)
        {
            fireball.PoolItemsManager.ReturnItemToPool(fireball);
        }
    }
    private void ReformatCircle()
    {
        for(int i = 0; i < projectiles.Count; ++i)
        {
            float circlePosition = (float)i / (float)projectiles.Count;
            float x = Mathf.Sin( circlePosition * Mathf.PI * 2.0f ) * radius;
            float z = Mathf.Cos( circlePosition * Mathf.PI * 2.0f ) * radius;
            projectiles[i].transform.position =transform.position + new Vector3(x, height, z);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector3(transform.position.x,height,transform.position.z),radius);
    }
}
