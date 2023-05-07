using UnityEngine;
using Zenject;

public class ControllersInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private DataPersistanceManager dataPersistanceManager;
    [SerializeField] private PoolItemsManager poolItemsManager;
    [SerializeField] private GameProcessManager gameProcessManager;
    [SerializeField] private ResourcesManager resourcesManager;
    [SerializeField] private PlayerExperienceManager playerExperienceManager;
    [SerializeField] private SpawnEnemiesManager spawnEnemiesManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private HapticManager hapticManager;

    public override void InstallBindings()
    {
        Container.Bind<DataPersistanceManager>().FromInstance(dataPersistanceManager).AsSingle().NonLazy();
        Container.Bind<PoolItemsManager>().FromInstance(poolItemsManager).AsSingle().NonLazy();
        Container.Bind<GameProcessManager>().FromInstance(gameProcessManager).AsSingle().NonLazy();
        Container.Bind<ResourcesManager>().FromInstance(resourcesManager).AsSingle().NonLazy();
        Container.Bind<PlayerExperienceManager>().FromInstance(playerExperienceManager).AsSingle().NonLazy();
        Container.Bind<SpawnEnemiesManager>().FromInstance(spawnEnemiesManager).AsSingle().NonLazy();
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle().NonLazy();
        Container.Bind<HapticManager>().FromInstance(hapticManager).AsSingle().NonLazy();
    }
}
