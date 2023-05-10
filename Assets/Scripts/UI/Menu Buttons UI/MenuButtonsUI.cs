using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Zenject;

public class MenuButtonsUI : MainCanvasPanel
{
    [Header("Resources")]
    [Space]
    [SerializeField] private TextMeshProUGUI coinsAmountText;
    [SerializeField] private TextMeshProUGUI gemsAmountText;
    [Header("Buttons")]
    [Space]
    [SerializeField] private List<MenuPanelButton> buttonsList;
    [Header("Internal References")]
    [Space]
    [SerializeField] private GameObject shieldImage;

    private ResourcesManager _resourcesManager;

    private void Start()
    {
        SubscribeOnEvents();
        ChangeScreenBlockingState(false);
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
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


    private void SubscribeOnEvents()
    {
        _resourcesManager.OnCoinsAmountChanged += ResourcesManager_CoinsAmountChanged_ExecuteReaction;
        _resourcesManager.OnGemsAmountChanged += ResourcesManager_GemsAmountChanged_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _resourcesManager.OnCoinsAmountChanged -= ResourcesManager_CoinsAmountChanged_ExecuteReaction;
        _resourcesManager.OnGemsAmountChanged -= ResourcesManager_GemsAmountChanged_ExecuteReaction;
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

    private void ResourcesManager_CoinsAmountChanged_ExecuteReaction(int amount)
    {
        coinsAmountText.text = $"{amount}";
    }

    private void ResourcesManager_GemsAmountChanged_ExecuteReaction(int amount)
    {
        gemsAmountText.text = $"{amount}";
    }
}
