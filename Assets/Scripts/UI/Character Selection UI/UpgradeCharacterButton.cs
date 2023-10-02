using UnityEngine;
using Zenject;

public class UpgradeCharacterButton : ButtonInteractionHandler
{
    [Header("Cost Data")]
    [Space]
    [SerializeField] private ResourcesTypes buyingCurrency = ResourcesTypes.Coins;
    [SerializeField] private int cost = 100;

    private ResourcesManager _resourcesManager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;
    private CharacterSelectionUI _characterSelectionUI;

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager, PlayerCharacteristicsManager playerCharacteristicsManager,
                           CharacterSelectionUI characterSelectionUI)
    {
        _resourcesManager = resourcesManager;
        _playerCharacteristicsManager = playerCharacteristicsManager;
        _characterSelectionUI = characterSelectionUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if (_resourcesManager.CheckIfEnoughResources(buyingCurrency, cost))
        {
            _resourcesManager.SpentResource(buyingCurrency, cost);
            ShowAnimation_ButtonPressed();
            _playerCharacteristicsManager.UpgradeCharacterClassData(_characterSelectionUI.VisibleCharacter);
        }
    }
}
