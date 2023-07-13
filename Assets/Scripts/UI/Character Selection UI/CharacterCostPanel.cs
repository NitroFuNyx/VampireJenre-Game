using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class CharacterCostPanel : MonoBehaviour
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private Image resourceImage;
    [SerializeField] private TextMeshProUGUI costText;
    [Header("Classes Data")]
    [Space]
    [SerializeField] private PlayerClassesDataSO playerClassesDataSO;
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite gemSprite;
    [SerializeField] private Sprite coinsSprite;
    [Header("Colors")]
    [Space]
    [SerializeField] private Color notEnoughResourcesColor;

    private ResourcesManager _resourcesManager;

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager)
    {
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    public void UpdateCharacterCostPanel(PlayerClassDataStruct classData)
    {
        if(classData.buyingCurrency == ResourcesTypes.Gems)
        {
            resourceImage.sprite = gemSprite;

        }
        else
        {
            resourceImage.sprite = coinsSprite;
        }

        costText.text = $"{classData.price}";
        SetCostTextColor(classData.buyingCurrency, classData.price);
    }

    private void SetCostTextColor(ResourcesTypes resource, float currentCharacterCost)
    {
        int resourceAmount = 0;

        if(resource == ResourcesTypes.Gems)
        {
            resourceAmount = _resourcesManager.GameData.gemsAmount.GetValue();
        }
        else
        {
            resourceAmount = _resourcesManager.GameData.coinsAmount.GetValue();
        }

        if (resourceAmount >= currentCharacterCost)
        {
            costText.color = Color.white;
        }
        else
        {
            costText.color = notEnoughResourcesColor;
        }
    }
}
