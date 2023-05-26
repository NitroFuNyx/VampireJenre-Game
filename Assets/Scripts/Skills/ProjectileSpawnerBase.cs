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
       
        yield return null;
    }

    
}
