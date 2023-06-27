using UnityEngine;
using Zenject;

public class ControllersInstaller : MonoInstaller
{
    [Header("Main References")]
    [Space]
    [SerializeField] private DataPersistanceManager dataPersistanceManager;
    [SerializeField] private PoolItemsManager poolItemsManager;
    [SerializeField] private GameProcessManager gameProcessManager;
    [SerializeField] private ResourcesManager resourcesManager;
    [SerializeField] private SpawnEnemiesManager spawnEnemiesManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private HapticManager hapticManager;
    [SerializeField] private LanguageManager languageManager;
    [SerializeField] private TimersManager timersManager;
    [SerializeField] private SystemTimeManager systemTimeManager;
    [SerializeField] private RewardsManager rewardsManager;
    [SerializeField] private TalentsManager talentsManager;
    [SerializeField] private PickableItemsManager pickableItemsManager;
    [SerializeField] private EnemiesCharacteristicsManager enemiesCharacteristicsManager;
    [SerializeField] private ChaptersProgressManager chaptersProgressManager;
    [SerializeField] private AdsManager adsManager;
    [SerializeField] private VFXManager vfxManager;
    [Header("Player References")]
    [Space]
    [SerializeField] private PlayerExperienceManager playerExperienceManager;
    [SerializeField] private PlayerCharacteristicsManager playerCharacteristicsManager;
    [SerializeField] private SkillsManager skillsManager;
    [SerializeField] private PlayerCharactersManager playerCharactersManager;

    public override void InstallBindings()
    {
        Container.Bind<DataPersistanceManager>().FromInstance(dataPersistanceManager).AsSingle().NonLazy();
        Container.Bind<PoolItemsManager>().FromInstance(poolItemsManager).AsSingle().NonLazy();
        Container.Bind<GameProcessManager>().FromInstance(gameProcessManager).AsSingle().NonLazy();
        Container.Bind<ResourcesManager>().FromInstance(resourcesManager).AsSingle().NonLazy();
        Container.Bind<SpawnEnemiesManager>().FromInstance(spawnEnemiesManager).AsSingle().NonLazy();
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle().NonLazy();
        Container.Bind<HapticManager>().FromInstance(hapticManager).AsSingle().NonLazy();
        Container.Bind<LanguageManager>().FromInstance(languageManager).AsSingle().NonLazy();
        Container.Bind<TimersManager>().FromInstance(timersManager).AsSingle().NonLazy();
        Container.Bind<SystemTimeManager>().FromInstance(systemTimeManager).AsSingle().NonLazy();
        Container.Bind<RewardsManager>().FromInstance(rewardsManager).AsSingle().NonLazy();
        Container.Bind<TalentsManager>().FromInstance(talentsManager).AsSingle().NonLazy();
        Container.Bind<PickableItemsManager>().FromInstance(pickableItemsManager).AsSingle().NonLazy();
        Container.Bind<EnemiesCharacteristicsManager>().FromInstance(enemiesCharacteristicsManager).AsSingle().NonLazy();
        Container.Bind<ChaptersProgressManager>().FromInstance(chaptersProgressManager).AsSingle().NonLazy();
        Container.Bind<AdsManager>().FromInstance(adsManager).AsSingle().NonLazy();
        Container.Bind<VFXManager>().FromInstance(vfxManager).AsSingle().NonLazy();

        Container.Bind<PlayerExperienceManager>().FromInstance(playerExperienceManager).AsSingle().NonLazy();
        Container.Bind<PlayerCharacteristicsManager>().FromInstance(playerCharacteristicsManager).AsSingle().NonLazy();
        Container.Bind<SkillsManager>().FromInstance(skillsManager).AsSingle().NonLazy();
        Container.Bind<PlayerCharactersManager>().FromInstance(playerCharactersManager).AsSingle().NonLazy();
    }
}
