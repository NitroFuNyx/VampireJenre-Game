using UnityEngine;
using Zenject;

public class ControllersInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private DataPersistanceManager dataPersistanceManager;
    [SerializeField] private PoolItemsManager poolItemsManager;

    public override void InstallBindings()
    {
        Container.Bind<DataPersistanceManager>().FromInstance(dataPersistanceManager).AsSingle().NonLazy();
        Container.Bind<PoolItemsManager>().FromInstance(poolItemsManager).AsSingle().NonLazy();
    }
}
