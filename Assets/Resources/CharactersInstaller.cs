using UnityEngine;
using Zenject;

public class CharactersInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private PlayerCollisionsManager playerCollisionsManager;

    public override void InstallBindings()
    {
        Container.Bind<PlayerCollisionsManager>().FromInstance(playerCollisionsManager).AsSingle().NonLazy();
        
    }
}
