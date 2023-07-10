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

    public event Action OnBuyCharacterButtonPressed;

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

        if (CanBeBought())
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
        if(CanBeBought())
        {
            OnBuyCharacterButtonPressed?.Invoke();
        }
    }

    private bool CanBeBought()
    {
        bool canBeBought = false;

        int resourceAmount = 0;

        if (currentVisibleCharacterData.buyingCurrency == ResourcesTypes.Gems)
        {
            resourceAmount = _resourcesManager.GameData.gemsAmount.GetValue();
        }
        else
        {
            resourceAmount = _resourcesManager.GameData.coinsAmount.GetValue();
        }

        if (resourceAmount >= currentVisibleCharacterData.price)
        {
            canBeBought = true;
        }

        return canBeBought;
    }
}
