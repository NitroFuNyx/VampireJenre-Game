using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreasureResourceInfoPanel : MonoBehaviour
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private Image resourceImage;
    [SerializeField] private TextMeshProUGUI resourceAmountText;
    [Header("Resource Data")]
    [Space]
    [SerializeField] private ResourcesTypes resourceType;
    [SerializeField] private int resourceAmount;

    public void UpdateResourceData(TreasureChestResourceDataStruct treasureData)
    {
        resourceImage.sprite = treasureData.resourceSprite;
        resourceAmountText.text = $"+ {treasureData.resourceAmount}";
    }
}
