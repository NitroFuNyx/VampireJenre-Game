using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class LightningBoltSkillSpawner : ProjectileSpawnerBase
{
    [Range(0, 1)] [SerializeField] protected float skillCooldownBetweenShots;

    [Header("Gizmo")] [SerializeField] private bool drawSpawnZone;
    [Range(0.1f, 100)] [SerializeField] private float spawnZoneWidth;
    [Range(0.1f, 100)] [SerializeField] private float spawnZoneLenght;
    [Range(0.1f, 100)] [SerializeField] private float spawnZoneHeight;
    [SerializeField] private BoxCollider spawnZone;
    private PoolItemsManager _poolmanager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    [Inject]
    private void InjectDependencies(PoolItemsManager poolmanager,
        PlayerCharacteristicsManager playerCharacteristicsManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
        _poolmanager = poolmanager;
    }

    private void Start()
    {
        spawnZone.center = new Vector3(transform.position.x, spawnZoneHeight / 4, transform.position.z);
        spawnZone.size = new Vector3(spawnZoneWidth, spawnZoneHeight / 2, spawnZoneLenght);
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
                .lightningBoltSkillData.cooldown);
            
        }
    }
    protected override IEnumerator SettingUpProjectile()
    {
        int projectileSpawnerCounter = 0;
        while (projectileSpawnerCounter < _playerCharacteristicsManager.CurrentPlayerData.playerSkillsData
            .lightningBoltSkillData.projectilesAmount)
        {
            Vector3 spawnPoint = new Vector3(GetSpawnPosition(), 0,
                GetSpawnPosition());
            PoolItem lightning = _poolmanager.SpawnItemFromPool(PoolItemsTypes.Lightning_Bolt_Skill, spawnPoint,
                Quaternion.identity, dynamicEnvironment);
            if (lightning != null)
            {
                lightning.SetObjectAwakeState();
            }

            projectileSpawnerCounter++;
            yield return new WaitForSeconds(skillCooldownBetweenShots);
        }

        
    }

    private void OnDrawGizmosSelected()
    {
        if (drawSpawnZone)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(new Vector3(transform.position.x, spawnZoneHeight / 4, transform.position.z),
                new Vector3(spawnZoneWidth, spawnZoneHeight / 2, spawnZoneLenght));
        }
    }

    private float GetSpawnPosition()
    {
        return Random.Range((float) -spawnZoneWidth / 2, (float) spawnZoneWidth / 2);
    }
}