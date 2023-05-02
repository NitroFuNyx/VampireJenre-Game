
using System;
using UnityEngine;

public class MeteorProjectileSpawner : ProjectileSpawnerBase
{
    [Header("Gizmo")] [SerializeField] private bool drawSpawnZone;
    [Range(0.1f, 100)] [SerializeField] private float spawnZoneWidth;
    [SerializeField] private BoxCollider spawnZone;

    private void Start()
    {
        spawnZone.center = new Vector3(transform.position.x, spawnZoneWidth / 4, transform.position.z);
        spawnZone.size = new Vector3(spawnZoneWidth, spawnZoneWidth/2, spawnZoneWidth);
    }

    private void OnDrawGizmosSelected()
    {
        if(drawSpawnZone)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(new Vector3(0, spawnZoneWidth / 4, 0 ), new Vector3(spawnZoneWidth, spawnZoneWidth/2, spawnZoneWidth));
            
        }
    }
}
