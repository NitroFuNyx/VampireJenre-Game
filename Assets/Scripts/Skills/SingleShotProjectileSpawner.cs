
public class SingleShotProjectileSpawner : ProjectileSpawnerBase
{
    

    private void OnEnable()
    {
        StartCoroutine(SpawningProjectile());
    }

}
