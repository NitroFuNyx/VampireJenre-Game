using System.Collections;
using UnityEngine;

public class EnemySkillsManager : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPoint;

    private PoolItemsManager _poolmanager;
    private PoolItem poolItem;
    private Transform dynamicEnvironment;
    private PlayerController player;

    public Transform DynamicEnvironment
    {
        get => dynamicEnvironment;
        set => dynamicEnvironment = value;
    }

    private void Start()
    {
        CashComponents();

        poolItem = GetComponent<PoolItem>();
    }

    private void CashComponents()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        StartCoroutine(SpawningProjectile());
    }

    private IEnumerator SpawningProjectile()
    {
        while (true)
        {
            StartCoroutine(SettingUpProjectile());

            yield return new WaitForSeconds(1f); //change cd
            //}
        }
    }

    private IEnumerator SettingUpProjectile()
    {
        if (dynamicEnvironment == null) yield break;
        var rotation = -35;
        int projectileSpawnerCounter = 0;
        while (projectileSpawnerCounter < 3)
        {
            projectileSpawnPoint.position = new Vector3(projectileSpawnPoint.position.x, player.transform.position.y,
                projectileSpawnPoint.position.z);
            PoolItem missile = poolItem.PoolItemsManager.SpawnItemFromPool(PoolItemsTypes.Boss_Dark_Missile,
                projectileSpawnPoint.position, Quaternion.identity, DynamicEnvironment);

            if (missile != null)
            {
                missile.transform.LookAt(player.PlayerTargetPosition);
                missile.transform.Rotate(new Vector3(0, rotation, 0));
                missile.SetObjectAwakeState();
            }

            projectileSpawnerCounter++;
            rotation += 35;
            yield return null;
        }
    }
}