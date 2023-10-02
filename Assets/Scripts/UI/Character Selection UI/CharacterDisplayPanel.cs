using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterDisplayPanel : MonoBehaviour
{
    [Header("Character Data")]
    [Space]
    [SerializeField] private PlayerClasses characterClass;
    [Header("Images")]
    [Space]
    [SerializeField] private Image characterImage;
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI classText;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [Header("Buttons")]
    [Space]
    [SerializeField] private ChangeVisibleCharacterButton changeVisibleCharacterButton;

    public void SetCharacterData(PlayerClassDataStruct characterInfo, int level)
    {
        characterClass = characterInfo.playerClass;
        characterImage.sprite = characterInfo.classSprite;
        classText.text = $"{characterClass}";
        characterNameText.text = $"{characterInfo.characterName}";

        levelText.text = $"{level}";

        changeVisibleCharacterButton.PlayerClass = characterClass;
    }
}
