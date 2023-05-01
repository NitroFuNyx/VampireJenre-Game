using System.Collections;
using UnityEngine;

public class SingleShotProjectileSpawner : MonoBehaviour
{
    [SerializeField] private SkillParameterBase skill ;
    [Range(0,2)]
    [SerializeField]private  float skillCooldown;
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
            thisTransform.rotation = Quaternion.Euler(0, Random.Range(0, 361),0); //TODO: MAKE TARGET LOCK
            Instantiate(skill, spawnPoint.position, thisTransform.rotation);
            yield return new WaitForSecondsRealtime(skillCooldown);
        }

    }
}
