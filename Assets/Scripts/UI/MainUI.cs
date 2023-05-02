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

    private void FillPanelsListAndDictionary()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out MainCanvasPanel mainCanvasPanel))
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
            }
            else
            {
                panelsList[i].HidePanel();
            }
        }
    }  

    private void MainLoaderAnimationFinished_ExecuteReaction()
    {
        ActivateMainCanvasPanel(UIPanels.GameLevelUI);
        _gameProcessManager.StartGame();// Change for Main Screen UI after it will be drawn
    }
}
