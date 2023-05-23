using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Zenject;

public class MenuButtonsUI : MainCanvasPanel
{
    [Header("Buttons")]
    [Space]
    [SerializeField] private List<MenuPanelButton> buttonsList;
    [Header("Internal References")]
    [Space]
    [SerializeField] private GameObject shieldImage;

    private ResourcesManager _resourcesManager;

    private void Start()
    {
        ChangeScreenBlockingState(false);
    }

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager)
    {
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    public override void PanelActivated_ExecuteReaction()
    {

    }

    public override void PanelDeactivated_ExecuteReaction()
    {

    }

    public void ResetButtonsSprites()
    {
        for(int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].SetStandartButtonSprite();
        }
    }

    public void ChangeScreenBlockingState(bool blocked)
    {
        shieldImage.SetActive(blocked);
    }
}
