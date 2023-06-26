using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [Header("Main References")]
    [Space]
    [SerializeField] private MainUI mainUI;
    [SerializeField] private MainLoaderUI mainLoaderUI;
    [SerializeField] private MainScreenUI mainScreenUI;
    [SerializeField] private MenuButtonsUI menuButtonsUI;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private RewardWheelSpinner rewardWheelSpinner;
    [SerializeField] private GameLevelUI gameLevelUI;
    [SerializeField] private SkillsInfoUI skillsInfoUI;
    [SerializeField] private AdsBlockerPurchasedPanel adsBlockerPurchasedPanel;
    [SerializeField] private TreasureChestInfoPanel treasureChestInfoPanel;
    [SerializeField] private CharacterSelectionUI characterSelectionUI;
    [Header("Talent UI")]
    [Space]
    [SerializeField] private TalentWheel talentWheel;
    [SerializeField] private TalentBoughtInfoPanel talentBoughtInfoPanel;

    public override void InstallBindings()
    {
        Container.Bind<MainUI>().FromInstance(mainUI).AsSingle().NonLazy();
        Container.Bind<MainLoaderUI>().FromInstance(mainLoaderUI).AsSingle().NonLazy();
        Container.Bind<MainScreenUI>().FromInstance(mainScreenUI).AsSingle().NonLazy();
        Container.Bind<MenuButtonsUI>().FromInstance(menuButtonsUI).AsSingle().NonLazy();
        Container.Bind<SettingsUI>().FromInstance(settingsUI).AsSingle().NonLazy();
        Container.Bind<RewardWheelSpinner>().FromInstance(rewardWheelSpinner).AsSingle().NonLazy();
        Container.Bind<GameLevelUI>().FromInstance(gameLevelUI).AsSingle().NonLazy();
        Container.Bind<SkillsInfoUI>().FromInstance(skillsInfoUI).AsSingle().NonLazy();
        Container.Bind<AdsBlockerPurchasedPanel>().FromInstance(adsBlockerPurchasedPanel).AsSingle().NonLazy();
        Container.Bind<TreasureChestInfoPanel>().FromInstance(treasureChestInfoPanel).AsSingle().NonLazy();
        Container.Bind<CharacterSelectionUI>().FromInstance(characterSelectionUI).AsSingle().NonLazy();

        Container.Bind<TalentWheel>().FromInstance(talentWheel).AsSingle().NonLazy();
        Container.Bind<TalentBoughtInfoPanel>().FromInstance(talentBoughtInfoPanel).AsSingle().NonLazy();
    }
}
