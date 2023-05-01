using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;



[Serializable]
public class MissilesProjectileSpawner : MonoBehaviour
{
    [SerializeField] private SkillParameterBase skill ;
    [Range(0,2)]
    [SerializeField]private  float skillCooldown;
    [Range(0,1)]
    [SerializeField] private float skillCooldownBetweenShots;

    [SerializeField]
    private Transform spawnPoint;

    private Transform thisTransform;

    private void Start()
    {
        thisTransform = transform;
        StartCoroutine(SpawnProjectile());
    }

    private IEnumerator SpawnProjectile()
    {
        while(true)
        {
            int projectileSpawnerCount=0;
            while(projectileSpawnerCount<3)
            {
                thisTransform.rotation = Quaternion.Euler(0, Random.Range(0, 361), 0); //TODO: MAKE TARGET LOCK
                Instantiate(skill, spawnPoint.position, thisTransform.rotation);
                projectileSpawnerCount++;
                yield return new WaitForSecondsRealtime(skillCooldownBetweenShots);
            }
            
            yield return new WaitForSecondsRealtime(skillCooldown);
        }

    }
}
