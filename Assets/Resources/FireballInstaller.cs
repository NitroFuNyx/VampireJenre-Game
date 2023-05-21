using UnityEngine;
using Zenject;

public class FireballInstaller : MonoInstaller
{
    [Header("References")] [Space] [SerializeField]
    private FireballSkillSpawner fireballSkillSpawner;

    public override void InstallBindings()
    {
        Container.Bind<FireballSkillSpawner>().FromInstance(fireballSkillSpawner).AsSingle().NonLazy();
    }
}