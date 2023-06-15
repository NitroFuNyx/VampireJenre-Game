using UnityEngine;
using Zenject;

public class AdsControllerInstaller : MonoInstaller
{
    [Header("References")] [Space] [SerializeField]
    private AdsController adsController;

    public override void InstallBindings()
    {
        Container.Bind<AdsController>().FromInstance(adsController).AsSingle().NonLazy();
    }
}