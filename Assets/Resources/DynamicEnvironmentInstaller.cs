using UnityEngine;
using Zenject;

public class DynamicEnvironmentInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private Transform dynamicEnvironment;

    public override void InstallBindings()
    {
        Container.Bind<Transform>().FromInstance(dynamicEnvironment).AsSingle().NonLazy();
    }
}
