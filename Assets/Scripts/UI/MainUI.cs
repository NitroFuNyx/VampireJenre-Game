using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainUI : MonoBehaviour
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private UIDisolveHandler disolveImageHandler;

    private List<MainCanvasPanel> panelsList = new List<MainCanvasPanel>();
    private Dictionary<UIPanels, MainCanvasPanel> panelsDictionary = new Dictionary<UIPanels, MainCanvasPanel>();

    private MainLoaderUI _mainLoaderUI;
    private MainScreenUI _mainScreenUI;
    private MenuButtonsUI _menuButtonsUI;
    private GameLevelUI _gameLevelUI;

    private GameProcessManager _gameProcessManager;

    private void Awake()
    {
        FillPanelsListAndDictionary();
    }

    private void Start()
    {
        SetStartSettings();
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(MainLoaderUI mainLoaderUI, MainScreenUI mainScreenUI, GameProcessManager gameProcessManager,
                           MenuButtonsUI menuButtonsUI, GameLevelUI gameLevelUI)
    {
        _mainLoaderUI = mainLoaderUI;
        _mainScreenUI = mainScreenUI;
        _gameProcessManager = gameProcessManager;
        _menuButtonsUI = menuButtonsUI;
        _gameLevelUI = gameLevelUI;
    }
    #endregion Zenject

    public void MenuButtonPressed_ExecuteReaction(UIPanels panel)
    {
        ActivateMainCanvasPanel(panel);
        panelsDictionary[UIPanels.MenuButtonsUI].ShowPanel();
    }

    public void ShowRoadmapUI()
    {
        ActivateMainCanvasPanel(UIPanels.RoadmapUI);
        panelsDictionary[UIPanels.MenuButtonsUI].ShowPanel();
    }

    public void PlayButtonPressed_ExecuteReaction()
    {
        //disolveImageHandler.DisolveImage(DisolveProccessFinished_ExecuteReaction);
        ActivateMainCanvasPanel(UIPanels.GameLevelUI);
        _gameLevelUI.SetBattleModeUI(GameModes.Standart);
        _gameProcessManager.StartGame(GameModes.Standart);
    }

    [ContextMenu("Deathmatch")]
    public void DeathmatchButtonPressed_ExecuteReaction()
    {
        ActivateMainCanvasPanel(UIPanels.GameLevelUI);
        _gameLevelUI.SetBattleModeUI(GameModes.Deathmatch);
        _gameProcessManager.StartGame(GameModes.Deathmatch);
    }

    public void ShowRewardsUI()
    {
        ActivateMainCanvasPanel(UIPanels.RewardsUI);
        panelsDictionary[UIPanels.MenuButtonsUI].ShowPanel();
        _menuButtonsUI.ChangeScreenBlockingState(false);
    }

    public void ShowSkillsInfoUI()
    {
        panelsDictionary[UIPanels.SkillsInfoUI].ShowPanel();
    }

    public void HideSkillsInfoUI()
    {
        panelsDictionary[UIPanels.SkillsInfoUI].HidePanel();
    }

    public void ShowMenuButtonsUI()
    {
        panelsDictionary[UIPanels.MenuButtonsUI].ShowPanel();
    }

    public void HideMenuButtonsUI()
    {
        panelsDictionary[UIPanels.MenuButtonsUI].HidePanel();
    }

    public void ShowMainScreen()
    {
        ActivateMainCanvasPanel(UIPanels.MainScreenPanel);
        ShowMenuButtonsUI();
    }

    public void ShowNoAdsUI()
    {
        panelsDictionary[UIPanels.NoAdsUI].ShowPanel();
    }

    public void HideNoAdsUI()
    {
        panelsDictionary[UIPanels.NoAdsUI].HidePanel();
    }

    private void SubscribeOnEvents()
    {
        
    }

    private void UnsubscribeFromEvents()
    {
        
    }

    private void DisolveProccessFinished_ExecuteReaction(GameModes gameMode)
    {
        ActivateMainCanvasPanel(UIPanels.GameLevelUI);
        _gameProcessManager.StartGame(gameMode);
    }

    private void FillPanelsListAndDictionary()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out MainCanvasPanel mainCanvasPanel) && transform.GetChild(i).gameObject.activeInHierarchy)
            {
                panelsList.Add(mainCanvasPanel);
                if(!panelsDictionary.ContainsValue(mainCanvasPanel))
                {
                    panelsDictionary.Add(mainCanvasPanel.PanelType, mainCanvasPanel);
                }
            }
        }
    }

    private void SetStartSettings()
    {
        ActivateMainCanvasPanel(UIPanels.MainLoaderPanel);
        _mainLoaderUI.ShowLoading(MainLoaderAnimationFinished_ExecuteReaction);
    }

    private void ActivateMainCanvasPanel(UIPanels panel)
    {
        for (int i = 0; i < panelsList.Count; i++)
        {
            if (panelsList[i].PanelType == panel)
            {
                panelsList[i].ShowPanel();
                panelsList[i].PanelActivated_ExecuteReaction();
            }
            else
            {
                panelsList[i].HidePanel();
                panelsList[i].PanelDeactivated_ExecuteReaction();
            }
        }
    }

    private void MainLoaderAnimationFinished_ExecuteReaction()
    {
        MenuButtonPressed_ExecuteReaction(UIPanels.MainScreenPanel);
    }
}
