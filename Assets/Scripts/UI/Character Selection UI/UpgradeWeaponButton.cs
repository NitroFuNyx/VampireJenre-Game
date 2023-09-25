using UnityEngine;
using Zenject;

public class UpgradeWeaponButton : ButtonInteractionHandler
{
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
        WeaponUpgradeDataStruct weaponUpgradeData = _playerCharacteristicsManager.GetWeaponUpgradeData(_characterSelectionUI.VisibleCharacter);

        if (_resourcesManager.CheckIfEnoughResources(weaponUpgradeData.buyingCurrency, (int)weaponUpgradeData.price)) // check if max level
        {
            _resourcesManager.SpentResource(weaponUpgradeData.buyingCurrency, (int)weaponUpgradeData.price);
            ShowAnimation_ButtonPressed();
            _playerCharacteristicsManager.UpgradePlayerWeapon(_characterSelectionUI.VisibleCharacter);
        }
    }
}
