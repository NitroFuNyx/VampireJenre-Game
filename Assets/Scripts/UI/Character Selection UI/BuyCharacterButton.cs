using System;
using UnityEngine;
using Zenject;

public class BuyCharacterButton : ButtonInteractionHandler
{
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite disabledSprite;
    [Header("Classes Data")]
    [Space]
    [SerializeField] private PlayerClassesDataSO playerClassesDataSO;

    private ResourcesManager _resourcesManager;
    private PlayerClassDataStruct currentVisibleCharacterData;

    public event Action<PlayerClasses> OnNewCharacterBought;

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager)
    {
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    public void UpdateButtonSprite(PlayerClassDataStruct classData)
    {
        currentVisibleCharacterData = classData;

        if (_resourcesManager.CheckIfEnoughResources(currentVisibleCharacterData.buyingCurrency, (int)currentVisibleCharacterData.price))
        {
            buttonImage.sprite = activeSprite;
        }
        else
        {
            buttonImage.sprite = disabledSprite;
        }
    }

    public override void ButtonActivated()
    {
        if(_resourcesManager.CheckIfEnoughResources(currentVisibleCharacterData.buyingCurrency, (int)currentVisibleCharacterData.price))
        {
            _resourcesManager.BuyCharacter(currentVisibleCharacterData.buyingCurrency, (int)currentVisibleCharacterData.price);
            ShowAnimation_ButtonPressed();
            OnNewCharacterBought?.Invoke(currentVisibleCharacterData.playerClass);
        }
    }
}
