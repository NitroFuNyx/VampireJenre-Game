using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private MainLoaderUI mainLoaderUI;
    [SerializeField] private MainScreenUI mainScreenUI;

    public override void InstallBindings()
    {
        Container.Bind<MainLoaderUI>().FromInstance(mainLoaderUI).AsSingle().NonLazy();
        Container.Bind<MainScreenUI>().FromInstance(mainScreenUI).AsSingle().NonLazy();
    }
}
