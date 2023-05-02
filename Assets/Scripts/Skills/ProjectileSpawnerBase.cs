using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class ProjectileSpawnerBase : MonoBehaviour
{
    [SerializeField] protected SkillParameterBase skill;
    [Range(0, 25)] [SerializeField] protected float skillCooldown;

    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected Transform dynamicEnvironment;
    protected virtual IEnumerator SpawningProjectile()
    {
        while (true)
        {
            StartCoroutine(SettingUpProjectile());

            yield return new WaitForSecondsRealtime(skillCooldown);
        }

    }

    protected virtual IEnumerator SettingUpProjectile()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 361),0); //TODO: MAKE TARGET LOCK
        Instantiate(skill, spawnPoint.position, transform.rotation,dynamicEnvironment);
        yield return null;
    }

    
}
