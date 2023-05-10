using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private MainUI mainUI;
    [SerializeField] private MainLoaderUI mainLoaderUI;
    [SerializeField] private MainScreenUI mainScreenUI;
    [SerializeField] private MenuButtonsUI menuButtonsUI;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private RewardWheelSpinner rewardWheelSpinner;

    public override void InstallBindings()
    {
        Container.Bind<MainUI>().FromInstance(mainUI).AsSingle().NonLazy();
        Container.Bind<MainLoaderUI>().FromInstance(mainLoaderUI).AsSingle().NonLazy();
        Container.Bind<MainScreenUI>().FromInstance(mainScreenUI).AsSingle().NonLazy();
        Container.Bind<MenuButtonsUI>().FromInstance(menuButtonsUI).AsSingle().NonLazy();
        Container.Bind<SettingsUI>().FromInstance(settingsUI).AsSingle().NonLazy();
        Container.Bind<RewardWheelSpinner>().FromInstance(rewardWheelSpinner).AsSingle().NonLazy();
    }
}
