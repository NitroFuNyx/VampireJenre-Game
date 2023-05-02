using UnityEngine;
using Zenject;

public class ControllersInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private DataPersistanceManager dataPersistanceManager;
    [SerializeField] private PoolItemsManager poolItemsManager;
    [SerializeField] private GameProcessManager gameProcessManager;
    [SerializeField] private SpawnEnemiesManager spawnEnemiesManager;

    public override void InstallBindings()
    {
        Container.Bind<DataPersistanceManager>().FromInstance(dataPersistanceManager).AsSingle().NonLazy();
        Container.Bind<PoolItemsManager>().FromInstance(poolItemsManager).AsSingle().NonLazy();
        Container.Bind<GameProcessManager>().FromInstance(gameProcessManager).AsSingle().NonLazy();
        Container.Bind<SpawnEnemiesManager>().FromInstance(spawnEnemiesManager).AsSingle().NonLazy();
    }
}
