using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class CharacterClassUpgradePanel : PanelActivationManager
{
    [Header("Images")]
    [Space]
    [SerializeField] private Image characterImage;
    [Header("Internal Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterLevel;
    [Space]
    [SerializeField] private TextMeshProUGUI speedValueText;
    [SerializeField] private TextMeshProUGUI damageValueText;
    [SerializeField] private TextMeshProUGUI healthValueText;
    [Header("Progress Bars")]
    [Space]
    [SerializeField] private CharacterClassProgressBar speedValueProgressBar;
    [SerializeField] private CharacterClassProgressBar damageValueProgressBar;
    [SerializeField] private CharacterClassProgressBar healthValueProgressBar;

    private PlayerCharacteristicsManager _playerCharacteristicsManager;
    private CharacterSelectionUI _characterSelectionUI;

    PlayerBasicCharacteristicsStruct visbleCharacterCharacteristicsData;

    #region Zenject
    [Inject]
    private void Construct(PlayerCharacteristicsManager playerCharacteristicsManager, CharacterSelectionUI characterSelectionUI)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
        _characterSelectionUI = characterSelectionUI;
    }
    #endregion Zenject

    public void UpdateCharacterClassData(PlayerClassDataStruct classData)
    {
        visbleCharacterCharacteristicsData = _playerCharacteristicsManager.GetCharacterCharacteristicsData(_characterSelectionUI.VisibleCharacter);

        characterName.text = $"{classData.characterName}";
        characterLevel.text = $"{visbleCharacterCharacteristicsData.characterLevel}";
        characterImage.sprite = classData.classSprite;

        SetProgressBarsValues(classData);
    }

    private void SetProgressBarsValues(PlayerClassDataStruct classData)
    {
        speedValueText.text = $"{visbleCharacterCharacteristicsData.currentClassProgressValue_Speed}";
        damageValueText.text = $"{visbleCharacterCharacteristicsData.currentClassProgressValue_Damage}";
        healthValueText.text = $"{visbleCharacterCharacteristicsData.currentClassProgressValue_Health}";

        Debug.Log($"Current {visbleCharacterCharacteristicsData.currentClassProgressValue_Speed} Max {classData.maxSpeedValue}");

        speedValueProgressBar.UpdateValue(visbleCharacterCharacteristicsData.currentClassProgressValue_Speed, classData.maxSpeedValue);
        damageValueProgressBar.UpdateValue(visbleCharacterCharacteristicsData.currentClassProgressValue_Damage, classData.maxDamageValue);
        healthValueProgressBar.UpdateValue(visbleCharacterCharacteristicsData.currentClassProgressValue_Health, classData.maxHealthValue);
    }
}
