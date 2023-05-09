using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainUI : MonoBehaviour
{
    private List<MainCanvasPanel> panelsList = new List<MainCanvasPanel>();
    private Dictionary<UIPanels, MainCanvasPanel> panelsDictionary = new Dictionary<UIPanels, MainCanvasPanel>();

    private MainLoaderUI _mainLoaderUI;
    private MainScreenUI _mainScreenUI;

    private GameProcessManager _gameProcessManager;

    private void Awake()
    {
        FillPanelsListAndDictionary();
    }

    private void Start()
    {
        SetStartSettings();
    }

    #region Zenject
    [Inject]
    private void Construct(MainLoaderUI mainLoaderUI, MainScreenUI mainScreenUI, GameProcessManager gameProcessManager)
    {
        _mainLoaderUI = mainLoaderUI;
        _mainScreenUI = mainScreenUI;
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    public void MenuButtonPressed_ExecuteReaction(UIPanels panel)
    {
        ActivateMainCanvasPanel(panel);
        panelsDictionary[UIPanels.MenuButtonsUI].ShowPanel();
    }

    public void PlayButtonPressed_ExecuteReaction()
    {
        ActivateMainCanvasPanel(UIPanels.GameLevelUI);
        _gameProcessManager.StartGame();
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
