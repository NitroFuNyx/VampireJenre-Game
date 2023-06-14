using UnityEngine;
using Zenject;

public class MenuPanelButton : ButtonInteractionHandler
{
    [Header("Panel Data")]
    [Space]
    [SerializeField] private UIPanels panelType;
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite standartButtonSprite;
    [SerializeField] private Sprite highlightedButtonSprite;

    private MainUI _mainUI;
    private MenuButtonsUI _menuButtonsUI;

    public UIPanels PanelType { get => panelType; }

    #region Zenject
    [Inject]
    private void Construct(MainUI mainUI, MenuButtonsUI menuButtonsUI)
    {
        _mainUI = mainUI;
        _menuButtonsUI = menuButtonsUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if(panelType == UIPanels.CharacterSelectionUI || panelType == UIPanels.ShopUI)
        {
            _mainUI.ShowRoadmapUI();
            _menuButtonsUI.ResetButtonsSprites();
        }
        else
        {
            _mainUI.MenuButtonPressed_ExecuteReaction(panelType);
            _menuButtonsUI.ResetButtonsSprites();
            buttonImage.sprite = highlightedButtonSprite;
        }
    }

    public void SetStandartButtonSprite()
    {
        buttonImage.sprite = standartButtonSprite;
    }
}
