using UnityEngine;
using Zenject;

public class SkillSupporterInstaller : MonoInstaller
{
    [Header("References")] [Space] [SerializeField]
    private TargetsHolder targetsHolder;

    public override void InstallBindings()
    {
        Container.Bind<TargetsHolder>().FromInstance(targetsHolder).AsSingle().NonLazy();
    }
}